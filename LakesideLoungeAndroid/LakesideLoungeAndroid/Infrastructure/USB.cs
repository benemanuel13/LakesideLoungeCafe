using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using System.IO;
using System.Threading;
using System.Threading.Tasks;

using Java.Net;

using LakesideLoungeAndroid.Domain;

namespace LakesideLoungeAndroid.Infrastructure
{
    public static class USB
    {
        private static Thread thread;
        private static ServerSocket serverSocket;
        private static bool listening = false;

        public static void StartListening()
        {
            ThreadStart start = new ThreadStart(Listen);
            thread = new Thread(start);

            thread.Start();
        }

        private static void Listen()
        {
            serverSocket = new ServerSocket(10010);
            listening = true;

            while (true)
            {
                Socket socket;
                try
                {
                    socket = serverSocket.Accept();
                }
                catch (Exception)
                {
                    return;
                }

                StreamReader reader = new StreamReader(socket.InputStream);
                string message = reader.ReadLine();

                if(message != null)
                {
                    if (message == "REQUEST_ORDERS")
                        SendOrders(socket);
                    else if (message == "UPDATES_START")
                        RecieveUpdates(socket);
                }

                reader.Close();
                socket.Close();
            }
        }

        private static void SendOrders(Socket socket)
        {
            StreamWriter writer = new StreamWriter(socket.OutputStream);

            //List<Order> orders = Database.GetUnuploadedOrders(uploadProgress);

            List<Order> orders = new List<Order>();

            foreach (Order order in orders)
            {
                string name;

                if (order.Name == "")
                    name = "Order #" + order.Id;
                else
                    name = order.Name;

                string line = "SAVE_ORDER," + order.Id.ToString() + "," + name + "," + order.CustomerType.ToString() + "," + order.Date.ToLongDateString();
                writer.WriteLine(line);
                writer.Flush();

                Task.Run(() => { });

                foreach (OrderItem item in order.OrderItems)
                {
                    line = "ADD_ORDER_ITEM," + item.Id.ToString() + "," + item.OrderId.ToString() + "," + item.VariationId.ToString() + "," + item.InOutStatus.ToString() + "," + item.DiscountId.ToString();
                    writer.WriteLine(line);
                    writer.Flush();

                    Task.Run(() => { });

                    foreach (OrderItemComponent component in item.Components)
                    {
                        line = "ADD_ORDERITEM_COMPONENT," + component.Id.ToString() + "," + item.Id.ToString() + "," + component.Portions;
                        writer.WriteLine(line);
                        writer.Flush();

                        Task.Run(() => { });
                    }
                }

                Database.OrderUploaded(order.Id, true);

                line = "END_ORDER";
                writer.WriteLine(line);
                writer.Flush();

                Task.Run(() => { });
            }

            writer.WriteLine("END_OF_ORDERS");
            writer.Flush();

            Task.Run(() => { });

            writer.Close();
        }

        public static void RecieveUpdates(Socket socket)
        {
            Log.CreateTransactionFile("Updates");

            StreamReader reader = new StreamReader(socket.InputStream);

            string update = reader.ReadLine();

            while(update != "UPDATES_END")
            {
                Log.StoreUpdate(update);
                //Database.ProcessUpdate(update);

                update = reader.ReadLine();
            }

            reader.Close();
        }

        public static void StopListening()
        {
            serverSocket.Close();
            listening = false;
        }

        public static bool IsListening()
        {
            return listening;
        }
    }
}