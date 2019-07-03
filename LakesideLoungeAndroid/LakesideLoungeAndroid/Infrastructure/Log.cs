using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;
using System.IO;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using LakesideLoungeAndroid.Application;
using LakesideLoungeAndroid.Helpers;
using LakesideLoungeAndroid.Domain;

namespace LakesideLoungeAndroid.Infrastructure
{
    public static class Log
    {
        private static string currentTransactionFile;
        private static string currentUpdatesFile;
        //private static Context context;

        public static void CreateTransactionFile(string prefix)
        {
            //Java.IO.File outDir = Android.OS.Environment.ExternalStorageDirectory;
            //Java.IO.File newDir = new Java.IO.File(outDir, "LakesideLounge");
            //if (!newDir.Exists())
            //    newDir.Mkdir();

            //May need to use this...

            //Java.IO.File[] dirs = context.GetExternalFilesDirs(null);
            //DirectoryInfo inf = new DirectoryInfo(dirs[0].AbsolutePath);

            //DirectoryInfo info = new DirectoryInfo(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/LakesideLounge/");
            DirectoryInfo info = new DirectoryInfo("/sdcard/LakesideLounge/");
            FileInfo[] files = info.GetFiles();

            int maxVersion = 0;
            string todaysDate = Helper.FormatDate(DateTime.Now);
            todaysDate = todaysDate.Replace('/', '-');

            foreach (FileInfo file in files)
            {
                if (file.Name.StartsWith(prefix))
                {
                    int firstScorePos = file.Name.IndexOf('_');
                    int secondScorePos = file.Name.IndexOf('_', firstScorePos + 1);

                    string date = file.Name.Substring(firstScorePos + 1, secondScorePos - firstScorePos - 1);
                    if (date.Replace('-', '/') == Helper.FormatDate(DateTime.Now))
                    {
                        int version = Int32.Parse(file.Name.Substring(secondScorePos + 1, file.Name.Length - secondScorePos - 5));

                        if (version > maxVersion)
                            maxVersion = version;
                    }
                }
            }

            string newFile = prefix + "_" + todaysDate.ToString() + "_" + (maxVersion + 1).ToString("00") + ".log";

            if (prefix == "Orders")
                currentTransactionFile = "/sdcard/LakesideLounge/" + newFile;
            else if (prefix == "Updates")
                currentUpdatesFile = "/sdcard/LakesideLounge/" + newFile;

            FileStream fs = new FileStream(currentTransactionFile, FileMode.Create);
            fs.Close();

            Database.AddTransactionFile(newFile);
        }

        public static void RunTransactions(string prefix)
        {
            //Java.IO.File outDir = Android.OS.Environment.ExternalStorageDirectory;
            Java.IO.File outDir = new Java.IO.File("/sdcard/");
            Java.IO.File newDir = new Java.IO.File(outDir, "LakesideLounge");

            if (!newDir.Exists())
            {
                newDir.Mkdir();
                return;
            }

            //DirectoryInfo info = new DirectoryInfo(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/LakesideLounge/");
            DirectoryInfo info = new DirectoryInfo("/sdcard/LakesideLounge/");
            FileInfo[] files = info.GetFiles();

            List<TransactionFile> transactionFiles = new List<TransactionFile>();

            foreach (FileInfo file in files)
            {
                if (file.Name.StartsWith(prefix))
                    if (!Database.TransactionFileExists(file.Name))
                    {
                        TransactionFile newFile = new TransactionFile(file.Name, file.FullName);
                        transactionFiles.Add(newFile);

                        Database.AddTransactionFile(file.Name);
                    }
            }

            transactionFiles.Sort();

            foreach (TransactionFile file in transactionFiles)
                if (prefix == "Orders")
                    ProcessOrdersTransactionFile(file.FullName);
                else if (prefix == "Updates")
                    ProcessUpdatesTransactionFile(file.FullName);
        }

        private static void ProcessUpdatesTransactionFile(string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open);
            StreamReader reader = new StreamReader(fs);

            while (!reader.EndOfStream)
            {
                string transaction = reader.ReadLine();
                //Database.ProcessUpdate(transaction);
            }

            reader.Close();
            fs.Close();
        }

        private static void ProcessOrdersTransactionFile(string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open);
            StreamReader reader = new StreamReader(fs);

            OrderModel model = null;
            OrderItemModel itemModel = null;
            OrderItemComponentModel componentModel = null;

