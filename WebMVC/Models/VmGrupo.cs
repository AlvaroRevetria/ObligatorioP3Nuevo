using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Models
{
    public class VmGrupo
    {
        public int Id { get; set; }
        [Display(Name = "Grupo")]
        public string Nombre { get; set; }
    }
}
