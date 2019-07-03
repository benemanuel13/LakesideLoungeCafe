using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Net.Sockets;
using System.Diagnostics;

using System.Windows;

using LakesideLoungeAdmin.Domain;

namespace LakesideLoungeAdmin.Infrastructure
{
    public class Network
    {
        static Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        static bool adbRun = false;

        public static void Connect()
        {
            if (!adbRun)
                RunADB();

            socket.Connect("127.0.0.1", 10010);
        }

        public static void RequestOrders()
        {
            SendMessage("REQUEST_ORDERS");
            RecieveOrders();

            socket.Disconnect(false);

            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect("127.0.0.1", 10010);
        }

        private static void RecieveOrders()
        {
            NetworkStream stream = new NetworkStream(socket);
            StreamReader reader = new StreamReader(stream);

            string message = reader.ReadLine();

            Order order = null;
            OrderItem orderItem = null;

            while(message != "END_OF_ORDERS")
            {
                if (message.StartsWith("SAVE_ORDER"))
                {
                    int firstComma = message.IndexOf(",");
                    int secondComma = message.IndexOf(",", firstComma + 1);
                    int id = Int32.Parse(message.Substring(firstComma + 1, secondComma - firstComma - 1));
                    order = new Order(id, 0, DateTime.Now);

                    int thirdComma = message.IndexOf(",", secondComma + 1);
                    order.Name = message.Substring(secondComma + 1, thirdComma - secondComma - 1);

                    int fourthComma = message.IndexOf(",", thirdComma + 1);
                    order.CustomerType = Int32.Parse(message.Substring(thirdComma + 1, fourthComma - thirdComma - 1));

                    order.Date = DateTime.Parse(message.Substring(fourthComma + 1, message.Length - fourthComma - 1));
                }
                else if (message.StartsWith("ADD_ORDER_ITEM"))
                {
                    int firstComma = message.IndexOf(",");
                    int secondComma = message.IndexOf(",", firstComma + 1);
                    int id = Int32.Parse(message.Substring(firstComma + 1, secondComma - firstComma - 1));

                    int thirdComma = message.IndexOf(",", secondComma + 1);
                    int orderId = Int32.Parse(message.Substring(secondComma + 1, thirdComma - secondComma - 1));

                    int fourthComma = message.IndexOf(",", thirdComma + 1);
                    int variationId = Int32.Parse(message.Substring(thirdComma + 1, fourthComma - thirdComma - 1));

                    int fifthComma = message.IndexOf(",", fourthComma + 1);
                    int inOutStatus = Int32.Parse(message.Substring(fourthComma + 1, fifthComma - fourthComma - 1));

                    //int sixthComma = message.IndexOf(",", fifthComma + 1);
                    int discountId = Int32.Parse(message.Substring(fifthComma + 1, message.Length - fifthComma - 1));
                    
                    orderItem = new OrderItem(id, orderId, variationId, inOutStatus, discountId);
                    order.AddOrderItem(orderItem);
                }
                else if (message.StartsWith("ADD_ORDERITEM_COMPONENT"))
                {
                    int firstComma = message.IndexOf(",");
                    int secondComma = message.IndexOf(",", firstComma + 1);
                    int id = Int32.Parse(message.Substring(firstComma + 1, secondComma - firstComma - 1));

                    int thirdComma = message.IndexOf(",", secondComma + 1);
                    int orderItemId = Int32.Parse(message.Substring(secondComma + 1, thirdComma - secondComma - 1));

                    //int fourthComma = message.IndexOf(",", thirdComma + 1);
                    //int componentId = Int32.Parse(line.Substring(thirdComma + 1, fourthComma - thirdComma - 1));
                    int portions = Int32.Parse(message.Substring(thirdComma + 1, message.Length - thirdComma - 1));

                    Component component = Database.GetComponent(id);
                    component.Portions = portions;

                    //orderItem.AddComponent(component);
                }
                else if(message == "END_ORDER")
                    Database.SaveOrder(order);

                message = reader.ReadLine();
            }

            reader.Close();
            stream.Close();
        }

        public static void SendMessage(string message)
        {
            NetworkStream stream = new NetworkStream(socket);
            StreamWriter writer = new StreamWriter(stream);

            writer.WriteLine(message);
            writer.Flush();

            Task.Run(() => { });

            writer.Close();
            stream.Close();
        }

        public static void SendMessages(List<string> messages)
        {
            NetworkStream stream = new NetworkStream(socket);
            StreamWriter writer = new StreamWriter(stream);

            foreach (string message in messages)
            {
                writer.WriteLine(message);
                writer.Flush();

                Task.Run(() => { });
            }

            writer.Close();
            stream.Close();
        }

        public static bool IsConnected()
        {
            return socket.Connected;
        }

        public static void Disconnect()
        {
            socket.Close();
        }

        private static void RunADB()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(AppDomain.CurrentDomain.BaseDirectory + "\\adb\\adb.exe");
            startInfo.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory + "\\adb";
            startInfo.UseShellExecute = true;
            startInfo.Arguments = "forward tcp:10010 tcp:10010";

            Process process = new Process();
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();

            adbRun = true;
        }
    }
}