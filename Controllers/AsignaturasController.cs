using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IDS325L___ProyectoFinal___Índice_académico.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IDS325L___ProyectoFinal___Índice_académico.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace IDS325L___ProyectoFinal___Índice_académico.Controllers
{

    [Authorize]
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

            List<Asignatura> lista = _indiceContext.Asignaturas.Include(c => c.CodigoAreaNavigation).Where(c => c.VigenciaAsignatura == true).ToList();
            return View(lista);
        }


        // GET: AsignaturasController/Create
        [HttpGet]
        public ActionResult Create(int idAsignatura)
        {
            AsignaturaVM oAsignaturaVM = new AsignaturaVM() {
                oAsignatura = new Asignatura(),
                oListaAreaAcademica = _indiceContext.AreaAcademicas.Select(area => new SelectListItem()
                {
                    Text = area.NombreArea,
                    Value = area.CodigoArea.ToString()
                }).ToList()
            };

            if(idAsignatura != 0)
            {
                oAsignaturaVM.oAsignatura = _indiceContext.Asignaturas.Find(idAsignatura);
            }

            ViewBag.AreaList = _indiceContext.AreaAcademicas.Select(x => new SelectListItem { Value = x.CodigoArea, Text = x.NombreArea }).ToList();
            return View(oAsignaturaVM);
        }

        // POST: AsignaturasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AsignaturaVM oAsignaturaVM)
        {

            ViewBag.AreaList = _indiceContext.AreaAcademicas.Select(x => new SelectListItem { Value = x.CodigoArea, Text = x.NombreArea }).ToList();

            if (oAsignaturaVM.oAsignatura.CodigoAsignatura == null || oAsignaturaVM.oAsignatura.NombreAsignatura == null || oAsignaturaVM.oAsignatura.Credito == 0 || oAsignaturaVM.oAsignatura.CodigoArea ==  null)
            {
                return View("Create", oAsignaturaVM);
            }

            if(_indiceContext.Asignaturas.FirstOrDefault(s => s.CodigoAsignatura == oAsignaturaVM.oAsignatura.CodigoAsignatura) != null)
            {
                ViewBag.Message = "El código de la asignatura ya existe en el sistema";
                return View("Create", oAsignaturaVM);
            }

            if (_indiceContext.Asignaturas.FirstOrDefault(s => s.NombreAsignatura == oAsignaturaVM.oAsignatura.NombreAsignatura) != null)
            {
                ViewBag.Message = "El nombre de la asignatura ya existe en el sistema";
                return View("Create", oAsignaturaVM);
            }

            try
            {
                oAsignaturaVM.oAsignatura.VigenciaAsignatura = true;
                if(oAsignaturaVM.oAsignatura.IdAsignatura == 0)
                    _indiceContext.Asignaturas.Add(oAsignaturaVM.oAsignatura);
                else
                    _indiceContext.Asignaturas.Update(oAsignaturaVM.oAsignatura);

                _indiceContext.SaveChanges();

            }
            catch (Exception)
            {

                throw;
            }



            return RedirectToAction("Index", "Asignaturas");
        }

        // GET: AsignaturasController/Delete/5
        [HttpGet]
        public ActionResult Delete(int idAsignatura)
        {
                               
            Asignatura oAsignatura = _indiceContext.Asignaturas.Include(c => c.CodigoAreaNavigation).Where(m => m.IdAsignatura == idAsignatura).FirstOrDefault();
            return View(oAsignatura);
        }

        // POST: AsignaturasController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Asignatura oAsignatura)
        {
            oAsignatura = _indiceContext.Asignaturas.Include(c => c.CodigoAreaNavigation).Where(c => c.IdAsignatura == oAsignatura.IdAsignatura).FirstOrDefault();
            oAsignatura.VigenciaAsignatura = false;
            _indiceContext.Update(oAsignatura);
            _indiceContext.SaveChanges();

            return RedirectToAction("Index", "Asignaturas");
        }
    }
}
