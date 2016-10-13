using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiPrimerProyectoMVC.Areas.Estudiante.Controllers
{
    public class LoginController : Controller
    {
        // GET: Estudiante/Login
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult GoLogin(string email, string pwd)
        {
            Model.ResponseModel response = new Model.ResponseModel();
            try
            {
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(pwd)) {
                    throw new Exception("Algunos datos como usuario y password son necesarios");
                }

                pwd = Utils.Encrypt.MD5(pwd);

                Model.Model.ESTUDIANTE est = new Model.Model.ESTUDIANTE();
                est = Model.Model.ESTUDIANTE.GetEstudiante(email, pwd);

                if (est == null || est.ID <= 0)
                {
                    response.response = false;
                    response.message = "Usuario(" + email + ") no encontrado ";
                }
                else {
                    response.response = true;
                    response.message = "Login Ok para " + email;
                }

                                            
            }
            catch (Exception ex) {
                response.response = false;
                response.message = ex.Message;
            }
            return Json(response);
        }

        public JsonResult GoLogout(Model.Model.ESTUDIANTE e)
        {
            Model.ResponseModel response = new Model.ResponseModel();
            try
            {
                response.response = true;
                response.message = "Login Ok";
            }
            catch (Exception ex)
            {
                response.response = false;
                response.message = "Error:" + ex.Message;
            }
            return Json(response);
        }

    }
}