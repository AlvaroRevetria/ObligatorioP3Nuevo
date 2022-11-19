using LogicaNegocio.Dominio;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Models
{
    public class PaisViewModel
    {
        public IEnumerable<Region> Regiones { get; set; }
        public Pais Pais { get; set; }
        public int IdRegionSeleccionada { get; set; }
        public IFormFile Bandera { get; set; }
       
    }

}

