using LogicaAplicacion.InterfacesCasosUso.ICasosUsoPais;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.CasosUso.CasosUsoPais
{
    public class BuscarPaisPorCodAlfa: IBuscarPaisPorCodigoAlfa
    {
        public IRepositorioPaises RepoPaises { get; set; }

        public BuscarPaisPorCodAlfa(IRepositorioPaises repoPaises)
        {
            RepoPaises = repoPaises;
        }

        public Pais BuscarPorCodAlfa(string codAlfa)
        {
            return RepoPaises.BuscarPorCodAlfa(codAlfa);
        }
    }
}
