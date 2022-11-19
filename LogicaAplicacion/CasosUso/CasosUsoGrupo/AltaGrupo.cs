using LogicaAplicacion.InterfacesCasosUso.ICasosUsoGrupos;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.CasosUso
{
    public class AltaGrupo : IAltaGrupo
    {
        public IRepositorioGrupos RepoGrupos { get; set; }

        public AltaGrupo(IRepositorioGrupos repoGrupos)
        {
            RepoGrupos = repoGrupos;
        }

        public void Add(Grupo nuevo)
        {
            try
            {
                nuevo.Validar();
                RepoGrupos.Add(nuevo);
            }
            catch (Exception e)
            {
                throw new Exception("No se pudo dar de alta el grupo", e);
            }
        }
    }
}
