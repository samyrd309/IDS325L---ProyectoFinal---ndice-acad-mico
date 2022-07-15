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

        public int IdSeccion { get; set; }
        public int? Matricula { get; set; }
        public int IdAsignatura { get; set; }
        public int NumeroSeccion { get; set; }
        public DateTime? FechaIngresoSección { get; set; }
        public bool? VigenciaSección { get; set; }

        public virtual Asignatura IdAsignaturaNavigation { get; set; } = null!;
        public virtual List<Asignatura> IdAsignaturaNavigation2 { get; set; } = null!;
        public virtual Persona? MatriculaNavigation { get; set; }
        public virtual ICollection<Calificacion> Calificacions { get; set; }
    }
}
