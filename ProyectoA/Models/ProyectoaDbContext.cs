namespace ProyectoA.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ProyectoaDbContext : DbContext
    {
        public ProyectoaDbContext()
            : base("name=ProyectoaDbContext")
        {
        }

        public virtual DbSet<ARTICULO> ARTICULO { get; set; }
        public virtual DbSet<USUARIOS> USUARIOS { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ARTICULO>()
                .Property(e => e.TITULO)
                .IsUnicode(false);

            modelBuilder.Entity<ARTICULO>()
                .Property(e => e.ID_USUARIO)
                .IsUnicode(false);

            modelBuilder.Entity<ARTICULO>()
                .Property(e => e.IMAGEN1)
                .IsUnicode(false);

            modelBuilder.Entity<ARTICULO>()
                .Property(e => e.IMAGEN2)
                .IsUnicode(false);

            modelBuilder.Entity<ARTICULO>()
                .Property(e => e.IMAGEN3)
                .IsUnicode(false);

            modelBuilder.Entity<ARTICULO>()
                .Property(e => e.DESCRIPCION)
                .IsUnicode(false);

            modelBuilder.Entity<USUARIOS>()
                .Property(e => e.ID_USUARIO)
                .IsUnicode(false);

            modelBuilder.Entity<USUARIOS>()
                .Property(e => e.PASS)
                .IsUnicode(false);

            modelBuilder.Entity<USUARIOS>()
                .Property(e => e.NOMBRE)
                .IsUnicode(false);

            modelBuilder.Entity<USUARIOS>()
                .Property(e => e.APELLIDO)
                .IsUnicode(false);

            modelBuilder.Entity<USUARIOS>()
                .Property(e => e.CORREO)
                .IsUnicode(false);

            modelBuilder.Entity<USUARIOS>()
                .Property(e => e.ADMINISTRADOR)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<USUARIOS>()
                .Property(e => e.CIUDAD)
                .IsUnicode(false);
        }
    }
}
