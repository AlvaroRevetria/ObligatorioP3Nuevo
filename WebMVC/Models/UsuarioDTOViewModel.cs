using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Models
{
    public class UsuarioDTOViewModel
    {
        public DTOUsuarioViewModel usuario { get; set; }
        public IEnumerable<UsuarioRolDTOViewModel> UsuarioRol { get; set; }

        public IEnumerable<RolDTOViewModel> Roles { get; set; }

        public int IdRolSeleccionado { get; set; }

    }
}
