using System;
using System.Collections.Generic;

namespace IDS325L___ProyectoFinal___Índice_académico.Models
{
    public partial class Persona
    {
        public Persona()
        {
            Calificacions = new HashSet<Calificacion>();
            Seccions = new HashSet<Seccion>();
        }

        public int Matricula { get; set; }
        public int IdRol { get; set; }
        public string? Carrera { get; set; }
        public string? CodigoArea { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string CorreoElectronico { get; set; } = null!;
        public bool? VigenciaPersona { get; set; }
        public decimal Indice { get; set; }
        public string Contraseña { get; set; } = null!;

        public virtual Carrera? oCarrera { get; set; }
        public virtual AreaAcademica? oAreaAcademica { get; set; }
        public virtual Rol oRol { get; set; } = null!;
        public virtual ICollection<Calificacion> Calificacions { get; set; }
        public virtual ICollection<Seccion> Seccions { get; set; }
    }
}
