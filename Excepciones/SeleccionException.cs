using System;
using System.Collections.Generic;
using System.Text;

namespace Excepciones
{
    public class SeleccionException: Exception
    {
        public SeleccionException() { }
        public SeleccionException(string mensaje) : base(mensaje) { }
        public SeleccionException(string mensaje, Exception inner) : base(mensaje, inner) { }
    }
}
