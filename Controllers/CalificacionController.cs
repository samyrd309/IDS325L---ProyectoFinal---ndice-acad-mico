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

        public ActionResult IndexEstudiantes()
        {
            List<Calificacion> lista = _indiceContext.Calificacions.Include(c => c.MatriculaNavigation).Include(c => c.IdAsignaturaNavigation).Where(c => c.VigenciaCalificacion.Equals(true) && c.IdAsignaturaNavigation.VigenciaAsignatura.Equals(true) && c.Nota != null).ToList();
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

            return RedirectToAction("Index", "Calificacion");
        }

        // GET: CalificacionController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CalificacionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private bool CalificacionExists(int Matricula, int IdAsignatura, string Trimestre)
        {
            return (_indiceContext.Calificacions?.Any(e => e.Matricula == Matricula && e.IdAsignatura == IdAsignatura && e.Trimestre == Trimestre)).GetValueOrDefault();
        }

    }
}
