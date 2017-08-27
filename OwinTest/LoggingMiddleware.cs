using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwinTest
{
    public class LoggingMiddleware
    {
        private Func<IDictionary<string, object>, Task> nextAppFunc;
        public LoggingMiddleware(Func<IDictionary<string,object>,Task> nextMiddleWareFunc)
        {
            nextAppFunc = nextMiddleWareFunc;
        }

        public async Task Invoke(IDictionary<string, object> parameters)
        {
            Console.WriteLine("Logging Start");

            foreach (var kvp in parameters)
            {
                Console.WriteLine(string.Format("key:{0}, value:{1}", kvp.Key, kvp.Value));
            }

            if (nextAppFunc != null)
            {
                await nextAppFunc.Invoke(parameters);
            }

            Console.WriteLine("Logging End");
        }
    }
}
