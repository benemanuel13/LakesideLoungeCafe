using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.Threading.Tasks;

using LakesideLoungeWebApi.Application;

namespace LakesideLoungeWebApi.Controllers
{
    public class TestController : ApiController
    {
        public string GetOrder(int id)
        {
            return "Test: " + id;
        }

        public async Task<HttpResponseMessage> PostIt()
        {
            if (!Request.Content.IsMimeMultipartContent("form-data"))
                //return "";
                return Request.CreateResponse(HttpStatusCode.InternalServerError);

            string folder = "C:\\Users\\Ben\\Temp";

            MultipartFormDataStreamProvider p = new MultipartFormDataStreamProvider(folder);
            MultipartFileStreamProvider sp = await Request.Content.ReadAsMultipartAsync(p);


            return Request.CreateResponse(HttpStatusCode.OK, "OK");
            //return "";
        }

        // POST api/test
        public Person Post([FromBody]Person value)
        {
            Person newPerson = new Application.Person();
            newPerson.Name = "Freddy";
            return newPerson;
        }
    }
}
