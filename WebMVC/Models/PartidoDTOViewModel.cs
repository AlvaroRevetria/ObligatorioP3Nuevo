﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Models
{
    public class PartidoDTOViewModel
    {
        public DateTime Fecha { get; set; }
        public string Hora { get; set; }
        [Display(Name = "Seleccion")]
        public string NombreSeleccion1 { get; set; }
        [Display(Name = "Goles")]
        public int GolesSeleccion1 { get; set; }
        [Display(Name = "Seleccion")]
        public string NombreSeleccion2 { get; set; }
        [Display(Name = "Goles")]
        public int GolesSeleccion2 { get; set; }
        public string Estado { get; set; }
    }
}
