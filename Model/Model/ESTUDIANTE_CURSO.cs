namespace Model.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Data.Entity;
    using System.Linq;

    [Table("DEV_001.ESTUDIANTE_CURSO")]
    public partial class ESTUDIANTE_CURSO
    {
        [Key]
        [Column(Order = 0)]
        public decimal ID_ESTUDIANTE { get; set; }

        public decimal? ID_CURSO { get; set; }

        [Key]
        [Column(Order = 1)]
        [Required(ErrorMessage ="El puntaje es requerido")]
        [Range(0,10,ErrorMessage ="El puntaje minimo es 0 y el maximo es 10")]
        public decimal PUNTOS_CURSO { get; set; }

        public virtual CURSO CURSO { get; set; }

        public virtual ESTUDIANTE ESTUDIANTE { get; set; }

        public ResponseModel Guardar() {
            var rm = new ResponseModel();
            try
            {
                using (var ctx = new Dev001Context())
                {
                    ctx.Entry(this).State = EntityState.Added;
                    ctx.SaveChanges();
                    rm.SetResponse(true, "Datos guardados");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error SaveEstudiante:{0}", ex.Message);
                rm.SetResponse(false, ex.Message);
                throw;
            }
            return rm;
        }

        public ResponseModel Eliminar()
        {
            var rm = new ResponseModel();
            try
            {
                using (var ctx = new Dev001Context())
                {
                    ctx.Entry(this).State = EntityState.Deleted;
                    ctx.SaveChanges();
                    rm.SetResponse(true, "Datos eliminados");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error SaveEstudiante:{0}", ex.Message);
                rm.SetResponse(false, ex.Message);
                throw;
            }
            return rm;
        }

        #region GetEstudianteList
        public static List<ESTUDIANTE_CURSO> GetEstudianteCursos(int EstudianteId)
        {
            var estudianteCursos = new List<ESTUDIANTE_CURSO>();
            try
            {
                using (var ctx = new Dev001Context())
                {
                    estudianteCursos = ctx.ESTUDIANTE_CURSO
                        .Include("CURSO")
                        .Where(x=> x.ID_ESTUDIANTE == EstudianteId)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error GetEstudianteList:{0}", ex.Message);
                throw;
            }
            return estudianteCursos;
        }
        #endregion
    }
}
