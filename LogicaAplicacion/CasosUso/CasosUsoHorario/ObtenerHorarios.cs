using LogicaAplicacion.InterfacesCasosUso.ICasosUsoHorario;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.CasosUso.CasosUsoHorario
{
   public  class ObtenerHorarios : IObtenerHorarios
    {
        public IRepositorioHorarios RepoHorarios { get; set; }

        public ObtenerHorarios(IRepositorioHorarios repoHorarios)
        {
            RepoHorarios = repoHorarios;
        }
       

        public IEnumerable<Horario> FindAll()
        {
            try
            {
                return RepoHorarios.FindAll();
            }
            catch (Exception e)
            {
                throw new Exception("No se pudieron obtener los horarios", e);
            }
        }
    }
}
