using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace OwinTest
{

    public class UserPrinciple : IPrincipal
    {
        private IIdentity identity;

        public UserPrinciple(IIdentity identity)
        {
            this.identity = identity;
        }

        public IIdentity Identity
        {
            get
            {
                return this.identity;
            }
        }

        public bool IsInRole(string role)
        {
            return true;
        }
    }

    public class UserIdentity : IIdentity
    {
        private string name;
        private string authType;
        public UserIdentity(string name, string authenticateType)
        {
            this.name = name;
            this.authType = authenticateType;
        }
        public string Name
        {
            get { return name; }
        }

        public string AuthenticationType
        {
            get { return authType; }
        }

        public bool IsAuthenticated
        {
            get { return true; }
        }
    }

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
            string queryString = parameters["owin.RequestQueryString"] as string;
            var respStream = parameters["owin.ResponseBody"] as Stream;
            var streamWriter = new StreamWriter(respStream);
            var queryDic = ParseQueryString(queryString);

            const string tokenKey = "token";
            const string predefineToken = "88888888";
            if (!queryDic.ContainsKey(tokenKey)||queryDic[tokenKey]!=predefineToken)
            {
                streamWriter.WriteLine("Access Denied!");
                streamWriter.Flush();
                return;
            }

            var identity = new GenericIdentity("boss zhang");
            parameters["server.User"] = new GenericPrincipal(identity, new string[] { "admin" });
            if (nextAppFunc != null)
            {
                await nextAppFunc.Invoke(parameters);
            }
        }

        private Dictionary<string, string> ParseQueryString(string originalString)
        {
            string[] queryStringItems = originalString.Split(new string[] { "&" }, StringSplitOptions.RemoveEmptyEntries);
            var queryStringDic = new Dictionary<string, string>();
            foreach (var item in queryStringItems)
            {
                string[] queryStringKvp = item.Split(new string[] { "=" }, StringSplitOptions.None);
                if (queryStringKvp.Length == 2)
                {
                    queryStringDic[queryStringKvp[0]] = queryStringKvp[1];
                }
            }

            return queryStringDic;
        }
    }
}
