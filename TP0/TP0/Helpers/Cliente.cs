﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public class Cliente:Persona
    {
        public Documento documento;
        public string telefono;
        public enum Categoria { R1, R2, R3, R4, R5, R6, R7, R8, R9 };
        public List<Dispositivo> dispositivos;
        public bool AlgunDispositivoEncendido()
        {
            return dispositivos.Any(d => d.EstaEncendido());
        }
        public int DispositivosEncendidos()
        {
            return dispositivos.Count(d=>d.EstaEncendido());
        }
        public int DispositivosApagados()
        {
            return dispositivos.Count(d => !d.EstaEncendido());
        }
        public int DispositivosTotales()
        {
            return dispositivos.Count();
        }
        public int CalcularConsumo()
        {
            return dispositivos.Sum(d=>d.KWxHora());
        }
        public void ActualizarCategoria(){
            
        }
    }
}
