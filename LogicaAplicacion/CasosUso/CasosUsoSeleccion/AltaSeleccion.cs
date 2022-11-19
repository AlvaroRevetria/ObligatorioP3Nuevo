using LogicaAplicacion.InterfacesCasosUso.ICasosUsoSeleccion;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.CasosUso.CasosUsoSeleccion
{
    class AltaSeleccion : IAltaSeleccion
    {
        public IRepositorioSelecciones RepoSelecciones { get; set; }

        public AltaSeleccion(IRepositorioSelecciones repoSelecciones)
        {
            RepoSelecciones = repoSelecciones;
        }


        public void Add(Seleccion seleccion)
        {
            try
            {
                RepoSelecciones.Add(seleccion);
            }
            catch (Exception e)
            {
                throw new Exception("No se pudo dar de alta la seleccion", e);
            }
        }
    }
}
