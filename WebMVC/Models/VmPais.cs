using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Models
{
    public class VmPais
    {
        public int Id { get; set; }   
        public string Nombre { get; set; }       
        public string Codigo { get; set; }
        public int Pbi { get; set; }
        public int Poblacion { get; set; }
        public string Bandera { get; set; }        
        public int RegionId { get; set; }
    }
}
