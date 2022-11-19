using LogicaNegocio.Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaNegocio.InterfacesRepositorios
{
   public  interface IRepositorioPaises:IRepositorio<Pais>
    {
        Pais BuscarPorCodAlfa(string codAlfa);
        IEnumerable<Pais> PaisesPorRegion(int regionId);
    }
}
