using System;
using System.Collections.Generic;

namespace IDS325L___ProyectoFinal___Índice_académico.Models
{
    public partial class Rol
    {
        public Rol()
        {
            Personas = new HashSet<Persona>();
        }

        public int IdRol { get; set; }
        public string DescripcionRol { get; set; } = null!;
        public DateTime? FechaIngresoRol { get; set; }
        public bool? VigenciaRol { get; set; }

        public virtual ICollection<Persona> Personas { get; set; }
    }
}
