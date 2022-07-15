using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IDS325L___ProyectoFinal___Índice_académico.Models;
using IDS325L___ProyectoFinal___Índice_académico.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace IDS325L___ProyectoFinal___Índice_académico.Controllers
{
    [Authorize]
    public class DocenteController : Controller
    {
        private readonly IndiceContext _indiceContext;
        public DocenteController(IndiceContext indiceContext)
        {
            _indiceContext = indiceContext;
        }

        // GET: SeccionController
        public ActionResult Index(int MatriculaProfesor)
        {
            MatriculaProfesor = 4;
            List<Seccion> lista = _indiceContext.Seccions.Include(c => c.MatriculaNavigation).Include(c => c.IdAsignaturaNavigation).Where(c => c.VigenciaSección.Equals(true) && c.IdAsignaturaNavigation.VigenciaAsignatura.Equals(true)&&c.Matricula == MatriculaProfesor).ToList();
            return View(lista);
        }

        [HttpGet]
        public ActionResult EditarCalificacion(int Matricula, int IdAsignatura, int IdCalificacion, string Trimestre)
        {
            DocentesCalificacionesVM oAsignarEstudiantesVM = new DocentesCalificacionesVM()
            {
                oListaRevision = _indiceContext.Calificacions.Include(s => s.Id.IdAsignaturaNavigation).Include(s => s.Id).Include(m => m.MatriculaNavigation).Where(a => a.VigenciaCalificacion.Equals(true)).ToList(),
                oCalificacion = new Calificacion()
            };
            if (IdCalificacion == 0)
                oAsignarEstudiantesVM.oCalificacion.Id = _indiceContext.Seccions.Find(Matricula, IdAsignatura);
            else
            {
                oAsignarEstudiantesVM.oCalificacion.IdCalificacion = IdCalificacion;
                oAsignarEstudiantesVM.oCalificacion.IdAsignatura = IdAsignatura;
                oAsignarEstudiantesVM.oCalificacion.Matricula = Matricula;
                oAsignarEstudiantesVM.oCalificacion.Trimestre = Trimestre;
                oAsignarEstudiantesVM.oCalificacion = _indiceContext.Calificacions.Find(Matricula, IdAsignatura, Trimestre, IdCalificacion);
            }

            //oAsignarEstudiantesVM.oCalificacion.IdCalificacion = _indiceContext.Calificacions.Find(oAsignarEstudiantesVM.IdCalificacion);




            return View(oAsignarEstudiantesVM);
        }

        [HttpPost]
        public ActionResult EditarCalificacion(DocentesCalificacionesVM oCalificacionVM, int Matricula, int IdAsignatura, int IdCalificacion,int IdSeccion, string Nota)
        {

            if (oCalificacionVM.oCalificacion.IdCalificacion != 0)
            {
                oCalificacionVM.oCalificacion.Matricula = Matricula;
                oCalificacionVM.oCalificacion.IdAsignatura = IdAsignatura;
                oCalificacionVM.oCalificacion.IdCalificacion = IdCalificacion;
                oCalificacionVM.oCalificacion.IdSeccion = IdSeccion;
                oCalificacionVM.oCalificacion.Nota = Nota;
                //oAsignarEstudiantesVM.oCalificacion.Nota = Nota;
                _indiceContext.Calificacions.Update(oCalificacionVM.oCalificacion);
            }
            _indiceContext.SaveChanges();

            return RedirectToAction("EditarCalificacion", "Docente");
        }
    }
}
