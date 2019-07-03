using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.Text;
using System.IO;

using LakesideLoungeWebApi.Application;
using LakesideLoungeWebApi.Domain;
using LakesideLoungeWebApi.Application.Services;

namespace LakesideLoungeWebApi.Controllers
{
    public class OrdersController : ApiController
    {
        OrdersControllerService svc = new OrdersControllerService();

        // POST api/orders
        public Order PostOrder([FromBody]Order order)
        {
            //svc.SaveOrder(order);
            return order;
        }

        public List<Order> PostOrders([FromBody] List<Order> orders)
        {
            return orders;
        }

        public List<List<Person>> PostPerson([FromBody] List<List<Person>> person)
        {
            return person;
        }

        public Dictionary<int, Person> PostDict([FromBody] Dictionary<int, Person> dict)
        {
            return dict;
        }

        public Person[] PostArray([FromBody] Person[] person)
        {
            return person;
        }

        public int[][] PostMulti([FromBody] int[][] person)
        {
            return person;
        }

        public void PostBytes([FromBody] byte[] bytes)
        {
        }

        public Person PostImage([FromBody] Person person)
        {
            string filename = Request.Content.Headers.ContentDisposition.FileName;

            //byte[] bytes = System.Net.WebUtility.UrlDecodeToBytes(person.Image, 0, person.Image.Length);

            filename = AppDomain.CurrentDomain.BaseDirectory + "\\" + filename;

            FileStream stream = new FileStream(filename, FileMode.Create);

            //stream.Write(bytes, 0, bytes.Length);
            stream.Flush();

            stream.Close();

            return person;
        }

        public void PostByteArray([FromBody] byte[] bytes)
        {
            string con = Request.Content.Headers.ContentDisposition.Name;

            string filename = AppDomain.CurrentDomain.BaseDirectory + "\\person_from_array.jpg";

            FileStream stream = new FileStream(filename, FileMode.Create);
            StreamWriter writer = new StreamWriter(stream);

            stream.Write(bytes, 0, bytes.Length);
            stream.Flush();

            writer.Close();
            stream.Close();
        }
    }
}
