﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace IDS325L___ProyectoFinal___Índice_académico.Models
{
    public partial class IndiceContext : DbContext
    {
        public IndiceContext()
        {
        }

        public IndiceContext(DbContextOptions<IndiceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AreaAcademica> AreaAcademicas { get; set; } = null!;
        public virtual DbSet<Asignatura> Asignaturas { get; set; } = null!;
        public virtual DbSet<Calificacion> Calificacions { get; set; } = null!;
        public virtual DbSet<Carrera> Carreras { get; set; } = null!;
        public virtual DbSet<Literal> Literals { get; set; } = null!;
        public virtual DbSet<Persona> Personas { get; set; } = null!;
        public virtual DbSet<Rol> Rols { get; set; } = null!;
        public virtual DbSet<Seccion> Seccions { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-91438D4;Initial Catalog=Indice;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AreaAcademica>(entity =>
            {
                entity.HasKey(e => e.CodigoArea)
                    .HasName("PK__AreaAcad__CF230A45586FED18");

                entity.ToTable("AreaAcademica");

                entity.Property(e => e.CodigoArea)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.FechaIngresoArea)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.NombreArea)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.VigenciaArea)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Asignatura>(entity =>
            {
                entity.HasKey(e => e.CodigoAsignatura)
                    .HasName("PK__Asignatu__4783438F7AFC5527");

                entity.ToTable("Asignatura");

                entity.HasIndex(e => e.NombreAsignatura, "UQ__Asignatu__101CCCC5A327A549")
                    .IsUnique();

                entity.Property(e => e.CodigoAsignatura).HasMaxLength(7);

                entity.Property(e => e.CodigoArea)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.FechaIngresoAsignatura)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.NombreAsignatura)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.VigenciaAsignatura)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.CodigoAreaNavigation)
                    .WithMany(p => p.Asignaturas)
                    .HasForeignKey(d => d.CodigoArea)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Asignatura.CodigoArea");
            });

            modelBuilder.Entity<Calificacion>(entity =>
            {
                entity.HasKey(e => new { e.Matricula, e.CodigoAsignatura, e.Trimestre })
                    .HasName("PK__Califica__C34B8FFA635812F8");

                entity.ToTable("Calificacion");

                entity.Property(e => e.CodigoAsignatura).HasMaxLength(7);

                entity.Property(e => e.Trimestre)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasDefaultValueSql("([dbo].[TrimestreAct]())")
                    .IsFixedLength();

                entity.Property(e => e.FechaIngresoCalificacion)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nota).HasMaxLength(2);

                entity.Property(e => e.VigenciaCalificacion).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.MatriculaNavigation)
                    .WithMany(p => p.Calificacions)
                    .HasForeignKey(d => d.Matricula)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Calificacion.Matricula");

                entity.HasOne(d => d.NotaNavigation)
                    .WithMany(p => p.Calificacions)
                    .HasForeignKey(d => d.Nota)
                    .HasConstraintName("FK_Calificacion.Nota");

                entity.HasOne(d => d.Seccion)
                    .WithMany(p => p.Calificacions)
                    .HasForeignKey(d => new { d.IdSeccion, d.CodigoAsignatura })
                    .HasConstraintName("FK_Calificacion.IdSeccion");
            });

            modelBuilder.Entity<Carrera>(entity =>
            {
                entity.HasKey(e => e.CodigoCarrera)
                    .HasName("PK__Carrera__2D5445FCC9E7883A");

                entity.ToTable("Carrera");

                entity.Property(e => e.CodigoCarrera)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.FechaIngresoCarrera)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.NombreCarrera)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.VigenciaCarrera)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Literal>(entity =>
            {
                entity.HasKey(e => e.Nota)
                    .HasName("PK__Literal__7D8C2AD04CD8E2CA");

                entity.ToTable("Literal");

                entity.Property(e => e.Nota).HasMaxLength(2);

                entity.Property(e => e.Numero).HasColumnType("decimal(2, 1)");
            });

            modelBuilder.Entity<Persona>(entity =>
            {
                entity.HasKey(e => e.Matricula)
                    .HasName("PK__Persona__0FB9FB4EA68707A1");

                entity.ToTable("Persona");

                entity.Property(e => e.Apellido)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Carrera)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.CodigoArea)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Contraseña)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.CorreoElectronico)
                    .HasMaxLength(320)
                    .IsUnicode(false);

                entity.Property(e => e.FechaIngresoPersona)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Indice).HasColumnType("decimal(3, 2)");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.VigenciaPersona).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.CarreraNavigation)
                    .WithMany(p => p.Personas)
                    .HasForeignKey(d => d.Carrera)
                    .HasConstraintName("FK_Persona.Carrera");

                entity.HasOne(d => d.CodigoAreaNavigation)
                    .WithMany(p => p.Personas)
                    .HasForeignKey(d => d.CodigoArea)
                    .HasConstraintName("FK_Persona.CodigoArea");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.Personas)
                    .HasForeignKey(d => d.IdRol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Persona.IdRol");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(e => e.IdRol)
                    .HasName("PK__Rol__2A49584C8ABB2593");

                entity.ToTable("Rol");

                entity.Property(e => e.DescripcionRol)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FechaIngresoRol)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.VigenciaRol).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Seccion>(entity =>
            {
                entity.HasKey(e => new { e.IdSeccion, e.CodigoAsignatura })
                    .HasName("PK__Seccion__295330A79B2D1186");

                entity.ToTable("Seccion");

                entity.Property(e => e.CodigoAsignatura).HasMaxLength(7);

                entity.Property(e => e.FechaIngresoSección)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.VigenciaSección).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.CodigoAsignaturaNavigation)
                    .WithMany(p => p.Seccions)
                    .HasForeignKey(d => d.CodigoAsignatura)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sección.CodigoAsignatura");

                entity.HasOne(d => d.MatriculaNavigation)
                    .WithMany(p => p.Seccions)
                    .HasForeignKey(d => d.Matricula)
                    .HasConstraintName("FK_Sección.Matricula");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
