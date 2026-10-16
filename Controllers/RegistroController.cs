using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
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

       
        /// Registro de la información 
        [HttpPost]
        public JsonResult registro(registro_tarea tarea)
        {
           

            if (tarea == null || string.IsNullOrWhiteSpace(tarea.Titulo) || string.IsNullOrWhiteSpace(tarea.Descripcion) || tarea.Duracion <= 0)
            {
                return Json(new
                {
                    validacion = false,
                    mensaje = "Por favor, ingrese los datos correctamente"
                });

            }

            List<registro_tarea> tareas = lista_tareas();
            int registroId = tareas.Count == 0 ? 1 : tareas.Max(t => t.Id) + 1;
            tarea.Id = registroId;
            tarea.Fecha_registro = DateTime.Now;
            tarea.Estado = false;

            tareas.Add(tarea);
            Session["bloque_tareas"] = tareas;

            return Json(new
            {

                validacion = true,
                mensaje = "La tarea se ha registrado correctamemte"


            });
        }

        /// Mostrar la información en la tabla
        [HttpGet]
        public JsonResult mostrar_lista()
        {
            List<registro_tarea> tareas = lista_tareas();
            var seleccion = tareas.Select(t => new { 
                t.Id,
                t.Titulo,
                t.Descripcion,
                Fecha_registro = t.Fecha_registro.ToString("dd/MM/yyyy"),
                t.Duracion,
                t.Estado
           
            }).ToList();

            return Json(seleccion, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult cambiar_estado(int id, bool estado)
        {
            List<registro_tarea> tareas = lista_tareas();
            registro_tarea _Tarea = tareas.FirstOrDefault(t => t.Id == id);
            if (_Tarea == null)
            {
                return Json(new
                {
                    validacion = false,
                    mensaje = "No se encontró la infomación"
                });
                
            }

            _Tarea.Estado = estado;
            Session["bloque_tareas"] = tareas;

            return Json(new
            {
                validacion = true,
                mensaje = "El estado de la tarea se actualizó correctamente"
            });
        }

        [HttpPost]
        public JsonResult editar_tarea(registro_tarea tarea)
        {
            if (tarea == null || tarea.Id <= 0 || string.IsNullOrWhiteSpace(tarea.Titulo) || string.IsNullOrWhiteSpace(tarea.Descripcion) || tarea.Duracion <= 0)
            {
                return Json(new
                {

                    validacion = false,
                    mensaje = "Por favor, ingrese los datos correctamente"
                });
                
            }
            List<registro_tarea> tareas = lista_tareas();
            registro_tarea tEdicion = tareas.FirstOrDefault(t => t.Id == tarea.Id);
            if (tEdicion == null)
            {
                return Json(new
                {
                    validacion = false,
                    mensaje = "No se encontró la infomación"
                });

            }

            tEdicion.Titulo = tarea.Titulo;
            tEdicion.Descripcion = tarea.Descripcion;
            tEdicion.Duracion = tarea.Duracion;

            Session["bloque_tareas"] = tareas;

            return Json(new
            {
                validacion = true,
                mensaje = "El estado de la tarea se actualizó correctamente"
            });

        }

        [HttpPost]
        public JsonResult eliminar_tarea(int id)
        {
            
            List<registro_tarea> tareas = lista_tareas();
            registro_tarea tElimina = tareas.FirstOrDefault(t => t.Id == id);
            if (tElimina == null)
            {
                return Json(new
                {
                    validacion = false,
                    mensaje = "No se encontró la infomación"
                });

            }

            tareas.Remove(tElimina);

            Session["bloque_tareas"] = tareas;

            return Json(new
            {
                validacion = true,
                mensaje = "La tarea se eliminó correctamente"
            });

        }
    }


}