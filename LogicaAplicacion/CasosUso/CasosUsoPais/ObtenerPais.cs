using LogicaAplicacion.InterfacesCasosUso;
using LogicaAplicacion.InterfacesCasosUso.ICasosUsoPais;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.CasosUso.CasosUsoPais
{
    public class ObtenerPais : IObtenerPais
    {
        public IRepositorioPaises RepoPaises { get; set; }

        public ObtenerPais(IRepositorioPaises repoPaises)
        {
            RepoPaises = repoPaises;
        }

       

        public void ObtenerPaisPorCodAlfa3(string codAlfa)
        {
            try
            {
                RepoPaises.BuscarPorCodAlfa(codAlfa);
            }
            catch (Exception e)
            {
                throw new Exception("No se pudo obter el pais con el código alfa-3 indicado", e);
            }
        }

        Pais IObtenerObjeto<Pais>.FindById(int id)
        {
            try
            {
               return RepoPaises.FindById(id);
            }
            catch (Exception e)
            {
                throw new Exception("No se pudo obter el pais con el id indicado", e);
            }
        }
    }
}
