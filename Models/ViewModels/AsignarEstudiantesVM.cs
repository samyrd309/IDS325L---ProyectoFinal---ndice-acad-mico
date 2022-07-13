using Microsoft.AspNetCore.Mvc.Rendering;

namespace IDS325L___ProyectoFinal___Índice_académico.Models.ViewModels
{
    public class AsignarEstudiantesVM
    {
        public Calificacion oCalificacion { get; set; }
        public List<SelectListItem> oEstudiante { get; set; }
        public List<SelectListItem> oSeccion { get; set; }
        public List<SelectListItem> oAsignatura { get; set; }
        
    }
}
