using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;
using System.IO;

using LakesideLoungeAdmin.Helpers;
using LakesideLoungeAdmin.Application;
using LakesideLoungeAdmin.Domain;

namespace LakesideLoungeAdmin.Infrastructure
{
    public static class Log
    {
        private static string currentTransactionFile;

        public static void CreateTransactionFile()
        {
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Logs"))
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\Logs");

            DirectoryInfo info = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "\\Logs");
            FileInfo[] files = info.GetFiles();

            int maxVersion = 0;
            string todaysDate = Helper.FormatDate(DateTime.Now);
            todaysDate = todaysDate.Replace('/', '-');

            foreach (FileInfo file in files)
            {
                if (file.Name.StartsWith("Updates"))
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

            string newFile = "Updates_" + todaysDate.ToString() + "_" + (maxVersion + 1).ToString("00") + ".log";
            currentTransactionFile = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\" + newFile;

            FileStream fs = new FileStream(currentTransactionFile, FileMode.Create);
            fs.Close();
        }

        public static void SendTransactions()
        {
            DirectoryInfo info = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "\\Logs");
            FileInfo[] files = info.GetFiles();

            List<TransactionFile> transactionFiles = new List<TransactionFile>();

            foreach (FileInfo file in files)
            {
                if (file.Name.StartsWith("Updates"))
                    if (!Database.TransactionFileUploaded(file.Name))
                    {
                        TransactionFile newFile = new TransactionFile(file.Name, file.FullName);
                        transactionFiles.Add(newFile);

                        Database.AddUploadedTransactionFile(file.Name);
                    }
            }

            transactionFiles.Sort();

            Network.SendMessage("UPDATES_START");

            foreach (TransactionFile file in transactionFiles)
                SendTransactionFile(file.FullName);

            Network.SendMessage("UPDATES_END");
        }

        private static void SendTransactionFile(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Open);
            StreamReader sr = new StreamReader(fs);

            List<string> messages = new List<String>();

            while (!sr.EndOfStream)
            {
                string transaction = sr.ReadLine();
                messages.Add(transaction);
            }

            sr.Close();
            fs.Close();

            Network.SendMessages(messages);
        }

        private static void StoreTransaction(string transaction)
        {
            FileStream fs = new FileStream(currentTransactionFile, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);

            sw.WriteLine(transaction);
            sw.Flush();
            sw.Close();

            fs.Close();
        }

        public static void AddVariationComponent(int variationId, int componentId)
        {
            string line = "ADD_VARIATION_COMPONENT," + variationId.ToString() + "," + componentId;
            StoreTransaction(line);
        }

        public static void UpdateVariationComponent(int variationId, int componentId, bool isDefault)
        {
            string line = "UPDATE_VARIATION_COMPONENT," + variationId.ToString() + "," + componentId + "," + isDefault.ToString();
            StoreTransaction(line);
        }

        public static void RemoveVariationComponent(int variationId, int componentId)
        {
            string line = "REMOVE_VARIATION_COMPONENT," + variationId.ToString() + "," + componentId;
            StoreTransaction(line);
        }

        public static void DeleteTransactionFiles()
        {
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Logs"))
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\Logs");

            DirectoryInfo info = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "\\Logs");
            FileInfo[] files = info.GetFiles();

            foreach(FileInfo file in files)
                file.Delete();
        }
    }
}