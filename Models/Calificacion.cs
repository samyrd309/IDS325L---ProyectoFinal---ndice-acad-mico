﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IDS325L___ProyectoFinal___Índice_académico.Models
{
    public partial class Calificacion
    {
        public int? IdCalificacion { get; set; }
        public int Matricula { get; set; }
        public int IdAsignatura { get; set; }
        public int? IdSeccion { get; set; }
        
        public string? Nota { get; set; }
        public string Trimestre { get; set; } = null!;
        public DateTime? FechaIngresoCalificacion { get; set; }
        public bool? VigenciaCalificacion { get; set; }

        public virtual Seccion? Id { get; set; }
        public virtual Persona MatriculaNavigation { get; set; } = null!;
        public virtual Literal? NotaNavigation { get; set; }

        public virtual Asignatura IdAsignaturaNavigation { get; set; } = null!;
    }
}
