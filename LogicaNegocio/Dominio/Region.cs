using LogicaNegocio.InterfacesDominio;
using System;
using System.Collections.Generic;
using System.Text;
using Excepciones;

namespace LogicaNegocio.Dominio
{
    public class Region : IValidable
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public void Validar()
        {
            ValidarRegion();
        }

        private void ValidarRegion()
        {
            List<string> regiones = new List<string> { "AFRICA", "AMERICA", "ASIA", "EUROPA", "OCEANIA" };
            if (!regiones.Contains(Nombre.ToUpper()))
            {
                throw new RegionException("La región debe ser un continente válido");
            }
        }
    }
}
