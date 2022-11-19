using LogicaNegocio.InterfacesDominio;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
using Excepciones;

namespace LogicaNegocio.Dominio
{
    public class Pais: IValidable
    {
        public int Id { get; set; }
        [MinLength(3,ErrorMessage ="El nombre debe contener al menos 3 caracteres"), MaxLength(100), Required(ErrorMessage = "El nombre es olbigatorio ingresarlo")]
        [RegularExpression("^[a-zA-Z]+((\\s[a-zA-Z]+)?)*$",ErrorMessage = "El Nombre solo puede contener letras y espacios"), Display(Name = "Pais")]
        public string Nombre { get; set; }
        [MinLength(3, ErrorMessage = "El codigo debe contener al menos 3 caracteres"), MaxLength(3), Required(ErrorMessage = "Es obligatorio ingresar un código alfa3")]
        [RegularExpression("^[a-zA-Z]+$",ErrorMessage ="El Codigo solo puede contener letras")]
        public string Codigo { get; set; }
        public int Pbi { get; set; }
        public int Poblacion { get; set; }
        public string Bandera { get; set; }
        public Region Region { get; set; }
        public int RegionId { get; set; }
        public void Validar()
        {
            ValidarNombre();
            ValidarCodigo();        
            ValidarNumeros();
        }

        private void ValidarNombre()
        {
            if (string.IsNullOrEmpty(Nombre))
            {
                throw new PaisException("El nombre del país es obligatorio");
            }
            if (Nombre.Length < 3)
            {
                throw new PaisException("El nombre del país debe tener al menos 3 caracteres");
            }

            if (!Regex.IsMatch(Nombre, "^[a-zA-Z]+((\\s[a-zA-Z]+)?)*$"))
            {
                throw new PaisException("El nombre del país solo puede contener letras y espacios embebidos");
            }
        }

        private void ValidarCodigo()
        {
            if (string.IsNullOrEmpty(Codigo))
            {
                throw new PaisException("El código del país es obligatorio");
            }

            if (Codigo.Length < 3 || Codigo.Length > 3)
            {
                throw new PaisException("El código del país solo puede tener 3 caracteres");
            }

            string c = Codigo.ToCharArray()[0].ToString();
            string p = Nombre.ToCharArray()[0].ToString();

            if (c.ToLower() != p.ToLower())
            {
                throw new PaisException("El código del país debe comenzar con la misma letra que el nombre del país");
            }
        }

       

        private void ValidarNumeros()
        {
            if (Pbi < 0 )
            {
                throw new PaisException("El Pbi debe ser mayor a 0");
            }

            if (Poblacion < 0)
            {
                throw new PaisException("La Poblacion debe ser mayor a 0");
            }
        }
    }
}
