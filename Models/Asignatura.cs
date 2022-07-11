using System;
using System.Collections.Generic;

namespace IDS325L___ProyectoFinal___Índice_académico.Models
{
    public partial class Asignatura
    {
        public Asignatura()
        {
            Seccions = new HashSet<Seccion>();
        }

        public int IdAsignatura { get; set; }
        public string? CodigoAsignatura { get; set; }
        public string CodigoArea { get; set; } = null!;
        public int Credito { get; set; }
        public string NombreAsignatura { get; set; } = null!;
        public DateTime? FechaIngresoAsignatura { get; set; }
        public bool? VigenciaAsignatura { get; set; }

        public virtual AreaAcademica CodigoAreaNavigation { get; set; } = null!;
        public virtual ICollection<Seccion> Seccions { get; set; }
    }
}
