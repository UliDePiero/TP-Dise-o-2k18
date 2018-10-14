﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using TP0.Helpers.ORM;

namespace TP0.Helpers
{
    public class DispositivoInteligente : Dispositivo
    {
        [NotMapped]
        public State Estado;
        [NotMapped]
        [JsonProperty]
        public ICollection<State> estadosAnteriores;
        [NotMapped]
        public Actuador act;

        public DispositivoInteligente()
        {

        }
        public DispositivoInteligente(string nom, string idnuevo, double kWxHoraNuevo, double mx, double mn)
        {
            KWxHora = kWxHoraNuevo;
            Nombre = nom;
            Codigo = idnuevo;
            Max = mx;
            Min = mn;
            estadosAnteriores = new List<State>();
            ConsumoAcumulado = 0;
            EsInteligente = true;
            Estado = null;
            //act = new Actuador(DispositivoID);
        }

        public DispositivoInteligente(int DIID)
        {
            using (var context = new DBContext())
            {
                var Disp = context.Dispositivos.Find(DIID);
                KWxHora = Disp.KWxHora;
                Nombre = Disp.Nombre;
                Codigo = Disp.Codigo;
                Max = Disp.Max;
                Min = Disp.Min;
                estadosAnteriores = new List<State>();
                ConsumoAcumulado = 0;
                EsInteligente = true;
                Estado = null;
                //act = new Actuador(DispositivoID);

            }
        }

        public override State GetEstado()
        {
            return Estado;
        }
        public override bool EstaEncendido()
        {
            return Estado is Encendido;
        }
        public override bool EstaApagado()
        {
            return Estado is Apagado ;
        }
        public override void Encender()
        {
            UltimoEstado();
            Estado.Encender(this);
        }
        public override void Apagar()
        {
            UltimoEstado();
            Estado.Apagar();
        }
        public override void AhorrarEnergia()
        {
            UltimoEstado();
            Estado.AhorrarEnergia();
        }
        public override double ConsumoEnHoras(double horas)
        {
            DateTime fFinal = DateTime.Now;
            DateTime fInicial = fFinal.AddHours(-horas);
            double hs = Static.FechasAdmin.ConsumoHsTotalPeriodo(fInicial, fFinal, estadosAnteriores);
            return hs * KWxHora;
        }

       public override double ConsumoEnPeriodo(DateTime fInicial, DateTime fFinal)
       { 
           double hs = Static.FechasAdmin.ConsumoHsTotalPeriodo(fInicial, fFinal, estadosAnteriores);
           return hs * KWxHora;
       }

       public override void AgregarEstado(State est)
       {
           
            Estado = est; //dejar sirve para los cambios de estado cuando el disp esta en memoria , asi evitar recurrir a la base
            estadosAnteriores.Add(Estado);
            using (var db = new DBContext())
            {

                db.Estados.Add(est); //Agrega el nuevo estado a la db
                db.SaveChanges();
                IDUltimoEstado = est.StateID;
                var DIact = db.Dispositivos.Find(DispositivoID);
                DIact.IDUltimoEstado = est.StateID;
                db.SaveChanges();
            }
       }

        public override double Consumo()
        {
            throw new NotImplementedException();
        }

        private void UltimoEstado()
        {
            if (Estado == null)
            { 
                using (var db = new DBContext())
                {
                    var ultimoEstado = db.Estados.Find(IDUltimoEstado);
                    switch (ultimoEstado.Desc)
                    {
                        case "Apagado":
                            Estado = new Apagado(this);
                            Estado.StateID = ultimoEstado.StateID;
                            break;
                        case "Encendido":
                            Estado = new Encendido(this);
                            Estado.StateID = ultimoEstado.StateID;
                            break;
                        case "Ahorro":
                            Estado = new Ahorro(this);
                            Estado.StateID = ultimoEstado.StateID;
                            break;
                        default:
                            throw new Exception("Estado no reconocido");
                    }
                }
            }
        }

        public override DispositivoInteligente ConvertirEnInteligente(string marca)
        {
            throw new NotImplementedException();
        }

        public void AsignarActuadorHumedad()
        {
            act = new ActuadorHumedad(DispositivoID);
        }
        public void AsignarActuadorMovimiento()
        {
            act = new ActuadorMovimiento(DispositivoID);
        }
        public void AsignarActuadorTemperatura()
        {
            act = new ActuadorTemperatura(DispositivoID);
        }
        public void AsignarActuadorLuz()
        {
            act = new ActuadorLuz(DispositivoID);
        }
    }
}
