﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public class Sensor
    {
        [Key]
        public int SensorID { get; set; }
        public string Desc { get; set; }
        
        [NotMapped]
        public List<Actuador> Observers { get; set; }
        [NotMapped]
        public float Medicion;

        public Sensor(string descripcion)
        {
            Desc = descripcion;
        }
        public Sensor()
        {
        }

        public void AgregarObservador(Actuador c)
        {
            Observers.Add(c);
        }
        public void QuitarObservador(Actuador c)
        {
            Observers.Remove(c);
        }
        public void Notificar()
        {
            Observers.ForEach(o => o.Notificar(Medicion));
        }
    }
}