using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace OwinTest
{
    [RoutePrefix("api/persons")]
    public class PersonController : ApiController
    {
        [Route("{id}/name")]
        [Authorize]
        public string getName(string id)
        {
            return id + "@boss";
        }
    }
}
