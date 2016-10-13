using System.Web.Mvc;

namespace MiPrimerProyectoMVC.Areas.Estudiante
{
    public class EstudianteAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Estudiante";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Estudiante_default",
                "Estudiante/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}