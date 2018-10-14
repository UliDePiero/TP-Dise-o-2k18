﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TP0.Helpers.ORM;

namespace TP0.Helpers
{
    public class DispositivoEstandar : Dispositivo
    {
        
        public DispositivoEstandar()
        {

        }

        public DispositivoEstandar(string nom, string idnuevo, double kWxH, double hxdia, double mx, double mn)
        {
            Codigo = idnuevo;
            Nombre = nom;
            KWxHora = kWxH;
            HorasXDia = hxdia;
            Max = mx;
            Min = mn;
            EsInteligente = false;
        }
        public DispositivoEstandar(int DEID)
        {
                using (var context = new DBContext())
                {
                    var Disp = context.Dispositivos.Find(DEID);
                    KWxHora = Disp.KWxHora;
                    Nombre = Disp.Nombre;
                    Codigo = Disp.Codigo;
                    Max = Disp.Max;
                    Min = Disp.Min;
                    ConsumoAcumulado = 0;
                    EsInteligente = false;
                    //act = new Actuador(DispositivoID);
                }
            }

        public override DispositivoInteligente ConvertirEnInteligente(string marca)
        {
            DispositivoInteligente convertido = null;
            switch (marca)
            {
                
                case "Samsung":
                    //AdaptadorSamsug convertido = new AdaptadorSamsung(...)
                    convertido = new AdaptadorSamsung(Nombre, Codigo, KWxHora, Max, Min);
                    break;
                case "HP":
                    convertido = new AdaptadorHp(Nombre, Codigo, KWxHora, Max, Min);
                    break;
                case "Apple":
                    convertido = new AdaptadorApple(Nombre, Codigo, KWxHora, Max, Min);
                    break;
            }
            convertido.UsuarioID = UsuarioID;

            return convertido;
        }

        public override double Consumo()
        {
            return HorasXDia * KWxHora;
        }
        public override double ConsumoEnPeriodo(DateTime fInicial, DateTime fFinal)
        {
            return fFinal.Subtract(fInicial).Days*Consumo();
        }

        public override bool EstaEncendido()
        {
            throw new NotImplementedException();
        }

        public override bool EstaApagado()
        {
            throw new NotImplementedException();
        }

        public override void Encender()
        {
            throw new NotImplementedException();
        }

        public override void Apagar()
        {
            throw new NotImplementedException();
        }

        public override void AhorrarEnergia()
        {
            throw new NotImplementedException();
        }

        public override double ConsumoEnHoras(double horas)
        {
            throw new NotImplementedException();
        }

        public override void AgregarEstado(State est)
        {
            throw new NotImplementedException();
        }

        public override State GetEstado()
        {
            throw new NotImplementedException();
        }
    }
}