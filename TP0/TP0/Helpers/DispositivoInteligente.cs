﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace TP0.Helpers
{
    public class DispositivoInteligente
    {
        FechasAdmin fadmin;
        [JsonProperty]
        public string id;
        [JsonProperty]
        public string nombre;
        public State Estado;
        [JsonProperty]
        public double kWxHora;
        [JsonProperty]
        public List<State> estadosAnteriores;

        public DispositivoInteligente(string nom, string idnuevo, double kWxHoraNuevo)
        {
            kWxHora = kWxHoraNuevo;
            nombre = nom;
            id = idnuevo;
        }

        public bool estaEncendido()
        {
            return Estado is Encendido;
        }
        public bool estaApagado()
        {
            return Estado is Apagado ;
        }
        public void encender()
        {
            Estado.Encender(this);
        }
        public void apagar()
        {
            Estado.Apagar(this);
        }
        public void ahorrarEnergia()
        {
            Estado.AhorrarEnergia(this);
        }
        public double consumoEnHoras(double horas)
        {
            DateTime fFinal = new DateTime();
            DateTime fInicial = fFinal.AddHours(-horas);

            double hs = fadmin.ConsumoHsTotalPeriodo(fInicial, fFinal, estadosAnteriores);
            return hs * kWxHora;
        }

       public double consumoEnPeriodo(DateTime finicial, DateTime ffinal)
       { 
           double hs = fadmin.ConsumoHsTotalPeriodo(finicial, ffinal, estadosAnteriores);
           return hs * kWxHora;
       }

          public void agregarEstado(State estado)
        {
            Estado = estado;
            estadosAnteriores.Add(Estado);
        }

    }
}
