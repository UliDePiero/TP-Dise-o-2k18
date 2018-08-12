﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace TP0.Helpers.Static.Simplex
{
    public static class SimplexHelper
    {
        public static string generarJson(List<DispositivoEstandar> estandars, List<DispositivoInteligente> inteligentes)
        {
            var obj = new Simplex();//obj es el objeto q se pasa a json
            int dispTotales = estandars.Count() + inteligentes.Count();
            List<Double> v = obj.getVars(dispTotales); //se hace la primera fila del archivo json
            obj.vars = v;
            List<Double> values1 = new List<Double>(); //esta es la primera fila de valores de la parte de "restricciones"
            values1.Add(620); //consumo total = 620
            foreach (DispositivoEstandar de in estandars)
            {
                values1.Add(de.kWxHora); //se llena la lista con los kwxh de los dispositivos estandars
            }
            foreach(DispositivoInteligente di in inteligentes)
            {
                values1.Add(di.kWxHora);//se llena la lista con los kwxh de los dispositivos inteligentes
            }
            
            restriction restriccion1 = new restriction(values1,"<="); //primera restriccion
            obj.restrictions = new List<restriction>();
            obj.restrictions.Add(restriccion1);
            //faltan todas las otras restricciones
            return JsonConvert.SerializeObject(obj);
        }
    }
}