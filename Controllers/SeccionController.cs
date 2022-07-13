using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IDS325L___ProyectoFinal___Índice_académico.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace IDS325L___ProyectoFinal___Índice_académico.Controllers
{
    public class SeccionController : Controller
    {
        private readonly IndiceContext _indiceContext;
        // GET: SeccionController
        public ActionResult Index()
        {
            List<Seccion> list = _indiceContext.Seccions.Include(a=>a.IdAsignaturaNavigation).Include(m=>m.MatriculaNavigation).Where(m=>m.VigenciaSección.Equals(true)).ToList();
            return View();
        }

        // GET: SeccionController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SeccionController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SeccionController/Create
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

        // GET: SeccionController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SeccionController/Edit/5
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

        // GET: SeccionController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SeccionController/Delete/5
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
