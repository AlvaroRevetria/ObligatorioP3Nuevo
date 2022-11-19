using LogicaAplicacion.InterfacesCasosUso.ICasosUsoGrupos;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.CasosUso.CasosUsoPais
{
    public class BajaGrupo : IBajaGrupo
    {
        public IRepositorioGrupos RepoGrupos { get; set; }

        public BajaGrupo(IRepositorioGrupos repoGrupos)
        {
            RepoGrupos = repoGrupos;
        }

        public void Remove(int id)
        {
            try
            {
                //Validar que no se encuentre vinculado con ninguna seleccion o partido. 
                RepoGrupos.Remove(id);
            }
            catch (Exception e)
            {
                throw new Exception("No se pudo borar el grupo", e);
            }
        }
    }
}
