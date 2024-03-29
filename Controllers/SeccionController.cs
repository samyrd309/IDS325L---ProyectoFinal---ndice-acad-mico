﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IDS325L___ProyectoFinal___Índice_académico.Models;
using IDS325L___ProyectoFinal___Índice_académico.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace IDS325L___ProyectoFinal___Índice_académico.Controllers
{
    [Authorize]
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
                    oDocente = _indiceContext.Personas.Where(c => c.IdRol.Equals(3)&&c.VigenciaPersona.Equals(true)).Select(docente => new SelectListItem()
                    {
                        Text = docente.Nombre + ' ' + docente.Apellido,
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

            if (oSeccionVM.oSeccion.NumeroSeccion != null || oSeccionVM.oSeccion.Matricula != null || oSeccionVM.oSeccion.IdAsignatura != null)
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
            else
            {
                ViewBag.Message = "Debe ingresar todas las informaciones del formulario";
                return View("Create", oSeccionVM);
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
            List<Calificacion> lista = _indiceContext.Calificacions.Include(s => s.IdAsignaturaNavigation).Include(s=> s.Id).Include(m=>m.MatriculaNavigation).Where(a=>a.VigenciaCalificacion.Equals(true)).ToList();
            return PartialView("IndexAsignarEstudiantes", lista);
        }


        [HttpGet]
        public ActionResult CreateAsignarEstudiantes(int IdSeccion, int IdAsignatura)
        {
            AsignarEstudiantesVM oAsignarEstudiantesVM = new AsignarEstudiantesVM()
            {
                oCalificacion = new Calificacion(),
                //oSeccion = _indiceContext.Seccions.Select(seccion => new SelectListItem()
                //{
                //    Text = seccion.NumeroSeccion.ToString(),
                //    Value = seccion.IdSeccion.ToString()
                //}).ToList(),
                //oAsignatura = _indiceContext.Asignaturas.Select(asignatura => new SelectListItem()
                //{
                //    Text = asignatura.NombreAsignatura.ToString(),
                //    Value = asignatura.IdAsignatura.ToString(),
                //}).ToList(),
                oListaCalificaciones = _indiceContext.Calificacions.Include(s => s.IdAsignaturaNavigation).Include(s => s.Id).Include(m => m.MatriculaNavigation).Where(a => a.VigenciaCalificacion.Equals(true)&& a.Id.IdSeccion.Equals(IdSeccion)).ToList()

            };

            oAsignarEstudiantesVM.oCalificacion.Id = _indiceContext.Seccions.Find(IdSeccion, IdAsignatura);
            ViewBag.seccion = IdSeccion;
            ViewBag.asignatura = IdAsignatura;

            return View(oAsignarEstudiantesVM);
        }

        [HttpPost]
        public ActionResult insert(AsignarEstudiantesVM oAsignarEstudiantesVM , int Matricula, int IdAsignatura, int IdSeccion)
        {
            try
            {
                oAsignarEstudiantesVM.oCalificacion.Matricula = Matricula;
                oAsignarEstudiantesVM.oCalificacion.IdAsignatura = IdAsignatura;
                oAsignarEstudiantesVM.oCalificacion.IdSeccion = IdSeccion;
                _indiceContext.Calificacions.Add(oAsignarEstudiantesVM.oCalificacion);
                _indiceContext.SaveChanges();
                return RedirectToAction("CreateAsignarEstudiantes", "Seccion", new { IdSeccion, IdAsignatura });
            }
            catch (Exception)
            {
                return View("CreateAsignarEstudiantes", "Seccion");
            }
            
        }



        public JsonResult GetEstudiante(int? IdSeccion, int IdAsignatura)
        {

            int NroPeticion = Convert.ToInt32(Request.Form["draw"].FirstOrDefault() ?? "0");

            int CantidadRegistros = Convert.ToInt32(Request.Form["length"].FirstOrDefault() ?? "0");

            int OmitirRegistros = Convert.ToInt32(Request.Form["start"].FirstOrDefault() ?? "0");

            string ValorBuscado = Request.Form["search[value]"].FirstOrDefault() ?? "";

            var query = from Calificacion in _indiceContext.Set<Calificacion>()
                                            join Estudiante in _indiceContext.Set<Persona>()
                                                 on new { Calificacion.IdSeccion, Calificacion.Matricula, Calificacion.IdAsignatura } equals new {IdSeccion, Estudiante.Matricula, IdAsignatura}
                                            join Asignatura in _indiceContext.Set<Asignatura>()
                                                 on Calificacion.IdAsignatura equals Asignatura.IdAsignatura
                                            join Seccion in _indiceContext.Set<Seccion>()
                                                on Calificacion.IdSeccion equals Seccion.IdSeccion
                        select new {Estudiante.Matricula, Estudiante.Nombre, Estudiante.Apellido, Asignatura.CodigoAsignatura, Seccion.NumeroSeccion };
            int count = query.Count();

            query = query.Where(e =>string.Concat(e.Nombre, e.Apellido).Contains(ValorBuscado));

            int filterquery = query.Count();

            var lista = query.Skip(OmitirRegistros).Take(CantidadRegistros).ToList();

            return Json(new
            {
                draw = NroPeticion,
                recordsTotal = count,
                recordsFiltered = filterquery,
                data = lista
            });
        }





    }
}
