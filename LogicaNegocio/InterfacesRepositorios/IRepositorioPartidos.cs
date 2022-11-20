using LogicaNegocio.Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaNegocio.InterfacesRepositorios
{
    public interface IRepositorioPartidos : IRepositorio<Partido>
    {
        IEnumerable<Partido> BuscarPorGurpo(string nombreGrupo);
        IEnumerable<Partido> BuscarPorSeleccionOPais(string nombre);
        IEnumerable<Partido> BuscarPorCodigoIsoPais(string codigoPais);
        IEnumerable<Partido> BuscarPorFechas(DateTime fechaDesde, DateTime fechaHasta);
    }
}
