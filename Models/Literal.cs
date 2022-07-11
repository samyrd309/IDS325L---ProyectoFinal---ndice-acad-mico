using System;
using System.Collections.Generic;

namespace IDS325L___ProyectoFinal___Índice_académico.Models
{
    public partial class Literal
    {
        public Literal()
        {
            Calificacions = new HashSet<Calificacion>();
        }

        public string Nota { get; set; } = null!;
        public decimal? Numero { get; set; }

        public virtual ICollection<Calificacion> Calificacions { get; set; }
    }
}
