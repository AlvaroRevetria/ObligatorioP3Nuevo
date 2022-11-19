using LogicaAplicacion.InterfacesCasosUso.ICasosUsoGrupos;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.CasosUso
{
    public class ObtenerGrupos : IObtenerGrupos
    {
        public IRepositorioGrupos RepoGrupos { get; set; }

        public ObtenerGrupos(IRepositorioGrupos repoGrupos)
        {
            RepoGrupos = repoGrupos;
        }

       

        public IEnumerable<Grupo> FindAll()
        {
            try
            {
               return RepoGrupos.FindAll();
            }
            catch (Exception e)
            {
                throw new Exception("No se pudieron obtener los grupos", e);
            }
        }
    }
}
