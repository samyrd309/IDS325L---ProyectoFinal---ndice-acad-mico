using IDS325L___ProyectoFinal___Índice_académico.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Linq;

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

            return View();
           
        }


        public JsonResult ListaCalificaciones(int Matricula)
        {
            Matricula = 2;
            
            int NroPeticion = Convert.ToInt32(Request.Form["draw"].FirstOrDefault() ?? "0");

            int CantidadRegistros = Convert.ToInt32(Request.Form["length"].FirstOrDefault() ?? "0");

            int OmitirRegistros = Convert.ToInt32(Request.Form["start"].FirstOrDefault() ?? "0");

            string ValorBuscado = Request.Form["search[value]"].FirstOrDefault() ?? "";

            var query = from Calificacion in _indiceContext.Set<Calificacion>()
                        join Asignatura in _indiceContext.Set<Asignatura>()
                            on new { Calificacion.IdAsignatura, Calificacion.Matricula } equals new { Asignatura.IdAsignatura, Matricula }
                        select new { Asignatura.CodigoAsignatura, Asignatura.NombreAsignatura, Asignatura.Credito, Calificacion.Trimestre ,Calificacion.Nota };

            int count = query.Count();

            query = query.Where(e => e.NombreAsignatura.Contains(ValorBuscado));

            int filterquery = query.Count();

            var lista = query.Skip(OmitirRegistros).Take(CantidadRegistros).ToList();

            return Json(new
            {
                draw = NroPeticion,
                recordsTotal = count,
                recordsFiltered = filterquery,
                data = lista
            });
        }

        public JsonResult ListaEstudiantes()
        {
            int NroPeticion = Convert.ToInt32(Request.Form["draw"].FirstOrDefault() ?? "0");

            int CantidadRegistros = Convert.ToInt32(Request.Form["length"].FirstOrDefault() ?? "0");

            int OmitirRegistros = Convert.ToInt32(Request.Form["start"].FirstOrDefault() ?? "0");

            string ValorBuscado = Request.Form["search[value]"].FirstOrDefault() ?? "";

            IQueryable<Persona> query = _indiceContext.Personas.Where(x => x.VigenciaPersona == true && x.IdRol == 2);

            int count = query.Count();

            query = query.Where(e => e.Carrera.Contains(ValorBuscado));

            int filterquery = query.Count();

            var lista = query.Skip(OmitirRegistros).Take(CantidadRegistros).ToList();

            return Json( new { 
                draw = NroPeticion,
                recordsTotal = count,
                recordsFiltered = filterquery,
                data = lista} );
        }

    }
}
