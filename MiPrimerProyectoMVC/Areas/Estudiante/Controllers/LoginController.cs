using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MiPrimerProyectoMVC.Tags;

namespace MiPrimerProyectoMVC.Areas.Estudiante.Controllers
{
    public class LoginController : Controller
    {
        // GET: Estudiante/Login
        public ActionResult Index()
        {
            return View();
        }

        [NoLogin] //parte que no se podra ver cuando ya esta en Login
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
                    Utils.SessionUtils.AddUserToSession(est.ID.ToString());
                    response.response = true;
                    response.message = "Espere un momento, se esta completado la operación...";
                    response.href = Url.Content("~/Estudiante/Estudiante/Index");//Server.MapPath("~/Estudiante");
                    
                    //response.message = "Login Ok para " + email;
                }

                                            
            }
            catch (Exception ex) {
                response.response = false;
                response.message = ex.Message;
            }
            return Json(response);
        }

        public ActionResult GoLogout(string id)
        {
            Utils.SessionUtils.DestroyUserSession();
            return Redirect("~/Estudiante/Login"); //retorna
        }

    }
}