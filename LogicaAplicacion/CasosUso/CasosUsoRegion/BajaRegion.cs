using LogicaAplicacion.InterfacesCasosUso.ICasosUsoRegion;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.CasosUso.CasosUsoRegion
{
   public class BajaRegion : IBajaRegion
    {
        public IRepositorioRegiones RepoRegiones { get; set; }

        public BajaRegion(IRepositorioRegiones repoRegiones)
        {
            RepoRegiones = repoRegiones;
        }

        public void Remove(int id)
        {
            try
            {
                //Validar que no se encuentre vinculado con ninguna seleccion. 
                RepoRegiones.Remove(id);
            }
            catch (Exception e)
            {
                throw new Exception("No se pudo borrar la region", e);
            }
        }
    }
}
