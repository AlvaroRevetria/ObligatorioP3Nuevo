using LogicaAplicacion.InterfacesCasosUso.ICasosUsoPartido;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.CasosUso.CasosUsoPartido
{
    public class BajaPartido : IBajaPartido
    {
        public IRepositorioPartidos RepoPartidos { get; set; }

        public BajaPartido(IRepositorioPartidos repoPartidos)
        {
            RepoPartidos = repoPartidos;
        }
        public void Remove(int id)
        {

            try
            {
                //Validar que no se encuentre vinculado con ningun grupo o seleccion. 
                RepoPartidos.Remove(id);
            }
            catch (Exception e)
            {
                throw new Exception("No se pudo borar el Partido", e);
            }
        }
    }
}
