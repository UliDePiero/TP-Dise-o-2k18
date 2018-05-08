﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public class Categoria
    {
        [JsonProperty]
        public int consumoMin;
        [JsonProperty]
        public int consumoMax;
        [JsonProperty]
        public float cargoFijo;      //  $/mes
        [JsonProperty]
        public float cargoVariable;  //  $/kWh
        public float CalcularTarifa(float consumo)
        {
            return (cargoFijo + consumo * cargoVariable);
        }
        public bool PerteneceACategoria(float consumo){
            return consumoMin <= consumo && consumo <= consumoMax;
        }
    }
}