using Microsoft.AspNetCore.Mvc.Rendering;

namespace IDS325L___ProyectoFinal___Índice_académico.Models.ViewModels
{
    public class AsignaturasVM
    {
        public Asignatura oAsignatura { get; set; }
        public List<SelectListItem> CodigoAreaNavigation { get; internal set; }
    }
}
