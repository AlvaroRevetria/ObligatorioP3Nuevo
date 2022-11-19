using LogicaAplicacion.InterfacesCasosUso;
using LogicaAplicacion.InterfacesCasosUso.ICasosUsoSeleccion;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.CasosUso.CasosUsoSeleccion
{
     public class ObtenerSeleccion : IObtenerSeleccion
    {
        public IRepositorioSelecciones RepoSelecciones { get; set; }

        public ObtenerSeleccion(IRepositorioSelecciones repoSelecciones)
        {
            RepoSelecciones = repoSelecciones;        }


      
        Seleccion IObtenerObjeto<Seleccion>.FindById(int id)
        {
            try
            {
                 return RepoSelecciones.FindById(id);
            }
            catch (Exception e)
            {
                throw new Exception("No se pudo obtener la seleccion con el id indicado", e);
            }
        }
    }
}
