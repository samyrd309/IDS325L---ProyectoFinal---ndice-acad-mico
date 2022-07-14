using Microsoft.AspNetCore.Mvc.Rendering;
namespace IDS325L___ProyectoFinal___Índice_académico.Models.ViewModels
{
    public class DocentesCalificacionesVM
    {
        public string IdCalificacion { get; set; }
        public Calificacion oCalificacion { get; set; }
        public Persona oEstudiante { get; set; }
        public List<Calificacion> oListaCalificaciones { get; set; }
        public List<Calificacion> oListaRevision { get; set; }
    }
}
