using System;
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
        public virtual DbSet<Carrera> Carreras { get; set; } = null!;
        public virtual DbSet<Literal> Literals { get; set; } = null!;
        public virtual DbSet<Persona> Personas { get; set; } = null!;
        public virtual DbSet<Rol> Rols { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AreaAcademica>(entity =>
            {
                entity.HasKey(e => e.CodigoArea)
                    .HasName("PK__AreaAcad__CF230A4541265BDC");

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
                    .HasName("PK__Asignatu__4783438FB0FC356E");

                entity.ToTable("Asignatura");

                entity.HasIndex(e => e.NombreAsignatura, "UQ__Asignatu__101CCCC5BEB8C6DB")
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

            modelBuilder.Entity<Carrera>(entity =>
            {
                entity.HasKey(e => e.CodigoCarrera)
                    .HasName("PK__Carrera__2D5445FCC69F5018");

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
                    .HasName("PK__Literal__7D8C2AD01A092671");

                entity.ToTable("Literal");

                entity.Property(e => e.Nota).HasMaxLength(2);

                entity.Property(e => e.Numero).HasColumnType("decimal(2, 1)");
            });

            modelBuilder.Entity<Persona>(entity =>
            {
                entity.HasKey(e => e.Matricula)
                    .HasName("PK__Persona__0FB9FB4E6764EF4D");

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
                    .HasName("PK__Rol__2A49584C9006282D");

                entity.ToTable("Rol");

                entity.Property(e => e.DescripcionRol)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FechaIngresoRol)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.VigenciaRol).HasDefaultValueSql("((1))");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
