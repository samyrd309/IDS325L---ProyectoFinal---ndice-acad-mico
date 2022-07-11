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



        // GET: PersonaController
        public ActionResult IndexEstudiantes()
        {
            List<Persona> lista = _indiceContext.Personas.Include(c => c.oRol).Include(c=>c.oCarrera).Include(c=>c.oAreaAcademica).Where(m => m.IdRol.Equals(2) && m.VigenciaPersona.Equals(true)).ToList();
            return View(lista);
        }



        // GET: PersonaController/Create
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

        // POST: PersonaController/Create
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

        // GET: PersonaController/Delete/5
        public ActionResult Delete(int Matricula)
        {
            Persona oPersona = _indiceContext.Personas.Include(c=> c.oCarrera).Include(c=>c.oAreaAcademica).Include(c=> c.oRol).Where(c=>c.Matricula == Matricula).FirstOrDefault();
            return View(oPersona);
        }

        // POST: PersonaController/Delete/5
        [HttpPost]
        public ActionResult Delete(Persona oPersona)
        {
            oPersona = _indiceContext.Personas.Include(c => c.oCarrera).Include(c => c.oAreaAcademica).Include(c => c.oRol).Where(c => c.Matricula == oPersona.Matricula).FirstOrDefault();
            oPersona.VigenciaPersona = false;
            _indiceContext.Update(oPersona);
            _indiceContext.SaveChanges();

            return RedirectToAction("IndexEstudiantes", "Persona");
        }
    }
}
