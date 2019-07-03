using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using LakesideLoungeWebApi.Domain;
using LakesideLoungeWebApi.Infrastructure;

namespace LakesideLoungeWebApi.Controllers
{
    public class LakesideLoungeController : ApiController
    {
        // GET: api/Lakeside
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [Route("api/LakesideLounge/ResetDatabase")]
        [HttpGet]
        public void ResetDatabase()
        {
            Database.Reset();
        }

        // GET: api/Lakeside/5
        public string Get(int id)
        {
            return "value";
        }

        public string GetOrder(int id)
        {
            return "Order " + id;
        }

        // GET api/LakesideLounge/GetUpdates
        [Route("api/LakesideLounge/GetUpdates")]
        public List<Update> GetUpdates()
        {
            return Database.GetUpdates();
        }

        // POST api/LakesideLounge/PostOrders
        [Route("api/LakesideLounge/PostOrders")]
        public string PostOrders([FromBody]List<Order> orders)
        {
            if (orders == null)
                return "NULL";

            foreach (Order order in orders)
            {
                try
                {
                    Database.SaveOrder(order);
                }
                catch(Exception ex)
                {
                    return ex.Message;
                }
            }

            return "OK";
        }

        // POST: api/LakesideLounge
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/LakesideLounge/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/LakesideLounge/5
        public void Delete(int id)
        {
        }
    }
}
