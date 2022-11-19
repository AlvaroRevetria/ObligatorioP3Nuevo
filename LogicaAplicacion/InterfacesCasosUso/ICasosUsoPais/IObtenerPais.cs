using LogicaNegocio.Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.InterfacesCasosUso.ICasosUsoPais
{
    public interface IObtenerPais:IObtenerObjeto<Pais>
    {
       
        void ObtenerPaisPorCodAlfa3(string codAlfa);
    }
}
