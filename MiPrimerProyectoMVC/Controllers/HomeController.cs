using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiPrimerProyectoMVC.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        //localhost:xxxx/home/index
        public ActionResult Index()
        {
            return View();
        }

        //localhost:xxxx/Home/Estudiantes
        public ActionResult Estudiantes()
        {
            return View();
        }
    }
}