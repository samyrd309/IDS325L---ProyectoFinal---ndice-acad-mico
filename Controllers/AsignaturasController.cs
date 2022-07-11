using IDS325L___ProyectoFinal___Índice_académico.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IDS325L___ProyectoFinal___Índice_académico.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace IDS325L___ProyectoFinal___Índice_académico.Controllers
{
    public class AsignaturasController : Controller
    {
        private readonly IndiceContext _indiceContext;

        public AsignaturasController(IndiceContext indiceContext)
        {
            _indiceContext = indiceContext;
        }

        // GET: AsignaturasController
        public ActionResult Index()
        {
            List<Asignatura> lista = _indiceContext.Asignaturas.Where(a => a.VigenciaAsignatura == true).ToList();
            return View(lista);
        }

        // GET: AsignaturasController/Create
        public ActionResult Create()
        {
            AsignaturaVM oAsignaturaVM = new AsignaturaVM()
            {
                oAsignatura = new Asignatura(),
                oAreaAcademica = _indiceContext.AreaAcademicas.Select(area => new SelectListItem()
                {
                    Text = area.NombreArea,
                    Value = area.CodigoArea.ToString()
                }).ToList()
            };


            return View(oAsignaturaVM);
        }

        // POST: AsignaturasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AsignaturaVM oAsignaturaVM)
        {
            if (ModelState.IsValid)
            {
                var clave = _indiceContext.Asignaturas.FirstOrDefaultAsync(a => a.CodigoAsignatura == oAsignaturaVM.oAsignatura.CodigoAsignatura);
                if (clave == null)
                {
                    return View("Error");
                }

                var nombre = _indiceContext.Asignaturas.FirstOrDefaultAsync(a => a.NombreAsignatura == oAsignaturaVM.oAsignatura.NombreAsignatura);
                if (nombre == null)
                {
                    return View("Error");
                }

                _indiceContext.Asignaturas.Add(oAsignaturaVM.oAsignatura);
                _indiceContext.SaveChanges();

                return RedirectToAction("Index", "Asignaturas");
            }
            else
            {
                return View();
            }

            
        }

        // GET: AsignaturasController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AsignaturasController/Edit/5
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

        // GET: AsignaturasController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AsignaturasController/Delete/5
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
