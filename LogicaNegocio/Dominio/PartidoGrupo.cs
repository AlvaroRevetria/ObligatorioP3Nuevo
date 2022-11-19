using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaNegocio.Dominio
{
    public class PartidoGrupo
    {
        public Partido Partido { get; set; }
        public Grupo Grupo { get; set; }
        public int PartidoId { get; set; }
        public int GrupoId { get; set; }

    }
}
