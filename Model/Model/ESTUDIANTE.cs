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
        //la sequence se tuvo que meter por medio de un trigger
        public decimal ID { get; set; }

        [Required]
        [StringLength(100)]
        public string NOMBRE { get; set; }

        [Required]
        [StringLength(100)]
        public string APELLIDO { get; set; }

        [StringLength(100)]
        public string EMAIL { get; set; }

        public decimal EDAD { get; set; }

        public DateTime? FECHA_NACIMIENTO { get; set; }

        public decimal SEXO { get; set; }

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
        public static void SaveEstudiante(ESTUDIANTE e)
        {
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
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error SaveEstudiante:{0}", ex.Message);
                throw;
            }
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
                    e = ctx.ESTUDIANTE.Where(x => x.ID == id).SingleOrDefault();
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
