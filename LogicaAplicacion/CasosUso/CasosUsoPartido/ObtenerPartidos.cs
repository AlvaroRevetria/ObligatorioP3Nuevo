using LogicaAplicacion.InterfacesCasosUso.ICasosUsoPartido;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.CasosUso.CasosUsoPartido
{
    public class ObtenerPartidos : IObtenerPartidos
    {
        public IRepositorioPartidos RepoPartidos { get; set; }

        public ObtenerPartidos(IRepositorioPartidos repoPartidos)
        {
            RepoPartidos = repoPartidos;
        }
       
        public IEnumerable<Partido> FindAll()
        {
            try
            {
               return RepoPartidos.FindAll();
            }
            catch (Exception e)
            {
                throw new Exception("No se pudieron obtener los partidos", e);
            }
        }
    }
}
