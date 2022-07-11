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
        public string CodigoAsignatura { get; set; } = null!;
        public DateTime? FechaIngresoSección { get; set; }
        public bool? VigenciaSección { get; set; }

        public virtual Asignatura CodigoAsignaturaNavigation { get; set; } = null!;
        public virtual Persona? MatriculaNavigation { get; set; }
        public virtual ICollection<Calificacion> Calificacions { get; set; }
    }
}
