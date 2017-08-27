using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;

namespace OwinIISHost
{
    public class AuthenticateMiddleware
    {
        private Func<IDictionary<string, object>, Task> nextAppFunc;
        public AuthenticateMiddleware(Func<IDictionary<string, object>, Task> nextMiddleWareFunc)
        {
            nextAppFunc = nextMiddleWareFunc;
        }

        public async Task Invoke(IDictionary<string, object> parameters)
        {
            Console.WriteLine("Authenticating");

            var identity = new GenericIdentity("jensen");
            //parameters["server.User"] = new GenericPrincipal(identity, new string[] { "admin" });
            if (nextAppFunc != null)
            {
                await nextAppFunc.Invoke(parameters);
            }
        }
    }
}