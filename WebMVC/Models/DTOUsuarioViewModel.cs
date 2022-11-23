using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Models
{
    public class DTOUsuarioViewModel
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public List<UsuarioRolDTOViewModel> UsuarioRol { get; set; }
    }
}
