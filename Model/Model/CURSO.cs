namespace Model.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using System.Data.SqlClient;

    [Table("DEV_001.CURSO")]
    public partial class CURSO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CURSO()
        {
            ESTUDIANTE_CURSO = new HashSet<ESTUDIANTE_CURSO>();
        }

        [Key]
        public decimal ID_CURSO { get; set; }

        [Required]
        [StringLength(100)]
        public string NOMBRE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ESTUDIANTE_CURSO> ESTUDIANTE_CURSO { get; set; }

        public static List<CURSO> GetCursos(int EstudianteId = 0) {
            List<CURSO> cursosList = new List<CURSO>();
            try
            {
                using (var ctx = new Dev001Context()) {

                    if (EstudianteId <= 0) {
                        return cursosList = ctx.CURSO.ToList();
                    }
                    //por medio de Linq sacamos los cursos tomados 
                    var cursosTomados = ctx.ESTUDIANTE_CURSO
                                            .Where(x => x.ID_ESTUDIANTE == EstudianteId)
                                            .Select(x=> x.ID_CURSO)
                                            .ToList();

                    
                    cursosList = ctx.CURSO.Where(x=> !cursosTomados.Contains(x.ID_CURSO)).ToList();

                    //forma mas sencilla de obtener los cursos disponibles para el alumno por medio de un Query
                    //cursosList = ctx.Database.SqlQuery<CURSO>(@"SELECT C.ID_CURSO, C.NOMBRE 
                    //                                FROM CURSO C
                    //                                WHERE C.ID_CURSO NOT IN(
                    //                                    SELECT EC.ID_CURSO FROM ESTUDIANTE_CURSO EC WHERE EC.ID_ESTUDIANTE=@ID_ESTUDIANTE)", 
                    //                                    new SqlParameter("ID_ESTUDIANTE",EstudianteId)).ToList();
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                throw;
            }
            return cursosList;
        }
    }
}
