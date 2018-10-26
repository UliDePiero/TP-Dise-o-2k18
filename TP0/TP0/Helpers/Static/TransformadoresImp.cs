﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers.Static
{
    public static class TransformadoresImp
    {
        public static List<Transformador> transformadores;
        public static List<Transformador> GetTransformadores()
        {
            if (transformadores.Count > 0)
            {
                return transformadores;
            }
            else return null;
        }
        public static Transformador filtrarTransformador(int idFiltro)
        {
            if (transformadores.Count > 0)
            {
                return transformadores.Find(x => x.TransformadorID == idFiltro);
            }
            else return null;
        }
        public static string generarJsonTransformadores (List<Transformador> transformadores)
        {
            string jsonData = JsonConvert.SerializeObject(transformadores);
            return jsonData;
        }
    }
}
