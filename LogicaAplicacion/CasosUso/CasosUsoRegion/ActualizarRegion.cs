using LogicaAplicacion.InterfacesCasosUso.ICasosUsoRegion;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.CasosUso.CasosUsoRegion
{
     public class ActualizarRegion : IActualizarRegion
    {
        public IRepositorioRegiones RepoRegiones { get; set; }

        public ActualizarRegion(IRepositorioRegiones repoRegiones)
        {
            RepoRegiones = repoRegiones;
        }

        public void Update(Region obj)
        {
            try
            {
                obj.Validar();
                RepoRegiones.Update(obj);
            }
            catch (Exception e)
            {
                throw new Exception("No se pudo actualizar la region", e);
            }
        }
    }
}
