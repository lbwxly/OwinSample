using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace OwinTest
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            appBuilder.Use<LoggingMiddleware>();
            appBuilder.Use<AuthenticateMiddleware>();

            var configuration = new HttpConfiguration();
            configuration.MapHttpAttributeRoutes();
            appBuilder.UseWebApi(configuration);
        }
    }
}
