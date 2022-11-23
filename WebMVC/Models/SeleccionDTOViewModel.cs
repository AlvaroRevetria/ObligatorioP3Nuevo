using LogicaNegocio.Dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Models
{
    public class SeleccionDTOViewModel
    {
        [Display(Name = "Seleccion")]
        public string Nombre { get; set; }
        public string Bandera { get; set; }
        public int Puntos { get; set; }
        [Display(Name = "GF")]
        public int GolesAFavor { get; set; }
        [Display(Name = "GC")]
        public int GolesEnContra { get; set; }
        [Display(Name = "GF")]
        public int DifGoles { get; set; }
    }
}
