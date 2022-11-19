using LogicaAplicacion.InterfacesCasosUso.ICasosUsoHorario;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.CasosUso.CasosUsoHorario
{
    public class AltaHorario : IAltaHorario
    {
        public IRepositorioHorarios RepoHorarios { get; set; }

        public AltaHorario(IRepositorioHorarios repoHorarios)
        {
            RepoHorarios = repoHorarios;
        }
        public void Add(Horario obj)
        {
            try
            {
                RepoHorarios.Add(obj);
            }
            catch (Exception e)
            {
                throw new Exception("No se pudo dar de alta el Horario", e);
            }
        }
    }
}