            bool onFirstOrder = true;

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (line.StartsWith("SAVE_ORDER"))
                {
                    if (!onFirstOrder)
                        Database.SaveOrder(model, false, false);

                    onFirstOrder = false;

                    model = new OrderModel();
                    int firstComma = line.IndexOf(",");
                    int secondComma = line.IndexOf(",", firstComma + 1);
                    model.Id = Int32.Parse(line.Substring(firstComma + 1, secondComma - firstComma - 1));

                    int thirdComma = line.IndexOf(",", secondComma + 1);
                    model.Name = line.Substring(secondComma + 1, thirdComma - secondComma - 1);

                    int fourthComma = line.IndexOf(",", thirdComma + 1);
                    model.CustomerType = Int32.Parse(line.Substring(thirdComma + 1, fourthComma - thirdComma - 1));

                    model.Date = DateTime.Parse(line.Substring(fourthComma + 1, line.Length - fourthComma - 1));
                }
                else if (line.StartsWith("ADD_ORDER_ITEM"))
                {
                    int firstComma = line.IndexOf(",");
                    int secondComma = line.IndexOf(",", firstComma + 1);
                    int id = Int32.Parse(line.Substring(firstComma + 1, secondComma - firstComma - 1));

                    int thirdComma = line.IndexOf(",", secondComma + 1);
                    int orderId = Int32.Parse(line.Substring(secondComma + 1, thirdComma - secondComma - 1));

                    int fourthComma = line.IndexOf(",", thirdComma + 1);
                    int variationId = Int32.Parse(line.Substring(thirdComma + 1, fourthComma - thirdComma - 1));

                    int fifthComma = line.IndexOf(",", fourthComma + 1);
                    int inOutStatus = Int32.Parse(line.Substring(fourthComma + 1, fifthComma - fourthComma - 1));

                    int sixthComma = line.IndexOf(",", fifthComma + 1);
                    int discountId = Int32.Parse(line.Substring(fifthComma + 1, sixthComma - fifthComma - 1));

                    State state = (State)Int32.Parse(line.Substring(sixthComma + 1, line.Length - sixthComma - 1));

                    itemModel = new OrderItemModel(id, orderId, variationId, inOutStatus, discountId, state);
                    model.OrderItems.Add(itemModel);
                }
                else if (line.StartsWith("ADD_ORDERITEM_COMPONENT"))
                {
                    int firstComma = line.IndexOf(",");
                    int secondComma = line.IndexOf(",", firstComma + 1);
                    int id = Int32.Parse(line.Substring(firstComma + 1, secondComma - firstComma - 1));

                    int thirdComma = line.IndexOf(",", secondComma + 1);
                    int orderItemId = Int32.Parse(line.Substring(secondComma + 1, thirdComma - secondComma - 1));

                    int fourthComma = line.IndexOf(",", thirdComma + 1);
                    int componentId = Int32.Parse(line.Substring(thirdComma + 1, fourthComma - thirdComma - 1));
                    int portions = Int32.Parse(line.Substring(fourthComma + 1, line.Length - fourthComma - 1));

                    ComponentModel component = Database.GetComponentModel(componentId);
                    component.Portions = portions;

                    componentModel = new OrderItemComponentModel(id, orderItemId, componentId, component.Name, component.DisplayName, component.Cost, component.Price, portions, 0);
                    itemModel.ComponentModels.Add(componentModel);
                }
                else if(line.StartsWith("ADD_ORDERITEM_SUBCOMPONENT"))
                {
                    int firstComma = line.IndexOf(",");
                    int secondComma = line.IndexOf(",", firstComma + 1);
                    int parentId = Int32.Parse(line.Substring(firstComma + 1, secondComma - firstComma - 1));

                    int thirdComma = line.IndexOf(",", secondComma + 1);
                    int componentId = Int32.Parse(line.Substring(secondComma + 1, thirdComma - secondComma - 1));
                    
                    int portions = Int32.Parse(line.Substring(thirdComma + 1, line.Length - thirdComma - 1));

                    ComponentModel component = Database.GetComponentModel(componentId);
                    component.Portions = portions;

                    OrderItemComponentComponentModel subComponentModel = new OrderItemComponentComponentModel(parentId, componentId, component.Name, component.DisplayName, component.Cost, component.Price, portions, component.IsDefault);
                    componentModel.AddComponent(subComponentModel);
                }
                else if (line.StartsWith("SET_CURRENT_ORDER"))
                {
                    if (!onFirstOrder)
                    {
                        Database.SaveOrder(model, false, false);
                        onFirstOrder = true;
                    }

                    int firstComma = line.IndexOf(",");
                    int orderId = Int32.Parse(line.Substring(firstComma + 1, line.Length - firstComma - 1));

                    Database.SetCurrentOrder(orderId, false);
                }
                else if (line.StartsWith("CLEAR_CURRENT_ORDER"))
                {
                    if (!onFirstOrder)
                    {
                        Database.SaveOrder(model, false, false);
                        onFirstOrder = true;
                    }

                    Database.ClearCurrentOrder(false);
                }
                else if (line.StartsWith("PAY_ORDER"))
                {
                    int firstComma = line.IndexOf(",");
                    int orderId = Int32.Parse(line.Substring(firstComma + 1, line.Length - firstComma - 1));

                    Database.SaveOrder(model, false, false);

                    onFirstOrder = true;

                    Database.PayOrder(model, false);
                }
                else if (line.StartsWith("DELETE_ORDER"))
                {
                    int firstComma = line.IndexOf(",");
                    int orderId = Int32.Parse(line.Substring(firstComma + 1, line.Length - firstComma - 1));

                    if (!onFirstOrder)
                    {
                        Database.SaveOrder(model, false, false);
                        onFirstOrder = true;
                    }

                    OrderModel orderModel = new OrderModel(orderId);
                    Database.DeleteOrder(orderModel, false, false);
                }
                else if(line.StartsWith("ORDER_UPLOADED"))
                {
                    int firstComma = line.IndexOf(",");
                    int orderId = Int32.Parse(line.Substring(firstComma + 1, line.Length - firstComma - 1));

                    if (!onFirstOrder)
                    {
                        Database.SaveOrder(model, false, false);
                        onFirstOrder = true;
                    }

                    Database.OrderUploaded(orderId, false);
                }
            }

            if (!onFirstOrder)
                Database.SaveOrder(model, false, false);

            reader.Close();
            fs.Close();
        }

        private static void StoreTransaction(string transaction)
        {
            //FileStream fs = new FileStream(currentTransactionFile, FileMode.Append);
            //StreamWriter sw = new StreamWriter(fs);

            //sw.WriteLine(transaction);
            //sw.Flush();
            //sw.Close();

            //fs.Close();
        }

        public static void StoreUpdate(string update)
        {
            //FileStream fs = new FileStream(currentUpdatesFile, FileMode.Append);
            //StreamWriter sw = new StreamWriter(fs);

            //sw.WriteLine(update);
            //sw.Flush();
            //sw.Close();

            //fs.Close();
        }

        public static void SaveOrder(OrderModel model)
        {
            string line = "SAVE_ORDER," + model.Id.ToString() + "," + model.Name + "," + model.CustomerType.ToString()+ "," + model.Date.ToShortDateString();
            StoreTransaction(line);
        }

        public static void DeleteOrder(OrderModel model)
        {
            string line = "DELETE_ORDER," + model.Id;
            StoreTransaction(line);
        }

        public static void AddOrderItem(OrderItemModel model)
        {
            string line = "ADD_ORDER_ITEM," + model.Id.ToString() + "," + model.OrderId.ToString() + "," + model.VariationId.ToString() + "," + model.InOutStatus.ToString() + "," + model.DiscountId.ToString() + "," + model.State.ToString();
            StoreTransaction(line);
        }

        public static void AddOrderItemComponent(OrderItemComponentModel component)
        {
            string line = "ADD_ORDERITEM_COMPONENT," + component.Id.ToString() + "," + component.OrderItemId.ToString() + "," + component.ComponentId.ToString() + "," + component.Portions;
            StoreTransaction(line);
        }
        
        public static void AddOrderItemComponentComponent(OrderItemComponentComponentModel subComponent)
        {
            string line = "ADD_ORDERITEM_SUBCOMPONENT," + subComponent.ParentId + "," + subComponent.ComponentId + "," + subComponent.Portions;
            StoreTransaction(line);
        }

        public static void SetCurrentOrder(int id)
        {
            string line = "SET_CURRENT_ORDER," + id.ToString();
            StoreTransaction(line);
        }

        public static void ClearCurrentOrder()
        {
            string line = "CLEAR_CURRENT_ORDER";
            StoreTransaction(line);
        }

        public static void PayOrder(OrderModel model)
        {
            string line = "PAY_ORDER," + model.Id.ToString();
            StoreTransaction(line);
        }

        public static void OrderUploaded(int id)
        {
            string line = "ORDER_UPLOADED," + id.ToString();
            StoreTransaction(line);
        }

        public static void DeleteTransactionFiles()
        {
            //Java.IO.File outDir = Android.OS.Environment.ExternalStorageDirectory;
            Java.IO.File outDir = new Java.IO.File("/sdcard/");
            Java.IO.File newDir = new Java.IO.File(outDir, "LakesideLounge");

            if (!newDir.Exists())
                return;

            DirectoryInfo info = new DirectoryInfo("/sdcard/LakesideLounge/");
            FileInfo[] files = info.GetFiles();

            foreach(FileInfo file in files)
                file.Delete();
        }
    }
}