using System;
using System.Collections.Generic;
using System.Text;

namespace DTOS
{
     public class DTOUsuario
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public List<DTOUsuarioRol> UsuarioRol { get; set; }
    }
}
