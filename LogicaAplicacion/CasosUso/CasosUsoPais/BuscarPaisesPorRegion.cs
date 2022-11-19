using LogicaAplicacion.InterfacesCasosUso.ICasosUsoPais;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.CasosUso.CasosUsoPais
{
    public class BuscarPaisesPorRegion : IBuscarPaisesPorRegion
    {
        public IRepositorioPaises RepoPaises { get; set; }

        public BuscarPaisesPorRegion(IRepositorioPaises repoPaises)
        {
            RepoPaises = repoPaises;
        }
        public IEnumerable<Pais> PaisesPorRegion(int regionId)
        {           
            return RepoPaises.PaisesPorRegion(regionId);          
            
        }
    }
}
