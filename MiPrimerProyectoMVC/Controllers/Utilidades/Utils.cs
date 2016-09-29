using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiPrimerProyectoMVC.Controllers.Utilidades
{
    public class Utils
    {
        public static string Sexo(int v) {
            return (v == 0 ? "Mujer" : "Hombre"); 
        }
    }
}