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
    }
}
