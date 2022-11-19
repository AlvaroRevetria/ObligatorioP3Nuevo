using LogicaAplicacion.InterfacesCasosUso;
using LogicaAplicacion.InterfacesCasosUso.ICasosUsoRegion;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.CasosUso.CasosUsoRegion
{
   public class ObtenerRegion : IObtenerRegion
    {
        public IRepositorioRegiones RepoRegiones { get; set; }

        public ObtenerRegion(IRepositorioRegiones repoRegiones)
        {
            RepoRegiones = repoRegiones;       }

       
         Region IObtenerObjeto<Region>.FindById(int id)
        {
            try
            {
               return RepoRegiones.FindById(id);
            }
            catch (Exception e)
            {
                throw new Exception("No se pudo obtener la region con el id indicado", e);
            }
        }
    }
}
