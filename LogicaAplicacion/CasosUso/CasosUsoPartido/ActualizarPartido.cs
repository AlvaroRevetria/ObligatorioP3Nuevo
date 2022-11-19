using LogicaAplicacion.InterfacesCasosUso.ICasosUsoPartido;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.CasosUso.CasosUsoPartido
{
    public class ActualizarPartido : IActualizarPartido
    {
        public IRepositorioPartidos RepoPartidos { get; set; }

        public ActualizarPartido(IRepositorioPartidos repoPartidos)
        {
            RepoPartidos = repoPartidos;
        }


        public void Update(Partido obj)
        {
            try
            {
                RepoPartidos.Update(obj);
            }
            catch (Exception e)
            {
                throw new Exception("No se pudo actualizar el Partido", e);
            }
        }
    }
}
