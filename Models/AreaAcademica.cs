using System;
using System.Collections.Generic;

namespace IDS325L___ProyectoFinal___Índice_académico.Models
{
    public partial class AreaAcademica
    {
        public AreaAcademica()
        {
            Asignaturas = new HashSet<Asignatura>();
            Carreras = new HashSet<Carrera>();
            Personas = new HashSet<Persona>();
        }

        public string CodigoArea { get; set; } = null!;
        public string NombreArea { get; set; } = null!;
        public DateTime? FechaIngresoArea { get; set; }
        public bool? VigenciaArea { get; set; }

        public virtual ICollection<Asignatura> Asignaturas { get; set; }
        public virtual ICollection<Carrera> Carreras { get; set; }
        public virtual ICollection<Persona> Personas { get; set; }
    }
}
