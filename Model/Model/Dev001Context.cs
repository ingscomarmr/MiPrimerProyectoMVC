namespace Model.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Dev001Context : DbContext
    {
        public Dev001Context()
            : base("name=Dev001Context1")
        {
        }

        public virtual DbSet<ESTUDIANTE> ESTUDIANTE { get; set; }
        public virtual DbSet<USUARIO> USUARIO { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ESTUDIANTE>()
                .Property(e => e.ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<ESTUDIANTE>()
                .Property(e => e.NOMBRE)
                .IsUnicode(false);

            modelBuilder.Entity<ESTUDIANTE>()
                .Property(e => e.APELLIDO)
                .IsUnicode(false);

            modelBuilder.Entity<ESTUDIANTE>()
                .Property(e => e.EMAIL)
                .IsUnicode(false);

            modelBuilder.Entity<ESTUDIANTE>()
                .Property(e => e.EDAD)
                .HasPrecision(38, 0);

            modelBuilder.Entity<ESTUDIANTE>()
                .Property(e => e.SEXO)
                .HasPrecision(38, 0);

            modelBuilder.Entity<USUARIO>()
                .Property(e => e.ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<USUARIO>()
                .Property(e => e.USER_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<USUARIO>()
                .Property(e => e.PWD)
                .IsUnicode(false);

            modelBuilder.Entity<USUARIO>()
                .Property(e => e.ROL)
                .IsUnicode(false);
        }
    }
}
