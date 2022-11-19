using System;
using System.Collections.Generic;
using System.Text;

namespace Excepciones
{
    public class PaisException: Exception
    {
        public PaisException() { }
        public PaisException(string mensaje) : base(mensaje) { }
        public PaisException(string mensaje, Exception inner) : base(mensaje, inner) { }

    }
}
