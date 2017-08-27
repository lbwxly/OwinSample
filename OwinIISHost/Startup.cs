using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin.Extensions;

[assembly: OwinStartup(typeof(OwinIISHost.Startup))]
namespace OwinIISHost
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            appBuilder.Use<AuthenticateMiddleware>();
            appBuilder.UseStageMarker(PipelineStage.Authenticate);
        }
    }
}