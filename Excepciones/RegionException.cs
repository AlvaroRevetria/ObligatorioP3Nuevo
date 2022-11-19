using System;
using System.Collections.Generic;
using System.Text;

namespace Excepciones
{
    public class RegionException: Exception
    {
        public RegionException() { }
        public RegionException(string mensaje) : base(mensaje) { }
        public RegionException(string mensaje, Exception inner) : base(mensaje, inner) { }

    }
}
