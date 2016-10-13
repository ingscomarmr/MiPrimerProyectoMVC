namespace Model.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using System.Data.Entity;

    [Table("DEV_001.ESTUDIANTE")]
    public partial class ESTUDIANTE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ESTUDIANTE()
        {
            ESTUDIANTE_CURSO = new HashSet<ESTUDIANTE_CURSO>();
            ESTUDIANTE_ARCHIVOS_ADJUNTOS = new HashSet<ESTUDIANTE_ARCHIVOS_ADJUNTOS>();
        }

        public decimal ID { get; set; }

        [Required(ErrorMessage ="Este datos es requerido")]
        [StringLength(100)]
        public string NOMBRE { get; set; }

        [Required(ErrorMessage = "Este datos es requerido")]
        [StringLength(100)]
        public string APELLIDO { get; set; }

        [Required(ErrorMessage = "Este datos es requerido, debe contener el formado de email, ejemplo@email.com")]
        [StringLength(100)]
        public string EMAIL { get; set; }

        [Required]
        public decimal EDAD { get; set; }

        [Required(ErrorMessage ="Es requerido, debe contener el formato YYYY-MM-DD")] //intro mensaje personalizado
        //[RegularExpression(@"\d\d\d\d-\d\d-\d\d",ErrorMessage ="Debe contener el formato YYYY-MM-DD")]
        public DateTime FECHA_NACIMIENTO { get; set; }

        [Required]
        [Range(0,1, ErrorMessage ="Debe seleccionar entre Mujer y Hombre")]
        public decimal SEXO { get; set; }

        [StringLength(100,ErrorMessage ="Solo permite 100 caracetes")]
        public string PWD { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ESTUDIANTE_CURSO> ESTUDIANTE_CURSO { get; set; }

        public virtual ICollection<ESTUDIANTE_ARCHIVOS_ADJUNTOS> ESTUDIANTE_ARCHIVOS_ADJUNTOS { get; set; }

        #region GetEstudianteList
        public static List<ESTUDIANTE> GetEstudianteList()
        {
            List<ESTUDIANTE> estudianteList = new List<ESTUDIANTE>();
            try
            {
                using (var ctx = new Dev001Context())
                {
                    estudianteList = ctx.ESTUDIANTE.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error GetEstudianteList:{0}", ex.Message);
                throw;
            }
            return estudianteList;
        }
        #endregion

        #region SaveEstudiante
        public static ResponseModel SaveEstudiante(ESTUDIANTE e)
        {
            var rm = new ResponseModel();
            try
            {
                using (var ctx = new Dev001Context())
                {
                    if (e.ID > 0)//usuario que ya existe
                    {
                        //le decimos con el EntityState que se modifico y lo debe de updatear
                        ctx.Entry(e).State = EntityState.Modified;
                    }
                    else
                    {//usuario nuevo
                        //le decimos con el EntityState que se inserte
                        ctx.Entry(e).State = EntityState.Added;
                    }
                    ctx.SaveChanges();
                    rm.SetResponse(true, "Datos guardados");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error SaveEstudiante:{0}", ex.Message);
                throw;
            }
            return rm;
        }
        #endregion

        #region DeleteEstudiante
        public static void DeleteEstudiante(int id)
        {
            try
            {
                using (var ctx = new Dev001Context())
                {
                    ctx.Entry(new ESTUDIANTE { ID = id }).State = EntityState.Deleted;
                    ctx.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error DeleteEstudiante:{0}", ex.Message);
                throw;
            }
        }
        #endregion

        #region GetEstudiante...
        public static ESTUDIANTE GetEstudiante(int id)
        {
            ESTUDIANTE e = new ESTUDIANTE();
            try
            {
                using (var ctx = new Dev001Context())
                {
                    e = ctx.ESTUDIANTE
                        .Include("ESTUDIANTE_CURSO") //incluir el objeto para que Entity lo incluya en su consulta
                        .Include("ESTUDIANTE_CURSO.CURSO") //incluir tambien el objeto del curso
                        .Where(x => x.ID == id).SingleOrDefault();
                } 

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error DeleteEstudiante:{0}", ex.Message);
                throw;
            }
            return e;
        }
        #endregion

        #region GetEstudiante email y pwd...
        public static ESTUDIANTE GetEstudiante(string email, string pwd)
        {
            ESTUDIANTE e = new ESTUDIANTE();
            try
            {
                using (var ctx = new Dev001Context())
                {                    
                    e = ctx.ESTUDIANTE
                        .Where(x => x.EMAIL == email && x.PWD == pwd).SingleOrDefault();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error DeleteEstudiante:{0}", ex.Message);
                throw;
            }
            return e;
        }
        #endregion
    }
}
