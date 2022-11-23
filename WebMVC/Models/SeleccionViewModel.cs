using LogicaNegocio.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Models
{
    public class SeleccionViewModel
    {
        public VmSeleccion seleccion { get; set; }
        public IEnumerable<VmPais> paises { get; set; }
        public IEnumerable<VmGrupo> grupos { get; set; }       
        public int IdPaisSeleccionado { get; set; }
        public int IdGrupoSeleccionado { get; set; }
       

    }
}
