using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LakesideLoungeWebApi.Application
{
    public class Person
    {
        private int id = 0;
        private string name = "None";
        private DateTime date = DateTime.Now;
        private decimal price = 0;

        private byte[] image = null;

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public DateTime Date
        {
            get
            {
                return date;
            }

            set
            {
                date = value;
            }
        }

        public decimal Price
        {
            get
            {
                return price;
            }

            set
            {
                price = value;
            }
        }

        //public byte[] Image
        //{
        //    get
        //    {
        //        return image;
        //    }

        //    set
        //    {
        //        image = value;
        //    }
        //}

        void Test()
        {
            //Newtonsoft.Json.JsonSerializer s = new Newtonsoft.Json.JsonSerializer();
            //s.Serialize()
        }
    }
}