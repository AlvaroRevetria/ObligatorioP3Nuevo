using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

namespace DTOS
{
    public class DtoSeleccion
    {
        [Display(Name = "Nombre")]
        public string nombre { get; set; }
        [Display(Name = "Puntos")]
        public int puntos { get; set; }
        [Display(Name = "Goles a favor")]
        public int golesAFavor { get; set; }
        [Display(Name = "Goles en contra")]
        public int golesEnContra { get; set; }
        [Display(Name = "Diferencia de goles")]
        public int difGoles { get; set; }
    }
}
