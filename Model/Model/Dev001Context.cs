namespace Model.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Dev001Context : DbContext
    {
        public Dev001Context()
            : base("name=Dev001Context")
        {
        }

        public virtual DbSet<CURSO> CURSO { get; set; }
        public virtual DbSet<ESTUDIANTE> ESTUDIANTE { get; set; }
        public virtual DbSet<USUARIO> USUARIO { get; set; }
        public virtual DbSet<ESTUDIANTE_CURSO> ESTUDIANTE_CURSO { get; set; }
        public virtual DbSet<ESTUDIANTE_ARCHIVOS_ADJUNTOS> ESTUDIANTE_ARCHIVOS_ADJUNTOS { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           modelBuilder.Entity<ESTUDIANTE>()
                .HasMany(e => e.ESTUDIANTE_CURSO)
                .WithRequired(e => e.ESTUDIANTE)
                .HasForeignKey(e => e.ID_ESTUDIANTE)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ESTUDIANTE>()
                .HasMany(e => e.ESTUDIANTE_ARCHIVOS_ADJUNTOS)
                .WithRequired(e => e.ESTUDIANTE)
                .HasForeignKey(e => e.ID_ESTUDIANTE)
                .WillCascadeOnDelete(false);
        }
    }
}
