using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IDS325L___ProyectoFinal___Índice_académico.Controllers
{
    public class CalificacionController : Controller
    {
        // GET: CalificacionController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CalificacionController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CalificacionController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CalificacionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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
