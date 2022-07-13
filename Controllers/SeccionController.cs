using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IDS325L___ProyectoFinal___Índice_académico.Models;
using IDS325L___ProyectoFinal___Índice_académico.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IDS325L___ProyectoFinal___Índice_académico.Controllers
{
    public class SeccionController : Controller
    {

        private readonly IndiceContext _indiceContext;
        public SeccionController(IndiceContext indiceContext)
        {
            _indiceContext = indiceContext;
        }

        // GET: SeccionController
        public ActionResult Index()
        {
            List<Seccion> lista = _indiceContext.Seccions.Include(c=>c.MatriculaNavigation).Include(c=>c.IdAsignaturaNavigation).Where(c=> c.VigenciaSección.Equals(true) && c.IdAsignaturaNavigation.VigenciaAsignatura.Equals(true)).ToList();
            return View(lista);
        }

        // GET: SeccionController/Create
        [HttpGet]
        public ActionResult Create(int IdSeccion, int IdAsignatura)
        {
            try
            {
                SeccionVM oSeccionVM = new SeccionVM()
                {
                    oSeccion = new Seccion(),
                    oAsignatura = _indiceContext.Asignaturas.Where(c => c.VigenciaAsignatura.Equals(true)).Select(asignatura => new SelectListItem()
                    {
                        Text = asignatura.NombreAsignatura,
                        Value = asignatura.IdAsignatura.ToString()
                    }).ToList(),
                    oDocente = _indiceContext.Personas.Where(c => c.IdRol.Equals(3)).Select(docente => new SelectListItem()
                    {
                        Text = docente.Nombre,
                        Value = docente.Matricula.ToString()
                    }).ToList()

                };

                if (IdSeccion != 0)
                {
                    oSeccionVM.oSeccion = _indiceContext.Seccions.Find(IdSeccion, IdAsignatura);
                }
                return View(oSeccionVM);
            }
            catch(Exception ex)
            {
                return View(ex);
            }
            
        }

        // POST: SeccionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SeccionVM oSeccionVM)
        {
            try
            {
                oSeccionVM.oSeccion.VigenciaSección = true;
                if (oSeccionVM.oSeccion.IdSeccion == 0)
                {
                    _indiceContext.Seccions.Add(oSeccionVM.oSeccion);

                }
                else
                {
                    _indiceContext.Seccions.Update(oSeccionVM.oSeccion);
                }
                _indiceContext.SaveChanges();
                return RedirectToAction("Index", "Seccion");
            }
            catch(Exception ex)
            {
                return View(ex);
            }
        }


        // GET: SeccionController/Delete/5
        [HttpGet]
        public ActionResult Delete(int IdSeccion)
        {
            Seccion oSeccion = _indiceContext.Seccions.Include(c=> c.IdAsignaturaNavigation).Include(c=> c.MatriculaNavigation).Where(s=> s.IdSeccion == IdSeccion).FirstOrDefault();

            return View(oSeccion);
        }

        // POST: SeccionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Seccion oSeccion)
        {
            oSeccion = _indiceContext.Seccions.Include(c => c.IdAsignaturaNavigation).Include(c => c.MatriculaNavigation).Where(s => s.IdSeccion == oSeccion.IdSeccion).FirstOrDefault();
            oSeccion.VigenciaSección = false;
            _indiceContext.Update(oSeccion);
            _indiceContext.SaveChanges();
            return RedirectToAction("Index", "Seccion");
        }

        public ActionResult IndexAsignarEstudiantes()
        {
            List<Calificacion> lista = _indiceContext.Calificacions.Include(s => s.IdAsignaturaNavigation).Include(s=> s.SeccionNavegation).Include(m=>m.MatriculaNavigation).Where(a=>a.VigenciaCalificacion.Equals(true)).ToList();
            return View(lista);
        }

        [HttpGet]
        public ActionResult CreateAsignarEstudiantes()
        {
            AsignarEstudiantesVM oAsignarEstudiantesVM = new AsignarEstudiantesVM()
            {
                oCalificacion = new Calificacion(),
                oEstudiante = _indiceContext.Personas.Where(p => p.IdRol.Equals(2)).Select(estudiante => new SelectListItem()
                {
                    Text = estudiante.Nombre + estudiante.Apellido,
                    Value = estudiante.Matricula.ToString()
                }).ToList(),
                oSeccion = _indiceContext.Seccions.Select(seccion => new SelectListItem()
                {
                    Text = seccion.NumeroSeccion.ToString(),
                    Value = seccion.IdSeccion.ToString()
                }).ToList(),
                oAsignatura = _indiceContext.Asignaturas.Select(asignatura => new SelectListItem()
                {
                    Text = asignatura.NombreAsignatura.ToString(),
                    Value = asignatura.IdAsignatura.ToString(),
                }).ToList()
            };
            return View(oAsignarEstudiantesVM);
        }





    }
}
