using LogicaAplicacion.InterfacesCasosUso.ICasosUsoSeleccion;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.CasosUso.CasosUsoSeleccion
{
    class BajaSeleccion: IBajaSeleccion
    {
        public IRepositorioSelecciones RepoSelecciones { get; set; }

        public BajaSeleccion(IRepositorioSelecciones repoSelecciones)
        {
            RepoSelecciones = repoSelecciones;
        }

        public void Remove(int id)
        {
            try
            {
                //Validar vinculos antes de borrar. 
                RepoSelecciones.Remove(id);
            }
            catch (Exception e)
            {
                throw new Exception("No se pudo borar la seleccion", e);
            }
        }
    }
}
