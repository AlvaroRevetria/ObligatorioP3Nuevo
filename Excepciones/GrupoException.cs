using System;
using System.Collections.Generic;
using System.Text;

namespace Excepciones
{
    public class GrupoException: Exception
    {
        public GrupoException() { }
        public GrupoException(string mensaje) : base(mensaje) { }
        public GrupoException(string mensaje, Exception inner) : base(mensaje, inner) { }

    }
}
