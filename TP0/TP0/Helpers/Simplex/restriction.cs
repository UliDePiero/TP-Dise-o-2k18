﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers.Static.Simplex
{
    public class restriction
    {
        public List<Double> values { get; set; }
        public string operador { get; set; }

        public restriction(List<Double> vs, string op)
        {
            values = vs;
            operador = op;

        }

        public restriction(List<Double> vs) // recibe lista tipo "values":[90,0,0,1...,minomax] para asignar el operador
        {
            List<restriction> rs = new List<restriction>();
            if (vs.Last() == 1)
            {
                operador = "<="; //max
            }
            else
            {
                operador = ">="; //min
            }
            vs.RemoveAt(vs.Count()-1);
             values = vs;

        }
    }
}