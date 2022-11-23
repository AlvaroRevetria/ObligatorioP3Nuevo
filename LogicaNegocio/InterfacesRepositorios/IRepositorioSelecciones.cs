using LogicaNegocio.Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaNegocio.InterfacesRepositorios
{
   public interface IRepositorioSelecciones:IRepositorio<Seleccion>
    {
        int CalcularGolesAFavor(Seleccion seleccion);
        int CalcularGolesEnContra(Seleccion seleccion);

        IEnumerable<Seleccion> SeleccionesPorGrupo(string nombreGrupo);

    }
}
