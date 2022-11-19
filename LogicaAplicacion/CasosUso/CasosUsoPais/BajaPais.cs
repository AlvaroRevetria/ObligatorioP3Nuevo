using LogicaAplicacion.InterfacesCasosUso.ICasosUsoPais;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Text;
using Excepciones;

namespace LogicaAplicacion.CasosUso.CasosUsoPais
{
    public class BajaPais : IBajaPais
    {
        public IRepositorioPaises RepoPaises { get; set; }

        public BajaPais(IRepositorioPaises repoPaises)
        {
            RepoPaises = repoPaises;
        }

        public void Remove(int id)
        {
            try
            {
                RepoPaises.Remove(id);
            }
            catch (PaisException)
            {
                throw;
            }
            catch (Exception e) 
            {             
            throw new Exception("No se pudo borrar el país", e);
            }

        }
    }
}
