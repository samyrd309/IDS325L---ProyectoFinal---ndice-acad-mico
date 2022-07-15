using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IDS325L___ProyectoFinal___Índice_académico.Models;
using IDS325L___ProyectoFinal___Índice_académico.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IDS325L___ProyectoFinal___Índice_académico.Controllers
{
    public class CalificacionController : Controller
    {



        private readonly IndiceContext _indiceContext;
        public CalificacionController(IndiceContext indiceContext)
        {
            _indiceContext = indiceContext;
        }
        // GET: CalificacionController
        public ActionResult Index()
        {
            List<Calificacion> lista = _indiceContext.Calificacions.Include(c => c.MatriculaNavigation).Include(c => c.IdAsignaturaNavigation).Where(c => c.VigenciaCalificacion.Equals(true) && c.IdAsignaturaNavigation.VigenciaAsignatura.Equals(true) && c.Nota != null).ToList();
            return View(lista);
        }

        public ActionResult IndexAsignaturasDocente(int Matricula)
        {
            Matricula = 5;
            List<Seccion> lista = _indiceContext.Seccions.Include(c => c.MatriculaNavigation).Include(c => c.IdAsignaturaNavigation).Where(c => c.VigenciaSección.Equals(true) && c.IdAsignaturaNavigation.VigenciaAsignatura.Equals(true)&& c.Matricula == Matricula).Distinct().ToList();

            var query = from Calificacion in _indiceContext.Set<Calificacion>()
                        join Seccion in _indiceContext.Set<Seccion>()
                            on Calificacion.IdSeccion equals Seccion.IdSeccion
                        select Calificacion.Trimestre;

            ViewBag.Trimestre = query;

            return View(lista);
        }


        // GET: CalificacionController/Create
        public ActionResult Create(int Matricula, int IdAsignatura, string Trimestre)
        {

            if (Matricula == null || _indiceContext.Calificacions == null)
            {
                return NotFound();
            }

            var calificacion = _indiceContext.Calificacions.Find(Matricula, IdAsignatura, Trimestre);
            if (calificacion == null)
            {
                return NotFound();
            }

            return View(calificacion);
        }

        // POST: CalificacionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int Matricula, int IdAsignatura, string Trimestre, Calificacion calificacion)
        {
            if (Matricula != calificacion.Matricula || IdAsignatura != calificacion.IdAsignatura || calificacion.Trimestre != Trimestre)
            {
                return NotFound();
            }

            try
            {
                calificacion.VigenciaCalificacion = true;
                _indiceContext.Update(calificacion);
                _indiceContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CalificacionExists(Matricula, IdAsignatura, Trimestre))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction("Index", "Calificacion");
        }

        public ActionResult IndexPublicar(int IdSeccion, int IdAsignatura, string Trimestre)
        {
            //IdSeccion = 4;
            //IdAsignatura = 2;
            //Trimestre = "2022-2";
            List<Calificacion> lista = _indiceContext.Calificacions.Include(c => c.MatriculaNavigation).Include(c => c.IdAsignaturaNavigation).Where(c => c.VigenciaCalificacion.Equals(true) && c.IdAsignaturaNavigation.VigenciaAsignatura.Equals(true) && c.IdSeccion == IdSeccion && c.IdAsignatura == IdAsignatura).ToList();
            return View(lista);
        }


        // GET: CalificacionController/Edit/5
        public ActionResult Publicar(int Matricula, int IdAsignatura, string Trimestre)
        {
            if (Matricula == null || _indiceContext.Calificacions == null)
            {
                return NotFound();
            }

            var calificacion = _indiceContext.Calificacions.Find(Matricula, IdAsignatura, Trimestre);
            if (calificacion == null)
            {
                return NotFound();
            }

            return View(calificacion);
        }

        // POST: CalificacionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Publicar(int Matricula, int IdAsignatura, string Trimestre, Calificacion calificacion)
        {
            if (Matricula != calificacion.Matricula || IdAsignatura != calificacion.IdAsignatura || calificacion.Trimestre != Trimestre)
            {
                return NotFound();
            }

            try
            {
                calificacion.VigenciaCalificacion = true;
                _indiceContext.Update(calificacion);
                _indiceContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CalificacionExists(Matricula, IdAsignatura, Trimestre))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction("IndexPublicar", "Calificacion");
        }

        private bool CalificacionExists(int Matricula, int IdAsignatura, string Trimestre)
        {
            return (_indiceContext.Calificacions?.Any(e => e.Matricula == Matricula && e.IdAsignatura == IdAsignatura && e.Trimestre == Trimestre)).GetValueOrDefault();
        }

    }
}
