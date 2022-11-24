using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Models
{
    public class vmPartido
    {
        public int Id { get; set; }
        public VmSeleccion Seleccion1 { get; set; }
        public VmSeleccion Seleccion2 { get; set; }
        public DateTime Fecha { get; set; }
        public vmHorario Hora { get; set; }
        public string Estado { get; set; }

        public int GolesS1 { get; set; }
        public int GolesS2 { get; set; }
        public int CantidadRojasS1 { get; set; }
        public int CantidadRojasS2 { get; set; }
        public int CantidadAmarillasS1 { get; set; }
        public int CantidadAmarillasS2 { get; set; }
        public int CantidadRojasAcAmarillasS1 { get; set; }
        public int CantidadRojasAcAmarillasS2 { get; set; }
    }
}
