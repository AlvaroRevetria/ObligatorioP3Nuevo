using LogicaAplicacion.InterfacesCasosUso.ICasosUsoRegion;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.CasosUso.CasosUsoRegion
{
     public class ObtenerRegiones : IObtenerRegiones
    {
        public IRepositorioRegiones RepoRegiones { get; set; }

        public ObtenerRegiones(IRepositorioRegiones repoRegiones)
        {
            RepoRegiones = repoRegiones;
        }      

        public IEnumerable<Region> FindAll()
        {
            try
            {
               return RepoRegiones.FindAll();
            }
            catch (Exception e)
            {
                throw new Exception("No se pudieron obtener las regiones", e);
            }
        }
    }
}
