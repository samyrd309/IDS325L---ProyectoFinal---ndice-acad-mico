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



        // GET: Estudiantes
        public ActionResult IndexEstudiantes()
        {
            List<Persona> lista = _indiceContext.Personas.Include(c => c.IdRolNavigation).Include(c=>c.CarreraNavigation).Include(c=>c.CodigoAreaNavigation).Where(m => m.IdRol.Equals(2) && m.VigenciaPersona.Equals(true)).ToList();
            return View(lista);
        }
        [HttpPost]
        public ActionResult IndexEstudiantes(EstudianteVM oEstudianteVM)
        {
 



            List<Persona> lista = _indiceContext.Personas.Include(c => c.IdRolNavigation).Include(c => c.CarreraNavigation).Include(c => c.CodigoAreaNavigation).Where(m => m.IdRol.Equals(2) && m.VigenciaPersona.Equals(true)).ToList();
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
            if (!ModelState.IsValid)
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
          

            return RedirectToAction("IndexEstudiantes", "Persona");
        }

        // GET: PersonaController/Delete/5
        public ActionResult Delete(int Matricula)
        {
            Persona oPersona = _indiceContext.Personas.Include(c=> c.CarreraNavigation).Include(c=>c.CodigoArea).Include(c=> c.IdRolNavigation).Where(c=>c.Matricula == Matricula).FirstOrDefault();
            return View(oPersona);
        }

        // POST: PersonaController/Delete/5
        [HttpPost]
        public ActionResult Delete(Persona oPersona)
        {
            oPersona = _indiceContext.Personas.Include(c => c.CarreraNavigation).Include(c => c.CodigoAreaNavigation).Include(c => c.IdRolNavigation).Where(c => c.Matricula == oPersona.Matricula).FirstOrDefault();
            oPersona.VigenciaPersona = false;
            _indiceContext.Update(oPersona);
            _indiceContext.SaveChanges();

            return RedirectToAction("IndexEstudiantes", "Persona");
        }

        // GET: Estudiantes
        public ActionResult IndexDocentes()
        {
            List<Persona> lista = _indiceContext.Personas.Include(c => c.IdRolNavigation).Include(c => c.CarreraNavigation).Include(c => c.CodigoAreaNavigation).Where(m => m.IdRol.Equals(3) && m.VigenciaPersona.Equals(true)).ToList();
            return View(lista);
       }

        [HttpGet]
        public ActionResult CreateDocentes(int Matricula)
        {
            DocentesVM oDocentesVM = new DocentesVM()
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
                oDocentesVM.oPersona = _indiceContext.Personas.Find(Matricula);
            }


            return View(oDocentesVM);
        }

        // POST: PersonaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDocentes(DocentesVM oDocentesVM)
        {
            oDocentesVM.oPersona.IdRol = 3;
            oDocentesVM.oPersona.VigenciaPersona = true;
            if (oDocentesVM.oPersona.Matricula == 0)
            {
                _indiceContext.Personas.Add(oDocentesVM.oPersona);
            }
            else
            {
                _indiceContext.Personas.Update(oDocentesVM.oPersona);

            }
            _indiceContext.SaveChanges();

            return RedirectToAction("IndexDocentes", "Persona");
        }

    }
}
