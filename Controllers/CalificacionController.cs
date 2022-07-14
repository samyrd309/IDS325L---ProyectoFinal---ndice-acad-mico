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
        public ActionResult Create()
        {
            try
            {
                RevisionVM oSeccionVM = new RevisionVM()
                {
                    oCalificacion = new Calificacion(),

                };

                return View(oSeccionVM);
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }
        private string conversion(int Nota)
        {
            string literal;

            if (Nota > 89)
                literal = "A";
            else if (Nota > 84)
                literal = "B+";
            else if (Nota > 79)
                literal = "B";
            else if (Nota > 74)
                literal = "C+";
            else if (Nota > 69)
                literal = "C";
            else if (Nota > 59)
                literal = "D";
            else if (Nota > 1)
                literal = "F";
            else
                literal = "R";

            return literal;
        }


        // POST: CalificacionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RevisionVM oRevisionVM)
        {
            try
            {
                if(oRevisionVM.oCalificacion.IdCalificacion != 0)
                {
                    oRevisionVM.oCalificacion = new Calificacion(); 
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CalificacionController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CalificacionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        private string conversion(int Nota)
        {
            string literal;

            if (Nota > 89)
                literal = "A";
            else if (Nota > 84)
                literal = "B+";
            else if (Nota > 79)
                literal = "B";
            else if (Nota > 74)
                literal = "C+";
            else if (Nota > 69)
                literal = "C";
            else if (Nota > 59)
                literal = "D";
            else if (Nota > 1)
                literal = "F";
            else
                literal = "R";

            return literal;
        }

    }
}
