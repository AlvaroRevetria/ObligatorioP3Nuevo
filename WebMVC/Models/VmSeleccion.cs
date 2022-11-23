using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace WebMVC.Models
{
    public class VmSeleccion
    {
        public int Id { get; set; }
        public VmPais Pais { get; set; }
        public int PaisId { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        [Display(Name = "Apostadores")]
        public int CantPotencialApostadores { get; set; }
        public int Puntos { get; set; }
        public VmGrupo Grupo { get; set; }
        public int GrupoId { get; set; }
    }
}
