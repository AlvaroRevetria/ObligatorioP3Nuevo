using LogicaNegocio.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Models
{
    public class SeleccionViewModel
    {
        public Seleccion seleccion { get; set; }
        public IEnumerable<Pais> paises { get; set; }
        public IEnumerable<Grupo> grupos { get; set; }       
        public int IdPaisSeleccionado { get; set; }
        public int IdGrupoSeleccionado { get; set; }
       

    }
}
