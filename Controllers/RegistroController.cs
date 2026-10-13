using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tarea_2_Ericka_MoralesQ.Models;


namespace Tarea_2_Ericka_MoralesQ.Controllers
{
    public class RegistroController : Controller
    {
  
        private List<registro_tarea> lista_tareas()
        {
            if (Session["bloque_tareas"] == null)
            {
                Session["bloque_tareas"] = new List<registro_tarea>();
            }

            return (List < registro_tarea >)Session["bloque_tareas"];

        }
       
        public ActionResult Index()
        {
            return View();
        }
    }
}