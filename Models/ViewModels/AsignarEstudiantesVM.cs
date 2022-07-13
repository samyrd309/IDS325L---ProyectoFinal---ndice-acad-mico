using Microsoft.AspNetCore.Mvc.Rendering;

namespace IDS325L___ProyectoFinal___Índice_académico.Models.ViewModels
{
    public class AsignarEstudiantesVM
    {
        public Calificacion oCalificacion { get; set; }
        public Persona oEstudiante { get; set; }


        public List<Calificacion> oListaCalificaciones { get; set; }
        
    }
}
