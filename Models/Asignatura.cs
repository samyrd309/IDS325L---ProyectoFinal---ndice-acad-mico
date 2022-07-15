using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IDS325L___ProyectoFinal___Índice_académico.Models
{
    public partial class Asignatura
    {
        public Asignatura()
        {
            Seccions = new HashSet<Seccion>();
        }

        public int IdAsignatura { get; set; }
        [Required]
        public string? CodigoAsignatura { get; set; }
        [Required]
        public string CodigoArea { get; set; } = null!;
        [Required]
        public int Credito { get; set; }
        [Required]
        public string NombreAsignatura { get; set; } = null!;
        public DateTime? FechaIngresoAsignatura { get; set; }
        public bool? VigenciaAsignatura { get; set; }

        public virtual AreaAcademica CodigoAreaNavigation { get; set; } = null!;
        public virtual ICollection<Seccion> Seccions { get; set; }
    }
}
