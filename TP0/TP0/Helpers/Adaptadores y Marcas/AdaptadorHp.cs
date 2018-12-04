﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TP0.Helpers.ORM;

namespace TP0.Helpers
{
    public class AdaptadorHp : DispositivoInteligente
    {
        public AdaptadorHp(string nom, string idnuevo, double kWxHoraNuevo, double mx, double mn)
        {
            KWxHora = kWxHoraNuevo;
            Nombre = nom;
            Codigo = idnuevo;
            Max = mx;
            Min = mn;
            estadosAnteriores = new List<State>();
            ConsumoAcumulado = 0;
            EsInteligente = true;
            //act = new Actuador(Int32.Parse(idnuevo));
        }
        public AdaptadorHp(int DIID)//para buscar en la DB + instanciar
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
                IDUltimoEstado = Disp.IDUltimoEstado;
                this.ActualizarUltimoEstado();
                UsuarioID = Disp.UsuarioID;
                DispositivoID = Disp.DispositivoID;
                actuadores = new List<Actuador>();
                FechaAlta = Disp.FechaAlta;
                //act = new Actuador(DispositivoID);
                ActualizarConsumoAcumulado(new Cliente(UsuarioID).FechaDeAlta);
            }
        }
        public AdaptadorHp()
        {

        }
        //private Marca_Hp hp;
        Marca_Hp hp = new Marca_Hp();

        public override void ActualizarUltimoEstado()
        {
            hp.ActualizarUltimoEstadoHP(this);
        }
        public override State GetEstado()
        {
            return hp.GetEstadoHP(this);
        }
        public override List<State> GetEstados()
        {
            return hp.GetEstadosHP(this);
        }
        public override bool EstaEncendido()
        {
            return hp.EstaEncendidoHP(this);
        }
        public override bool EstaApagado()
        {
            return hp.EstaApagadoHP(this);
        }
        public override bool EnAhorro()
        {
            return hp.EnAhorroHP(this);
        }
        public override void Encender()
        {
            hp.EncenderHP(this);
        }
        public override void Apagar()
        {
            hp.ApagarHP(this);
        }
        public override void AhorrarEnergia()
        {
            hp.AhorrarEnergiaHP(this);
        }
        public override void ActualizarConsumoAcumulado(string FechaAlta)
        {
            hp.ActualizarConsumoAcumuladoHP(FechaAlta, this);
        }
        public override double Consumo()
        {
            return hp.ConsumoHP(this);
        }
        public override double ConsumoActual()
        {
            return hp.ConsumoActualHP(this);
        }
        public override double ConsumoEnHoras(double horas)
        {
            return hp.ConsumoEnHorasHP(horas, this);
        }
        public override double ConsumoEnPeriodo(DateTime fInicial, DateTime fFinal)
        {
            return hp.ConsumoEnPeriodoHP(fInicial, fFinal, this);
        }
        public override void AgregarEstado(State est)
        {
            hp.AgregarEstadoHP(est, this);
        }
    }
}
