
using LogicaNegocio.InterfacesDominio;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
using Excepciones;

namespace LogicaNegocio.Dominio
{
   public class Seleccion : IValidable
    {
        public int Id { get; set; }
        public Pais Pais { get; set; }
        public int PaisId { get; set; }
        public string  Nombre { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        [Display(Name = "Apostadores")]
        public int CantPotencialApostadores { get; set; }
        public int Puntos { get; set; }
        public Grupo Grupo { get; set; }
        public int GrupoId { get; set; }

        public IEnumerable<SeleccionPartido> SeleccionPartido { get; set; }

        public void Validar()
        {
            ValidarNombre();
            ValidarTelefono();
            ValidarEmail();
        }

        private void ValidarNombre()
        {
            if (string.IsNullOrEmpty(Nombre)) throw new SeleccionException("El nombre de la persona de contacto es obligatorio");
            if (Nombre.Length < 3) throw new SeleccionException("El nombre de la persona de contacto debe tener al menos 3 caracteres");
            if (!Regex.IsMatch(Nombre, "^[a-zA-Z]+((\\s[a-zA-Z]+)?)*$")) throw new SeleccionException("El nombre de la persona de contacto solo puede contener letras y espacios embebidos");
        }

        private void ValidarTelefono()
        {
            if (Telefono.Length < 7) throw new SeleccionException("El telefono debe por lo menos tener 7 caracteres");

        }

        private void ValidarEmail()
        {
            if(!Regex.IsMatch(Email, "^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\\.[a-zA-Z0-9-]+)*$")) throw new SeleccionException("El email debe ser una dirección válida");
        }
    }
}
