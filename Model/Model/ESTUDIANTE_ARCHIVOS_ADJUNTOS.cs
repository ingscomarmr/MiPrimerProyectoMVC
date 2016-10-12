
namespace Model.Model
{

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Data.Entity;
    using System.Linq;


    [Table("DEV_001.ESTUDIANTE_ARCHIVOS_ADJUNTOS")]
    public partial class ESTUDIANTE_ARCHIVOS_ADJUNTOS
    {
        [Key]              
        public decimal? ID_ADJUNTOS { get; set; }

        [Required]
        public decimal ID_ESTUDIANTE { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "No debe exceder los 100 caracteres")]
        public string NOMBRE_ARCHIVO { get; set; }        
        
        public virtual ESTUDIANTE ESTUDIANTE { get; set; }

        

        #region GetArchivos Estudiante
        public static List<ESTUDIANTE_ARCHIVOS_ADJUNTOS> GetArchivos(int id_estudiante) {
            List<ESTUDIANTE_ARCHIVOS_ADJUNTOS> archivos = new List<ESTUDIANTE_ARCHIVOS_ADJUNTOS>();
            try
            {
                using (var ctx = new Dev001Context()) {
                    archivos = ctx.ESTUDIANTE_ARCHIVOS_ADJUNTOS
                        .Where(x => x.ID_ESTUDIANTE == id_estudiante)
                        .ToList();
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                throw;
            }
            return archivos;
        }
        #endregion

        #region Guardar..
        public bool Guardar() {
            try
            {
                using (var ctx = new Dev001Context()) {                    
                    ctx.Entry(this).State = EntityState.Added;
                    return (ctx.SaveChanges() > 0);
                }
            }
            catch (Exception ex) {
                Console.WriteLine("Error en Guardar adjunto:{0}", ex.Message);
                throw;
            }
            
        }
        #endregion

        #region Eliminar...
        public bool Eliminar()
        {
            try
            {
                using (var ctx = new Dev001Context())
                {
                    ctx.Entry(this).State = EntityState.Deleted;
                    return (ctx.SaveChanges() > 0);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en Guardar adjunto:{0}", ex.Message);
                throw;
            }

        }
        #endregion
    }
}
