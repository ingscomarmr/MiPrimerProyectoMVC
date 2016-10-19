using Model.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Services
{
    public class EstudianteService
    {
        #region GetEstudianteList
        public List<ESTUDIANTE> GetEstudianteList()
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
        public ResponseModel SaveEstudiante(ESTUDIANTE e)
        {
            var rm = new ResponseModel();
            try
            {
                using (var ctx = new Dev001Context())
                {
                    if (e.ID > 0)//usuario que ya existe
                    {
                        //intenta guardar sin pasar por la validacion de los campos requeridos
                        ctx.Configuration.ValidateOnSaveEnabled = false;
                        //le decimos con el EntityState que se modifico y lo debe de updatear
                        var estu = ctx.Entry(e);
                        estu.State = EntityState.Modified;
                        if (string.IsNullOrEmpty(e.PWD))
                        {
                            //excluye un campo de ser modificado
                            estu.Property(x => x.PWD).IsModified = false;
                        }
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
        public void DeleteEstudiante(int id)
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
        public ESTUDIANTE GetEstudiante(int id)
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
        public ESTUDIANTE GetEstudiante(string email, string pwd)
        {
            ESTUDIANTE e = new ESTUDIANTE();
            try
            {
                using (var ctx = new Dev001Context())
                {
                    e = ctx.ESTUDIANTE
                        .Where(x => x.EMAIL == email && x.PWD == pwd).SingleOrDefault();
                    Console.WriteLine("Estudiante id:{0}", e.ID);
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

        #region GetEstudianteID...
        public string GetNombreEstudiante(int id)
        {
            string nombre = "";
            try
            {
                using (var ctx = new Dev001Context())
                {
                    nombre = ctx.ESTUDIANTE
                        .Where(x => x.ID == id)
                        .Select(x => x.NOMBRE + " " + x.APELLIDO)
                        .SingleOrDefault();

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error DeleteEstudiante:{0}", ex.Message);
                throw;
            }
            return nombre;
        }
        #endregion
    }
}
