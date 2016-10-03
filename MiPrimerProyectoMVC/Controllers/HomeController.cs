﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Model;
namespace MiPrimerProyectoMVC.Controllers
{
    public class HomeController : Controller
    {
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
            ViewBag.EstudiantesList = ESTUDIANTE.GetEstudianteList();
            return View();
        }

        public ActionResult EstudiantesVer(int id)
        {
            if (id == 0)
            {
                return Redirect("~/home/Estudiantes"); //retorna
            }            
            return View(ESTUDIANTE.GetEstudiante(id));            
        }

        public ActionResult EstudianteEditar(int id, string mensage)
        {
            /*if (id == 0)
            {
                return Redirect("~/home/Estudiantes"); //retorna
            }*/
            ViewBag.Mensage = mensage;
            return View(id==0? new ESTUDIANTE() : ESTUDIANTE.GetEstudiante(id));
        }

        public ActionResult EstudianteGuardar(ESTUDIANTE e)
        {
            if (ModelState.IsValid) //valida si el modelo es correcto
            {
                ESTUDIANTE.SaveEstudiante(e); //Guarda el estudiante
                //return Redirect("~/home/EstudianteEditar?id=" + e.ID + "&mensage=Datos Guardados"); //retorna con el mismo ID
                return Redirect("~/home/Estudiantes"); //retorna
            }
            else {
                return View("~/views/home/EstudianteEditar.cshtml", e); //retorna
            }
            
        }

        public ActionResult EstudianteEliminar(int id)
        {
            if (id <= 0) {
                //no tiene nada que eliminar
                return Redirect("~/home/Estudiantes"); //retorna
            }
            ESTUDIANTE.DeleteEstudiante(id); //Eliminar el estudiante
            return Redirect("~/home/Estudiantes"); //retorna
        }
    }
}