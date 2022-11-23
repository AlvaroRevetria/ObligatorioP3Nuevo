﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

namespace DTOS
{
    public class DTOSeleccion
    {
        public string Nombre { get; set; }
        public string Bandera { get; set; }
        public int Puntos { get; set; }
        public int GolesAFavor { get; set; }
        public int GolesEnContra { get; set; }
        public int DifGoles { get; set; }
    }
}
