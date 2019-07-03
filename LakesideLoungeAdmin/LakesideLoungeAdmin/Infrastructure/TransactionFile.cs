using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LakesideLoungeAdmin.Infrastructure
{
    public class TransactionFile : IComparable<TransactionFile>
    {
        string name;
        string fullName;

        public TransactionFile(string name, string fullName)
        {
            this.name = name;
            this.fullName = fullName;
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public string FullName
        {
            get
            {
                return fullName;
            }
        }

        public int CompareTo(TransactionFile file)
        {
            string name = file.Name;

            int firstScorePos = name.IndexOf('_');
            int secondScorePos = name.IndexOf('_', firstScorePos + 1);

            string date = name.Substring(firstScorePos + 1, secondScorePos - firstScorePos - 1).Replace('-', '/');
            int version = Int32.Parse(name.Substring(secondScorePos + 1, name.Length - secondScorePos - 5));

            int localFirstScorePos = this.name.IndexOf('_');
            int localSecondScorePos = this.name.IndexOf('_', localFirstScorePos + 1);

            string localDate = this.name.Substring(localFirstScorePos + 1, localSecondScorePos - localFirstScorePos - 1).Replace('-', '/');
            int localVersion = Int32.Parse(this.name.Substring(localSecondScorePos + 1, this.name.Length - localSecondScorePos - 5));

            if (DateTime.Parse(localDate) > DateTime.Parse(date))
                return 1;
            else if (DateTime.Parse(localDate) < DateTime.Parse(date))
                return -1;

            if (localVersion > version)
                return 1;

            return -1;
        }
    }
}