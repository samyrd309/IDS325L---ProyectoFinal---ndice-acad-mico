using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IDS325L___ProyectoFinal___Índice_académico.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IDS325L___ProyectoFinal___Índice_académico.Models.ViewModels;
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

            List<Asignatura> lista = _indiceContext.Asignaturas.Include(c => c.CodigoAreaNavigation).Where(m => m.VigenciaAsignatura.Equals(true)).ToList();
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

            return View(oAsignaturaVM);
        }

        // POST: AsignaturasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AsignaturaVM oAsignaturaVM)
        {
            oAsignaturaVM.oAsignatura.VigenciaAsignatura = true;
            if(oAsignaturaVM.oAsignatura.IdAsignatura == 0)
            {
                _indiceContext.Asignaturas.Add(oAsignaturaVM.oAsignatura);
            }
            else
            {
                _indiceContext.Asignaturas.Update(oAsignaturaVM.oAsignatura);
            }

            _indiceContext.SaveChanges();

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
