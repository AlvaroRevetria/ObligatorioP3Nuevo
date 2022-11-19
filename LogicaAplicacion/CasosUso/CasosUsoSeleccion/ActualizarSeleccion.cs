using LogicaAplicacion.InterfacesCasosUso.ICasosUsoSeleccion;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.CasosUso.CasosUsoSeleccion
{
    public class ActualizarSeleccion : IActualizarSeleccion
    {
        public IRepositorioSelecciones RepoSelecciones { get; set; }

        public ActualizarSeleccion(IRepositorioSelecciones repoSelecciones)
        {
            RepoSelecciones = repoSelecciones;
        }

        public void Update(Seleccion obj)
        {
            try
            {
                obj.Validar();
                RepoSelecciones.Update(obj);
            }
            catch (Exception e)
            {
                throw new Exception("No se pudo actualizar la seleccion", e);
            }
        }
    }
}
