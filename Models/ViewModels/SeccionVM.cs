using Microsoft.AspNetCore.Mvc.Rendering;

namespace IDS325L___ProyectoFinal___Índice_académico.Models.ViewModels
{
    public class SeccionVM
    {
        public Seccion oSeccion  { get; set; }
        public List<SelectListItem> oDocente { get; set; }
        public List<SelectListItem> oAsignatura { get; set; }
    }
}
