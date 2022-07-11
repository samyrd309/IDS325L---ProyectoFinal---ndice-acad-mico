using System;
using System.Collections.Generic;

namespace IDS325L___ProyectoFinal___Índice_académico.Models
{
    public partial class Asignatura
    {

        {
            Seccions = new HashSet<Seccion>();
        }

        public string CodigoAsignatura { get; set; } = null!;
        public string CodigoArea { get; set; } = null!;
        public int Credito { get; set; }
        public string NombreAsignatura { get; set; } = null!;
        public DateTime? FechaIngresoAsignatura { get; set; }
        public bool? VigenciaAsignatura { get; set; }
        public int IdAsignatura { get; set; }

        public virtual AreaAcademica CodigoAreaNavigation { get; set; } = null!;
    }
}
