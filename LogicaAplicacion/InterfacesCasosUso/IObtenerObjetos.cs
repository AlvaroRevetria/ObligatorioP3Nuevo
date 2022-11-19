using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.InterfacesCasosUso
{
    public interface IObtenerObjetos<T>
    {
        IEnumerable<T> FindAll();
    }
}
