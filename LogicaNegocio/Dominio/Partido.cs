using LogicaNegocio.InterfacesDominio;
using System;
using Excepciones;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogicaNegocio.Dominio
{
    public class Partido : IValidable
    {
        public int Id { get; set; }
        public Seleccion Seleccion1 { get; set; }
        public Seleccion Seleccion2 { get; set; }
        public DateTime Fecha { get; set; }
        public Horario Hora { get; set; }       
        public string Estado { get; set; }

        public int GolesS1 { get; set; }
        public int GolesS2 { get; set; }
        public int CantidadRojasS1 { get; set; }
        public int CantidadRojasS2 { get; set; }
        public int CantidadAmarillasS1 { get; set; }
        public int CantidadAmarillasS2 { get; set; }
        public int CantidadRojasAcAmarillasS1 { get; set; }
        public int CantidadRojasAcAmarillasS2 { get; set; }


        public void Validar()
        {
            Hora.Validar();
            ValidarFecha();
            ValidarSelecciones();
            ValidarNumeros();
        }

        private void ValidarNumeros()
        {
            if (CantidadRojasS1 < 0 || CantidadRojasS2 < 0 || CantidadAmarillasS1 < 0 || CantidadAmarillasS2 < 0
                || CantidadRojasAcAmarillasS1 < 0 || CantidadRojasAcAmarillasS2 < 0)
            {
                throw new PartidoException("Los valores datos numéricos deben ser mayor a 0");
            }
        }


        public void ValidarFecha()
        {
            if( Fecha < new DateTime(2022,11,20) && Fecha > new DateTime(2022, 12, 02))
            {
                throw new PartidoException("La fecha del partido debe estar entre el 20/11/2022 y el 02/12/2022");
            }
        }

        public void ValidarSelecciones()
        {
            if(Seleccion1.Id == Seleccion2.Id)
            {
                throw new PartidoException("La seleccion no puede jugar contra si misma.");
            }
        }
    }
}
