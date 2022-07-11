using IDS325L___ProyectoFinal___Índice_académico.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IDS325L___ProyectoFinal___Índice_académico.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IDS325L___ProyectoFinal___Índice_académico.Models;
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

        public IActionResult Index()
        {
            List<Asignatura> lista = _indiceContext.Asignaturas.ToList();
            return View(lista);
        }
        [HttpGet]
        public IActionResult Create(int  IdAsignatura)
        {
            AsignaturasVM oAsignaturasVM = new AsignaturasVM()
            {
                oAsignatura = new Asignatura(),
                CodigoAreaNavigation = _indiceContext.AreaAcademicas.Select(area => new SelectListItem()
                {
                    Text = area.NombreArea,
                    Value = area.CodigoArea
                }).ToList()
            };

            if (IdAsignatura != 0)
            {
                oAsignaturasVM.oAsignatura = _indiceContext.Asignaturas.Find(IdAsignatura);
        }


            return View(oAsignaturasVM);
        }
        [HttpPost]
        public IActionResult Create(AsignaturasVM oAsignaturasVM)
        {

            if (ModelState.IsValid)
            {
                var clave = _indiceContext.Asignaturas.FirstOrDefaultAsync(a => a.CodigoAsignatura == oAsignaturaVM.oAsignatura.CodigoAsignatura);
                if (clave == null)
                {
                    return View("Error");
                }

                if(oAsignaturasVM.oAsignatura.IdAsignatura == 0)
                {
                    var clave = _indiceContext.Asignaturas.FirstOrDefaultAsync(a => a.CodigoAsignatura == oAsignaturasVM.oAsignatura.CodigoAsignatura);
                    var nombre = _indiceContext.Asignaturas.FirstOrDefaultAsync(a => a.NombreAsignatura == oAsignaturasVM.oAsignatura.NombreAsignatura);
                    if (clave != null || nombre != null)
                    {

                        _indiceContext.Asignaturas.Update(oAsignaturasVM.oAsignatura);

                    }
            }
            else
            {
                    _indiceContext.Asignaturas.Add(oAsignaturasVM.oAsignatura);
            }

                //var clave = _indiceContext.Asignaturas.FirstOrDefaultAsync(a => a.CodigoAsignatura == oAsignaturasVM.oAsignatura.CodigoAsignatura);
                //var nombre = _indiceContext.Asignaturas.FirstOrDefaultAsync(a => a.NombreAsignatura == oAsignaturasVM.oAsignatura.NombreAsignatura);
                //if (clave != null || nombre!= null)
                //{

                //    _indiceContext.Asignaturas.Update(oAsignaturasVM.oAsignatura);

                //}
                //else
                //{
                //    _indiceContext.Asignaturas.Add(oAsignaturasVM.oAsignatura);
                //}

                _indiceContext.SaveChanges();

                return RedirectToAction("Index", "Asignaturas");
            }
            else
            {
                return View();
            }
        }
    }
}
