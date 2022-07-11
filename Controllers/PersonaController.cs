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
            List<Persona> lista = _indiceContext.Personas.Include(c => c.oRol).Where(m => m.IdRol.Equals(2)).ToList();
            return View(lista);
        }



        // GET: PersonaController/Create
        [HttpGet]
        public ActionResult CreateEstudiantes()
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


            return View(oEstudianteVM);
        }

        // POST: PersonaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEstudiantes(EstudianteVM oEstudianteVM)
        {
            oEstudianteVM.oPersona.IdRol = 2;
           if(oEstudianteVM.oPersona.Matricula == 0)
            {
                _indiceContext.Personas.Add(oEstudianteVM.oPersona);
            }



            _indiceContext.SaveChanges();

            return RedirectToAction("IndexEstudiantes", "Persona");
        }

        // GET: PersonaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PersonaController/Edit/5
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

        // GET: PersonaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PersonaController/Delete/5
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
