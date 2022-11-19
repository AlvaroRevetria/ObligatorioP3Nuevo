using LogicaAplicacion.InterfacesCasosUso;
using LogicaAplicacion.InterfacesCasosUso.ICasosUsoPartido;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.CasosUso.CasosUsoPartido
{
    public class ObtenerPartido : IObtenerPartido
    {

        public IRepositorioPartidos RepoPartidos { get; set; }

        public ObtenerPartido(IRepositorioPartidos repoPartidos)
        {
            RepoPartidos = repoPartidos;
        }

        Partido IObtenerObjeto<Partido>.FindById(int id)
        {
            try
            {
                return RepoPartidos.FindById(id);
            }
            catch (Exception e) { throw new Exception("No se pudo obter el partido con el id indicado", e); }
        }
    }
}
