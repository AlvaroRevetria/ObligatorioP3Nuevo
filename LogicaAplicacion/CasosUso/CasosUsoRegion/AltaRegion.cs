using LogicaAplicacion.InterfacesCasosUso.ICasosUsoRegion;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.CasosUso.CasosUsoRegion
{
     public class AltaRegion : IAltaRegion
    {
        public IRepositorioRegiones RepoRegiones { get; set; }

        public AltaRegion(IRepositorioRegiones repoRegiones)
        {
            RepoRegiones = repoRegiones;
        }


        public void Add(Region obj)
        {
            try
            {
                obj.Validar();
                RepoRegiones.Add(obj);
            }
            catch (Exception e)
            {
                throw new Exception("No se pudo dar de alta la region", e);
            }
        }
    }
}
