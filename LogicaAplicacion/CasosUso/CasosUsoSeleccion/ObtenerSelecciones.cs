using LogicaAplicacion.InterfacesCasosUso.ICasosUsoSeleccion;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.CasosUso.CasosUsoSeleccion
{
     public class ObtenerSelecciones : IObtenerSelecciones
    {
        public IRepositorioSelecciones RepoSelecciones { get; set; }

        public ObtenerSelecciones(IRepositorioSelecciones repoSelecciones)
        {
            RepoSelecciones = repoSelecciones;
        }

       

        public IEnumerable<Seleccion> FindAll()
        {
            try
            {
               return RepoSelecciones.FindAll();
            }
            catch (Exception e)
            {
                throw new Exception("No se pudieron obtener las selecciones", e);
            }
        }
    }
}
