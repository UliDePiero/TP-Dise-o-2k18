﻿using Microsoft.Owin;
using Owin;
using TP0.Helpers;

[assembly: OwinStartupAttribute(typeof(TP0.Startup))]
namespace TP0

{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            Recomendacion.Instancia();
        }
    }
}
