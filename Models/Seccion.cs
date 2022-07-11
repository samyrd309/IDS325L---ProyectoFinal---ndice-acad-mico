using System;
using System.Collections.Generic;

namespace IDS325L___ProyectoFinal___Índice_académico.Models
{
    public partial class Seccion
    {
        public Seccion()
        {
            Calificacions = new HashSet<Calificacion>();
        }

        public string? CodigoAsignatura { get; set; }
        public int IdSeccion { get; set; }
        public int? MatriculaProfesor { get; set; }
        public DateTime? FechaIngresoSeccion { get; set; }
        public bool? VigenciaSección { get; set; }
        public int? NumeroSección { get; set; }

        public virtual Asignatura? CodigoAsignaturaNavigation { get; set; }
        public virtual Persona? MatriculaProfesorNavigation { get; set; }
        public virtual ICollection<Calificacion> Calificacions { get; set; }
    }
}
