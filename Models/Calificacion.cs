using System;
using System.Collections.Generic;

namespace IDS325L___ProyectoFinal___Índice_académico.Models
{
    public partial class Calificacion
    {
        public int Matricula { get; set; }
        public string CodigoAsignatura { get; set; } = null!;
        public string? Trimestre { get; set; }
        public string? Nota { get; set; }
        public int? IdSeccion { get; set; }
        public DateTime? FechaIngresoCalificacion { get; set; }
        public bool? VigenciaCalificacion { get; set; }

        public virtual Seccion? IdSeccionNavigation { get; set; }
        public virtual Persona MatriculaNavigation { get; set; } = null!;
    }
}
