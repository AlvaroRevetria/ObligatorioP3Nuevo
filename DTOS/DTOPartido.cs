using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DTOS
{
    public class DTOPartido
    {
        public DateTime Fecha { get; set; }
        public string Hora { get; set; }
        public string NombreSeleccion1 { get; set; }
        public int GolesSeleccion1 { get; set; }
        public string NombreSeleccion2 { get; set; }
        public int GolesSeleccion2 { get; set; }
        public string Estado { get; set; }
    }
}
