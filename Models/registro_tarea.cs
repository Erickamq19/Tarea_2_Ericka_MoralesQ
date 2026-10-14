using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tarea_2_Ericka_MoralesQ.Models
{
    public class registro_tarea
    {

        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha_registro { get; set; }
        public int Duracion { get; set; }
        public bool Estado { get; set; }

    }
}