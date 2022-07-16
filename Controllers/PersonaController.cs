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
    public class PersonaController : Controller
    {
        private readonly IndiceContext _indiceContext;

        public PersonaController(IndiceContext indiceContext)
        {
            _indiceContext = indiceContext;
        }



        //Get IndexEstudiantes
        public ActionResult IndexEstudiantes()
        {
            List<Persona> lista = _indiceContext.Personas.Include(c => c.IdRolNavigation).Include(c=>c.CarreraNavigation).Include(c=>c.CodigoAreaNavigation).Where(m => m.IdRol.Equals(2) && m.VigenciaPersona.Equals(true)).ToList();
            return View(lista);
        }
        //Get IndexDocentes
        public ActionResult IndexDocentes()
        {
            List<Persona> lista = _indiceContext.Personas.Include(c => c.IdRolNavigation).Include(c => c.CarreraNavigation).Include(c => c.CodigoAreaNavigation).Where(m => m.IdRol.Equals(3) && m.VigenciaPersona.Equals(true)).ToList();
            return View(lista);
        }


        //Get CreateEstudiantes
        [HttpGet]
        public ActionResult CreateEstudiantes(int Matricula)
        {
            EstudianteVM oEstudianteVM = new EstudianteVM()
            {
                oPersona = new Persona(),
                oCarrera = _indiceContext.Carreras.Select(carrera => new SelectListItem()
                {
                    Text = carrera.NombreCarrera,
                    Value = carrera.CodigoCarrera.ToString()
                }).ToList(),

                oAreaAcademica = _indiceContext.AreaAcademicas.Select(area => new SelectListItem()
                {
                    Text = area.NombreArea,
                    Value = area.CodigoArea.ToString()
                }).ToList()
            };

            ViewBag.AreaList = _indiceContext.AreaAcademicas.Select(x => new SelectListItem { Value = x.CodigoArea, Text = x.NombreArea }).ToList();
            ViewBag.CarreraList = _indiceContext.Carreras.Select(x => new SelectListItem { Value = x.CodigoCarrera, Text = x.NombreCarrera }).ToList();

            if (Matricula != 0)
            {
                oEstudianteVM.oPersona = _indiceContext.Personas.Find(Matricula);
            }


            return View(oEstudianteVM);
        }

        //Post CreateEstudiantes
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEstudiantes(EstudianteVM oEstudianteVM)
        {
            ViewBag.AreaList = _indiceContext.AreaAcademicas.Select(x => new SelectListItem { Value = x.CodigoArea, Text = x.NombreArea }).ToList();
            ViewBag.CarreraList = _indiceContext.Carreras.Select(x => new SelectListItem { Value = x.CodigoCarrera, Text = x.NombreCarrera }).ToList();

            if (oEstudianteVM.oPersona.Apellido != null || oEstudianteVM.oPersona.Nombre != null || oEstudianteVM.oPersona.CodigoArea != null || oEstudianteVM.oPersona.Carrera != null || oEstudianteVM.oPersona.Contraseña != null || oEstudianteVM.oPersona.CorreoElectronico != null)
            {
                oEstudianteVM.oPersona.IdRol = 2;
                oEstudianteVM.oPersona.VigenciaPersona = true;
                if (oEstudianteVM.oPersona.Matricula == 0)
                {
                    _indiceContext.Personas.Add(oEstudianteVM.oPersona);
                }
                else
                {
                    _indiceContext.Personas.Update(oEstudianteVM.oPersona);
                }
                _indiceContext.SaveChanges();
            }
            else
            {
                ViewBag.Message = "Debe ingresar todas las informaciones del formulario";
                return View("CreateEstudiantes", oEstudianteVM);
            }
           

            return RedirectToAction("IndexEstudiantes", "Persona");
        }

        //Get CreateDocentes

        [HttpGet]
        public ActionResult CreateDocentes(int Matricula)
        {
            DocentesVM oDocenteVM = new DocentesVM()
            {
                oPersona = new Persona(),
                oCarrera = _indiceContext.Carreras.Select(carrera => new SelectListItem()
                {
                    Text = carrera.NombreCarrera,
                    Value = carrera.CodigoCarrera.ToString()
                }).ToList(),

                oAreaAcademica = _indiceContext.AreaAcademicas.Select(area => new SelectListItem()
                {
                    Text = area.NombreArea,
                    Value = area.CodigoArea.ToString()
                }).ToList()
            };

            if (Matricula != 0)
            {
                oDocenteVM.oPersona = _indiceContext.Personas.Find(Matricula);
            }

            ViewBag.AreaList = _indiceContext.AreaAcademicas.Select(x => new SelectListItem { Value = x.CodigoArea, Text = x.NombreArea }).ToList();
            ViewBag.CarreraList = _indiceContext.Carreras.Select(x => new SelectListItem { Value = x.CodigoCarrera, Text = x.NombreCarrera }).ToList();
            return View(oDocenteVM);
        }

        // Post CreateDocentes
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDocentes(DocentesVM oDocenteVM)
        {
            ViewBag.AreaList = _indiceContext.AreaAcademicas.Select(x => new SelectListItem { Value = x.CodigoArea, Text = x.NombreArea }).ToList();
            ViewBag.CarreraList = _indiceContext.Carreras.Select(x => new SelectListItem { Value = x.CodigoCarrera, Text = x.NombreCarrera }).ToList();

            if (oDocenteVM.oPersona.Apellido != null || oDocenteVM.oPersona.Nombre != null || oDocenteVM.oPersona.CodigoArea != null || oDocenteVM.oPersona.Carrera != null || oDocenteVM.oPersona.Contraseña != null || oDocenteVM.oPersona.CorreoElectronico != null)
            {
                oDocenteVM.oPersona.IdRol = 3;
                oDocenteVM.oPersona.VigenciaPersona = true;
                if (oDocenteVM.oPersona.Matricula == 0)
                {
                    _indiceContext.Personas.Add(oDocenteVM.oPersona);
                }
                else
                {
                    _indiceContext.Personas.Update(oDocenteVM.oPersona);

                }

                _indiceContext.SaveChanges();

                return RedirectToAction("IndexDocentes", "Persona");
            }
            else
            {
                ViewBag.Message = "Debe ingresar todas las informaciones del formulario";
                return View("CreateDocentes", oDocenteVM);
            }
        }


        //Get Delete Persona
        public ActionResult Delete(int Matricula)
        {
            try
            {
                Persona oPersona = _indiceContext.Personas.Include(c => c.CarreraNavigation).Include(c => c.CodigoAreaNavigation).Include(c => c.IdRolNavigation).Where(c => c.Matricula == Matricula).FirstOrDefault();
                return View(oPersona);
            }
            catch (Exception)
            {
                TempData["Error"] = "Error al eliminar el registro";
                return View();
                throw;
            }
            
        }

        //Post Delete Persona
        [HttpPost]
        public ActionResult Delete(Persona oPersona)
        {
            oPersona = _indiceContext.Personas.Include(c => c.CarreraNavigation).Include(c => c.CodigoAreaNavigation).Include(c => c.IdRolNavigation).Where(c => c.Matricula == oPersona.Matricula).FirstOrDefault();
            oPersona.VigenciaPersona = false;
            _indiceContext.SaveChanges();
            _indiceContext.Update(oPersona);
            if(oPersona.IdRol == 2)
            {
                return RedirectToAction("IndexEstudiantes", "Persona");
            }

            else if (oPersona.IdRol == 3)
            {
                return RedirectToAction("IndexDocentes", "Persona");
            }
            
            return View(oPersona);
        }

    }
}

