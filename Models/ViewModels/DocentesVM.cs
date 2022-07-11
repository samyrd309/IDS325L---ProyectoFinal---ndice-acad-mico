using Microsoft.AspNetCore.Mvc.Rendering;

namespace IDS325L___ProyectoFinal___Índice_académico.Models.ViewModels
{
    public class DocentesVM
    {
        public Persona oPersona { get; set; }

        public List<SelectListItem> oCarrera { get; set; }
        public List<SelectListItem> oAreaAcademica { get; set; }
    }
}
