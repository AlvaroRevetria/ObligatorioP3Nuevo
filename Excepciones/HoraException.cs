using System;

namespace Excepciones
{
    public class HoraException: Exception
    {
        public HoraException() { }
        public HoraException(string mensaje) : base(mensaje) { }
        public HoraException(string mensaje, Exception inner) : base(mensaje, inner) { }

    }
}
