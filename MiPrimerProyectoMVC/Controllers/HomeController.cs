using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Model;
using System.IO;
using Model.Services;

namespace MiPrimerProyectoMVC.Controllers
{
    public class HomeController : Controller
    {
        EstudianteService estuService = new EstudianteService();
        // GET: Home 
        //localhost:xxxx/home/index
        public ActionResult Index()
        {
            ViewBag.Datos = "Mis Datos desde el controller";
            return View();
        }

        //localhost:xxxx/Home/Estudiantes
        public ActionResult Estudiantes()
        {
            ViewBag.EstudiantesList = estuService.GetEstudianteList();
            return View();
        }

        public ActionResult EstudiantesVer(int id)
        {
            if (id == 0)
            {
                return Redirect("~/home/Estudiantes"); //retorna
            }            
            return View(estuService.GetEstudiante(id));            
        }

        public ActionResult EstudianteEditar(int id, string mensage)
        {
            /*if (id == 0)
            {
                return Redirect("~/home/Estudiantes"); //retorna
            }*/
            ViewBag.Mensage = mensage;
            return View(id==0? new ESTUDIANTE() : estuService.GetEstudiante(id));
        }

        public ActionResult EstudianteGuardar(ESTUDIANTE e)
        {
            if (ModelState.IsValid) //valida si el modelo es correcto
            {
                estuService.SaveEstudiante(e); //Guarda el estudiante
                //return Redirect("~/home/EstudianteEditar?id=" + e.ID + "&mensage=Datos Guardados"); //retorna con el mismo ID
                return Redirect("~/home/Estudiantes"); //retorna
            }
            else {
                return View("~/views/home/EstudianteEditar.cshtml", e); //retorna
            }
            
        }

        //para usar ajax se cambia el tipo de vista a JsonResult
        public JsonResult EstudianteGuardarAjax(ESTUDIANTE e)
        {
            var rm = new Model.ResponseModel();

            if (ModelState.IsValid) //valida si el modelo es correcto
            {
                rm = estuService.SaveEstudiante(e); //Guarda el estudiante                
                if (rm.response)
                {
                    rm.message = "Se guardo correctamente la información!!";
                    rm.href = Url.Content("~/home/Estudiantes"); //a donde queremos que redireccione
                }
            }
            else {
                rm.SetResponse(false, "Datos no validos!!!"); //si tenemos un mensaje
            }

            return Json(rm);
        }

        public ActionResult EstudianteEliminar(int id)
        {
            if (id <= 0) {
                //no tiene nada que eliminar
                return Redirect("~/home/Estudiantes"); //retorna
            }
            estuService.DeleteEstudiante(id); //Eliminar el estudiante
            return Redirect("~/home/Estudiantes"); //retorna
        }

        //vista parcial para mostrar los cursos
        public PartialViewResult CursosAlumno(int id_estudiante)
        {
            //listar todos los cursos disponibles
            ViewBag.Cursos = Model.Model.CURSO.GetCursos(id_estudiante);
            //listar cursos que tiene el usuario
            ViewBag.CursosEstudiante = Model.Model.ESTUDIANTE_CURSO.GetEstudianteCursos(id_estudiante);

            ESTUDIANTE_CURSO es_cur = new ESTUDIANTE_CURSO();
            es_cur.ID_ESTUDIANTE = id_estudiante;
            return PartialView(es_cur);
        }

        public JsonResult GuardarCurso(ESTUDIANTE_CURSO ec) {
            var rm = new Model.ResponseModel();
            if (ModelState.IsValid)
            {
                rm = ec.Guardar();
                if (rm.response)
                {
                    rm.message = "Se guardo correctamente la información!!";
                    rm.function = "CargarCursos()"; //mandara a ejecutar una funcion javascript
                }
            }
            else {
                rm.message = "Datos no validos";
            }
           
            return Json(rm);
        }

        #region localhost:xxxx/Home/AdjuntosEstudiante?id_estudiante=0
        //localhost:xxxx/Home/AdjuntosEstudiante?id_estudiante=0
        public PartialViewResult AdjuntosEstudiante(int id_estudiante = 0)
        {
            //obtener archivos adjuntos
            ViewBag.AdjuntosList = ESTUDIANTE_ARCHIVOS_ADJUNTOS.GetArchivos(id_estudiante);

            //inicializar modelo
            ESTUDIANTE_ARCHIVOS_ADJUNTOS eaa = new ESTUDIANTE_ARCHIVOS_ADJUNTOS();
            eaa.ID_ESTUDIANTE = id_estudiante;
            return PartialView(eaa);
        }
        #endregion

        #region localhost:xxxx/Home/GuardarAdjunto?model ESTUDIANTE_ARCHIVOS_ADJUNTOS
        public JsonResult GuardarAdjunto(ESTUDIANTE_ARCHIVOS_ADJUNTOS eaa, HttpPostedFileBase archivo)
        {
            var rm = new Model.ResponseModel();

           if (archivo != null)
            {
                //nombre del archivo
                string nombreArchivo = DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(archivo.FileName);
                //ruta donde se guarda el archivo
                archivo.SaveAs(Server.MapPath("~/uploads/" + nombreArchivo));
                eaa.NOMBRE_ARCHIVO = nombreArchivo;
                eaa.ID_ADJUNTOS = 0;
                if (eaa.Guardar())
                {
                    rm.response = true;
                    rm.message = "Se guardo correctamente el adjunto!!";
                    rm.function = "CargarAdjuntos()"; //mandara a ejecutar una funcion javascript
                }
                
            }
            else
            {
                rm.message = "Datos no validos, archivo no encontrado.";
            }

            return Json(rm);
        }
        #endregion
    }
}