using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using LogicaNegocio.Dominio;

namespace LogicaAccesoDatos.BaseDatos
{
    public class LibreriaContext : DbContext
    {
        public DbSet<Pais> Paises { get; set; }
        public DbSet<Grupo> Grupos { get; set; }
        public DbSet<Partido> Partidos { get; set; }
        public DbSet<Region> Regiones { get; set; }
        public DbSet<Horario> Horarios { get; set; }
        public DbSet<Seleccion> Selecciones { get; set; }
        public DbSet<SeleccionPartido> SeleccionPartido { get; set; }
        public DbSet<PartidoGrupo> PartidoGrupo { get; set; }

        public LibreriaContext(DbContextOptions<LibreriaContext> opciones) : base(opciones)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Autor>().ToTable("Autor");
            modelBuilder.Entity<Pais>().HasIndex(p => p.Nombre).IsUnique(true);
            modelBuilder.Entity<Pais>().HasIndex(p => p.Codigo).IsUnique(true);
            modelBuilder.Entity<Seleccion>().HasIndex(s => s.PaisId).IsUnique(true);
            modelBuilder.Entity<Grupo>().HasIndex(g => g.Nombre).IsUnique(true);
            modelBuilder.Entity<Seleccion>().HasIndex(s => s.Telefono).IsUnique(true);
            modelBuilder.Entity<Seleccion>().HasIndex(s => s.Email).IsUnique(true);
            //modelBuilder.Entity<Autor>().Property(autor => autor.Documento).IsRequired(true);
            //modelBuilder.Entity<Autor>().Property(autor => autor.Nombre).IsRequired(true).HasMaxLength(50);
            modelBuilder.Entity<SeleccionPartido>().HasKey(sp => new { sp.PartidoId, sp.SeleccionId });
            modelBuilder.Entity<PartidoGrupo>().HasKey(PaG => new { PaG.PartidoId, PaG.GrupoId });
            //base.OnModelCreating(modelBuilder);
        }
    }
}
