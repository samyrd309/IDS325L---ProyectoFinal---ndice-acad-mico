using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IDS325L___ProyectoFinal___Índice_académico.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IDS325L___ProyectoFinal___Índice_académico.Models.ViewModels;


namespace IDS325L___ProyectoFinal___Índice_académico.Controllers
{
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

            if(Matricula != 0)
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
            oEstudianteVM.oPersona.IdRol = 2;
            oEstudianteVM.oPersona.VigenciaPersona = true;
           if(oEstudianteVM.oPersona.Matricula == 0)
            {
                _indiceContext.Personas.Add(oEstudianteVM.oPersona);
            }
            else
            {
                _indiceContext.Personas.Update(oEstudianteVM.oPersona);
                
            }



            _indiceContext.SaveChanges();

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


            return View(oDocenteVM);
        }

        // Post CreateDocentes
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDocentes(DocentesVM oDocenteVM)
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


        //Get Delete Persona
        public ActionResult Delete(int Matricula)
        {
            Persona oPersona = _indiceContext.Personas.Include(c=> c.CarreraNavigation).Include(c=>c.CodigoAreaNavigation).Include(c=> c.IdRolNavigation).Where(c=>c.Matricula == Matricula).FirstOrDefault();
            return View(oPersona);
        }

        //Post Delete Persona
        [HttpPost]
        public ActionResult Delete(Persona oPersona)
        {
            oPersona = _indiceContext.Personas.Include(c => c.CarreraNavigation).Include(c => c.CodigoAreaNavigation).Include(c => c.IdRolNavigation).Where(c => c.Matricula == oPersona.Matricula).FirstOrDefault();
            oPersona.VigenciaPersona = false;
            _indiceContext.Update(oPersona);
            _indiceContext.SaveChanges();
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

