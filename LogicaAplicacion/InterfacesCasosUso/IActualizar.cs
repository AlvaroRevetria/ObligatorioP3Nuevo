using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.InterfacesCasosUso
{
   public interface IActualizar<T>
    {
        void Update(T obj);
    }
}
