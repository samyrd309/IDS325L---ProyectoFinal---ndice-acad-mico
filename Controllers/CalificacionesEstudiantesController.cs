using IDS325L___ProyectoFinal___Índice_académico.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System.Data;

namespace IDS325L___ProyectoFinal___Índice_académico.Controllers
{
    public class CalificacionesEstudiantesController : Controller
    {
        private readonly IndiceContext _indiceContext;
        private readonly IConfiguration _config;

        public CalificacionesEstudiantesController(IndiceContext indiceContext, IConfiguration config)
        {
            _indiceContext = indiceContext;
            _config = config;
        }

        // GET: CalificacionesEstudiantesController
        public ActionResult Index(int Matricula)
        {
            Matricula = 2;

            DataSet data = new DataSet();
            using (SqlConnection con = new SqlConnection(_config.GetConnectionString("cadenaSQL")))
            {
                string q = $"CalcularIndice '{Matricula}'";
                using (SqlCommand sql = new SqlCommand(q))
                {
                    sql.Connection = con;
                    sql.CommandType = CommandType.Text;
                    con.Open();
                    SqlDataReader reader = sql.ExecuteReader();
                    if (reader.Read())
                    {
                        ViewBag.Indice = reader["Indice"].ToString();
                        ViewBag.Total = reader["TotalCreditos"].ToString();
                        ViewBag.Merito = reader["Meritos"].ToString();
                    }

                    con.Close();
                }
            }


            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(_config.GetConnectionString("cadenaSQL")))
            {
                string query = $"EXEC MostrarCalificaciones '{Matricula}'";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter())
                    {
                        adapter.SelectCommand = cmd;
                        adapter.Fill(ds);
                    }
                }
            }

            return View(ds);
        }

        public IActionResult Ranking(string txtCarrera)
        {
            //txtCarrera = "IDS";
            ViewBag.CarreraList = _indiceContext.Carreras.Select(x => new SelectListItem { Value = x.CodigoCarrera, Text = x.NombreCarrera }).ToList();
            return View(_indiceContext.Personas.Where(p => p.VigenciaPersona == true && p.IdRol == 2 && p.Carrera == txtCarrera).OrderBy(p => p.Indice).ToList());
        }

    }
}
