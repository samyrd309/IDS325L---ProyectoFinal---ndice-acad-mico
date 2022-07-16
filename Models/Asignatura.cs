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
        [Required(ErrorMessage ="Ingrese el código de la asignatura")]
        public string? CodigoAsignatura { get; set; }
        [Required(ErrorMessage = "Ingrese el código del área académica")]
        public string CodigoArea { get; set; } = null!;
        [Required(ErrorMessage = "Ingrese la cantidad de créditos")]
        public int Credito { get; set; }
        [Required(ErrorMessage = "Ingrese el nombre de la asignatura")]
        public string NombreAsignatura { get; set; } = null!;
        public DateTime? FechaIngresoAsignatura { get; set; }
        public bool? VigenciaAsignatura { get; set; }

        public virtual AreaAcademica CodigoAreaNavigation { get; set; } = null!;
        public virtual ICollection<Seccion> Seccions { get; set; }
    }
}
