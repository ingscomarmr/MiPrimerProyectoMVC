using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MiPrimerProyectoMVC.Tags;
using MiPrimerProyectoMVC.Utils;

namespace MiPrimerProyectoMVC.Areas.Estudiante.Controllers
{
    [Autenticado]
    public class EstudianteController : Controller
    {
        // GET: Estudiante/Estudiante
        public ActionResult Index()
        {
            int id = SessionUtils.GetUser();
            Console.WriteLine("Usuario de sessio:{0}", id);
            Model.Model.ESTUDIANTE est = Model.Model.ESTUDIANTE.GetEstudiante(id);
            return View(est);
        }
    }
}