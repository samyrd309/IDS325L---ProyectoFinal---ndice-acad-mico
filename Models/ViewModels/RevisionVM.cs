using Microsoft.AspNetCore.Mvc.Rendering;

namespace IDS325L___ProyectoFinal___Índice_académico.Models.ViewModels
{
    public class RevisionVM
    {
        public Calificacion oCalificacion { get; set; }

        public List<Calificacion> oListaCalificacions { get; set; }
        
    }
}
