using LogicaAplicacion.InterfacesCasosUso.ICasosUsoPartido;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.CasosUso.CasosUsoPartido
{
    public class AltaPartido : IAltaPartido
    {
        public IRepositorioPartidos RepoPartidos { get; set; }

        public AltaPartido(IRepositorioPartidos repoPartido)
        {
            RepoPartidos = repoPartido;
        }
        public void Add(Partido obj)
        {
            try
            {
                obj.Validar();
                RepoPartidos.Add(obj);
            }
            catch (Exception e)
            {
                throw new Exception("No se pudo dar de alta el partido", e);
            }
        }
    }
}
