using System;

namespace Excepciones
{
    public class PartidoException: Exception
    {
        public PartidoException() { }
        public PartidoException(string mensaje): base(mensaje) { }
        public PartidoException(string mensaje, Exception inner): base(mensaje, inner) { }
    }
}
