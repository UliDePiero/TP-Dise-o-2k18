﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using TP0.Helpers;
//using Windows.Devices.Geolocation;

namespace TP0.Helpers
{
    public class Cliente : Usuario
    {
        public string Documento { get; set; }
        public string TipoDocumento { get; set; }
        public string Telefono { get; set; }
        public ICollection<Dispositivo> Dispositivos { get; set; }

        public int TransformadorID { get; set; }
        [ForeignKey("TransformadorID")]
        public Transformador Transformador { get; set; }

        //Datos que no se persisten en la base de datos
        [NotMapped]
        public Categoria categoria;
        [NotMapped]
        public int puntos;
        [NotMapped]
        public Recomendacion recomendacion = Recomendacion.Instancia();
        [NotMapped]
        public bool accionAutomatica;

        public Cliente(string nombre, string apellido, string domicilio, string usuario, string contrasenia, string doc, string tipo, string tel) 
        {
            Nombre = nombre;
            Apellido = apellido;
            Domicilio = domicilio;
            Username = usuario;
            Contrasenia = contrasenia;
            EsAdmin = false;
            Documento = doc;
            TipoDocumento = tipo;
            Telefono = tel;
            Dispositivos = new List<Dispositivo>();
            recomendacion.NuevoCliente(this);
            accionAutomatica = false;
        }
         
        public bool AlgunDispositivoEncendido()
        {
            foreach (DispositivoInteligente disp in Dispositivos)
            {
                if (disp.EstaEncendido())
                    return true;
            }
            return false;
        }
        public int DispositivosEncendidos()
        {
            int encendidos = 0;
            foreach (DispositivoInteligente disp in Dispositivos)
            {
                if (disp.EstaEncendido())
                    encendidos++;
            }
            return encendidos;
        }
        public int DispositivosApagados()
        {
            int apagados = 0;
            foreach (DispositivoInteligente disp in Dispositivos)
            {
                if (!disp.EstaEncendido())
                    apagados++;
            }
            return apagados;
        }
        public int DispositivosTotales()
        {
            return Dispositivos.Count();
        }
        public double EstimarFacturacion(DateTime fInicial, DateTime fFinal)
        {
            return categoria.CalcularTarifa(KwTotales(fInicial, fFinal));
        }
        public double KwTotales(DateTime fInicial, DateTime fFinal)
        {
            double Consumo = 0;
            foreach (DispositivoEstandar d in Dispositivos)
                Consumo += d.ConsumoEnPeriodo(fInicial, fFinal);
            foreach (DispositivoInteligente d in Dispositivos)
                Consumo += d.ConsumoEnPeriodo(fInicial, fFinal);
            return Consumo;
        }
        public void AgregarDispInteligente(DispositivoInteligente DI)
        {
            Dispositivos.Add(DI);
            puntos += 15;
        }
        public void AdaptarDispositivo(DispositivoEstandar D, string marca)
        {
            DispositivoInteligente DI;
            DI=D.ConvertirEnInteligente(marca);
            Dispositivos.Add(DI);
            puntos += 10;
        }

        public string SolicitarRecomendacion()
        {
            return recomendacion.GenerarRecomendacion(this);
        }

        public void ActualizarCategoria()
        {
            
        }
        /*public double[] UbicacionDomicilio()
        {
            Geolocator geolocator = new Geolocator();
            geolocator.DesiredAccuracy.InMeters = 10;
            Geoposition ubicacion = await geolocator.GetGeopositionAsync();
            double latitud = ubicacion.Coordinate.Point.Position.Latitude;
            double longitud = ubicacion.Coordinate.Point.Position.Longitude;
            double[] CoordUbicacion = new double[] { latitud, longitud };
            return CoordUbicacion;
        }*/
    }
}
