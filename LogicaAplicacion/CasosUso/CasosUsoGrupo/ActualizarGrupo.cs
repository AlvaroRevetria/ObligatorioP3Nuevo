using LogicaAplicacion.InterfacesCasosUso.ICasosUsoGrupos;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.CasosUso
{
    public class ActualizarGrupo: IActualizarGrupo
    {
        public IRepositorioGrupos RepoGrupos { get; set; }

        public ActualizarGrupo(IRepositorioGrupos repoGrupos)
        {
            RepoGrupos = repoGrupos;
        }

        public void Update(Grupo nuevo)
        {
            try
            {
                nuevo.Validar();
                RepoGrupos.Update(nuevo);
            }
            catch (Exception e)
            {
                throw new Exception("No se actualizar el grupo", e);
            }
        }
    }
}
