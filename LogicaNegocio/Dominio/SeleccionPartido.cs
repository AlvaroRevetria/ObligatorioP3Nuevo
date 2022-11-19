using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaNegocio.Dominio
{
    public class SeleccionPartido
    {
        public Partido Partido { get; set; }
        public Seleccion Seleccion { get; set; }
        public int PartidoId { get; set; }
        public int SeleccionId { get; set; }

    }
}
