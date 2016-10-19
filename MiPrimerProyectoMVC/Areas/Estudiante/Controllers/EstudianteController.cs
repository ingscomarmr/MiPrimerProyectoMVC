using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MiPrimerProyectoMVC.Tags;
using MiPrimerProyectoMVC.Utils;
using Model.Services;
using Model.Model;

namespace MiPrimerProyectoMVC.Areas.Estudiante.Controllers
{
    [Autenticado]
    public class EstudianteController : Controller
    {
        EstudianteService estuService = new EstudianteService();
        // GET: Estudiante/Estudiante
        public ActionResult Index()
        {
            int id = SessionUtils.GetUser();
            Console.WriteLine("Usuario de sessio:{0}", id);
            ESTUDIANTE est = estuService.GetEstudiante(id);
            return View(est);
        }
    }
}