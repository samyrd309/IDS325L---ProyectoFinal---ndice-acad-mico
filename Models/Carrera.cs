using System;
using System.Collections.Generic;

namespace IDS325L___ProyectoFinal___Índice_académico.Models
{
    public partial class Carrera
    {
        public Carrera()
        {
            Asignaturas = new HashSet<Asignatura>();
            Personas = new HashSet<Persona>();
        }

        public string CodigoCarrera { get; set; } = null!;
        public string NombreCarrera { get; set; } = null!;
        public DateTime? FechaIngresoCarrera { get; set; }
        public bool? VigenciaCarrera { get; set; }

        public virtual ICollection<Asignatura> Asignaturas { get; set; }
        public virtual ICollection<Persona> Personas { get; set; }
    }
}
