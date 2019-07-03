using Android.App;
using Android.Widget;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.IO;
using System.Xml;
using Mono.Data.Sqlite;
using Android.Content;

using LakesideLoungeAndroid.Application;
using LakesideLoungeAndroid.Domain;

namespace LakesideLoungeAndroid.Infrastructure
{
    public static class Database
    {
        private static SqliteConnection connection = new SqliteConnection();
        private static string fileName = "LakesideLoungeDB.db3";

        static int variationComponentId = 1;

        static Database()
        {
            connection.ConnectionString = "Data Source = " + System.Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/" + fileName + ";Version=3;";
        }

        public static void InitializeDatabase(Context context)
        {
            SqliteConnection.CreateFile(System.Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/" + fileName);
            connection.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;

            string query = "create table Orders (Id integer primary key not null, Name text, CustomerType integer, Live boolean, Current boolean, Date datetime, Uploaded boolean, OrderNumber integer)";
            command.CommandText = query;
            command.ExecuteNonQuery();

            query = "create table QueuedOrders (Id integer primary key not null, OriginalId integer, Name text, CustomerType integer, Date datetime, Void boolean, OrderNumber integer)";
            command.CommandText = query;
            command.ExecuteNonQuery();

            query = "create table OrderItems (Id integer primary key not null, OrderId integer, VariationId integer, InOutStatus integer, Discount integer, State integer)";
            command.CommandText = query;
            command.ExecuteNonQuery();

            query = "create table QueuedOrderItems (Id integer primary key not null, QueuedOrderId integer, OriginalId integer, OriginalOrderId integer, VariationId integer)";
            command.CommandText = query;
            command.ExecuteNonQuery();

            query = "create table Variations (Id integer primary key not null, ParentId integer not null, Name text not null, DisplayName text, Points real, PointPrice money, Position integer, Removed boolean)";
            command.CommandText = query;
            command.ExecuteNonQuery();

            query = "create table VariationComponents (Id integer primary key not null, VariationId integer, ComponentId integer, IsDefault boolean, Portions integer, SelectionGroup integer, Points real, Position integer)";
            command.CommandText = query;
            command.ExecuteNonQuery();

            query = "create table ComponentComponents (ParentId integer, ComponentId integer, IsDefault boolean, SelectionGroup integer, Position integer)";
            command.CommandText = query;
            command.ExecuteNonQuery();

            query = "create table Components (Id integer primary key not null, Name text not null, DisplayName text)";
            command.CommandText = query;
            command.ExecuteNonQuery();

            query = "create table OrderItemComponents (Id integer primary key not null, OrderItemId integer not null, VariationId integer, ComponentId integer not null, Portions integer, Position integer)";
            command.CommandText = query;
            command.ExecuteNonQuery();

            query = "create table QueuedOrderItemComponents (Id integer primary key not null, QueuedOrderItemId integer, OriginalId, OriginalItemId integer, OriginalOrderId integer, VariationId integer, ComponentId integer, Portions integer, Position integer)";
            command.CommandText = query;
            command.ExecuteNonQuery();

            query = "create table OrderItemComponentComponents (OrderItemComponentsId integer not null, ComponentId integer not null, Portions integer)";
            command.CommandText = query;
            command.ExecuteNonQuery();
            
            query = "create table QueuedOrderItemComponentComponents (QueuedOrderItemComponentsId integer, OriginalItemComponentsId integer, ComponentId integer, Portions integer)";
            command.CommandText = query;
            command.ExecuteNonQuery();

            query = "create table Prices (Id integer primary key not null, ParentType integer, ParentId integer not null, StartDate datetime not null, EndDate datetime null, Cost money null, Price money null)";
            command.CommandText = query;
            command.ExecuteNonQuery();

            query = "create table Discounts (Id integer primary key not null, Description text null, DiscountType integer, Discount money not null)";
            command.CommandText = query;
            command.ExecuteNonQuery();

            query = "create table CustomerTypes (Id integer primary key not null, Description text null)";
            command.CommandText = query;
            command.ExecuteNonQuery();

            query = "create table Transactions (Filename text)";
            command.CommandText = query;
            command.ExecuteNonQuery();

            query = "create table Ingredients (Id integer, Name text, DisplayName text)";
            command.CommandText = query;
            command.ExecuteNonQuery();

            query = "create table ItemIngredients (ItemId integer, ItemType integer, IngredientId integer, Portions integer)";
            command.CommandText = query;
            command.ExecuteNonQuery();

            query = "create table Costs (IngredientId integer, StartDate datetime, Cost money)";
            command.CommandText = query;
            command.ExecuteNonQuery();

            query = "create table Updates (Id integer, UpdateText text)";
            command.CommandText = query;
            command.ExecuteNonQuery();

            InsertIngredients(context);
            InsertComponents(context);
            InsertVariations(context);
            InsertDiscounts();
            InsertCustomerTypes();

            connection.Close();
        }

        private static void InsertDiscounts()
        {
            InsertDiscount(1, "No Discount", 0, 0);
            InsertDiscount(2, "50% Discount", 0, 50);
            InsertDiscount(3, "100% Discount", 0, 100);
            InsertDiscount(4, "Escort Discount", 1, 0.95M);
        }

        private static void InsertDiscount(int id, string description, int type, decimal discount)
        {
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;

            string query = "insert into Discounts (Id, Description, DiscountType, Discount) Values (" + id.ToString() + ", \"" + description + "\", " + type.ToString() + ", " + discount.ToString() + ")";
            command.CommandText = query;

            command.ExecuteNonQuery();
        }

        private static void InsertCustomerTypes()
        {
            InsertCustomerType(1, "Patient");
            InsertCustomerType(2, "Staff");
            InsertCustomerType(3, "Other");
        }

        private static void InsertCustomerType(int id, string description)
        {
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;

            string query = "insert into CustomerTypes (Id, Description) Values (" + id.ToString() + ", \"" + description + "\")";
            command.CommandText = query;

            command.ExecuteNonQuery();
        }

        private static void InsertComponents(Context context)
        {
            Stream input = context.Assets.Open("Components.xml");

            XmlDocument doc = new XmlDocument();
            doc.Load(input);
            input.Close();

            foreach (XmlNode node in doc.ChildNodes[1])
                InsertComponent(node);
        }

        private static void InsertIngredients(Context context)
        {
            Stream input = context.Assets.Open("Ingredients.xml");

            XmlDocument doc = new XmlDocument();
            doc.Load(input);
            input.Close();

            XmlNode rootNode = doc.ChildNodes[1];

            foreach (XmlNode node in rootNode.ChildNodes)
            {
                int id = Int32.Parse(node.Attributes.GetNamedItem("Id").Value);
                string name = node.Attributes.GetNamedItem("Name").Value;
                string displayName = node.Attributes.GetNamedItem("DisplayName").Value;

                InsertIngredient(id, name, displayName);

                foreach (XmlNode costNode in node.FirstChild.ChildNodes)
                    InsertCost(id, costNode);
            }
        }

        private static void InsertCost(int ingredientId, XmlNode node)
        {
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;

            string startDate = node.Attributes.GetNamedItem("StartDate").Value;
            decimal cost = decimal.Parse(node.Attributes.GetNamedItem("Cost").Value);

            string query = "insert into Costs (IngredientId, StartDate, Cost) Values (" + ingredientId.ToString() + ", \"" + startDate + "\", " + cost + ")";
            command.CommandText = query;

            command.ExecuteNonQuery();
        }

        private static void InsertIngredient(int id, string name, string displayName)
        {
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;

            string query = "insert into Ingredients (Id, Name, DisplayName) Values (" + id.ToString() + ", \"" + name + "\", \"" + displayName + "\")";
            command.CommandText = query;

            command.ExecuteNonQuery();
        }

        private static void InsertVariations(Context context)
        {
            Stream input = context.Assets.Open("Variations.xml");

            XmlDocument doc = new XmlDocument();
            doc.Load(input);
            input.Close();

            InsertVariation(0, doc.FirstChild, 1);
        }

        private static void InsertVariationComponent(int variationId, XmlNode node, int position)
        {
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;

            int id = Int32.Parse(node.Attributes.GetNamedItem("Id").Value);
            string isDefault = "False";

            if (node.Attributes.GetNamedItem("Default") != null && (node.Attributes.GetNamedItem("Default").Value == "true" || node.Attributes.GetNamedItem("Default").Value == "True"))
                isDefault = "True";

            int portions = 1;
            if (node.Attributes.GetNamedItem("Portions") != null)
                portions = int.Parse(node.Attributes.GetNamedItem("Portions").Value);

            int group = 0;
            if (node.Attributes.GetNamedItem("Group") != null)
                group = int.Parse(node.Attributes.GetNamedItem("Group").Value);

            decimal points = 0;
            if (node.Attributes.GetNamedItem("Points") != null)
                points = decimal.Parse(node.Attributes.GetNamedItem("Points").Value);

            string query = "insert into VariationComponents (Id, VariationId, ComponentId, IsDefault, Portions, SelectionGroup, Points, Position) values (" + variationComponentId + "," + variationId.ToString() + ", " + id.ToString() + ", \"" + isDefault + "\", " + portions + ", " + group + ", " + points + ", " + position + ")";
            command.CommandText = query;
            command.ExecuteNonQuery();
        }

        private static void InsertComponent(XmlNode node)
        {
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;

            int id = Int32.Parse(node.Attributes.GetNamedItem("Id").Value);
            string name = node.Attributes.GetNamedItem("Name").Value;
            string displayName = node.Attributes.GetNamedItem("DisplayName").Value;

            string query = "insert into Components (Id, Name, DisplayName) values (" + id.ToString() + ", \"" + name + "\", \"" + displayName + "\");";
            command.CommandText = query;
            command.ExecuteNonQuery();

            InsertPrices(id, 1, node.FirstChild.ChildNodes);

            if (node.ChildNodes.Count == 2 && node.ChildNodes[1].Name == "Components")
            {
                int comPosition = 1;
                foreach (XmlNode componentNode in node.ChildNodes[1].ChildNodes)
                {
                    InsertComponentComponent(id, componentNode, comPosition);
                    ++comPosition;
                }
            }
        }

        public static void AddVariation(int id, int parentId, string name, string displayName, decimal price, float points, decimal pointPrice, int position)
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;

            string query = "insert into Variations (Id, ParentId, Name, DisplayName, Points, PointPrice, Position, Removed) values (" + id.ToString() + ", " + parentId.ToString() + ", \"" + name + "\", \"" + displayName + "\", " + points + ", " + pointPrice + ", " + position + ", \"False\");";
            command.CommandText = query;
            command.ExecuteNonQuery();

            connection.Close();
        }
        
        private static void InsertVariation(int parentId, XmlNode node, int position)
        {
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;

            int id = Int32.Parse(node.Attributes.GetNamedItem("Id").Value);
            string name = node.Attributes.GetNamedItem("Name").Value;
            string displayName = node.Attributes.GetNamedItem("DisplayName").Value;

            decimal points = 0;
            decimal pointPrice = 0;

            if (node.Attributes.GetNamedItem("Points") != null)
            {
                points = decimal.Parse(node.Attributes.GetNamedItem("Points").Value);
                pointPrice = decimal.Parse(node.Attributes.GetNamedItem("PointPrice").Value);
            }
            
            string query = "insert into Variations (Id, ParentId, Name, DisplayName, Points, PointPrice, Position, Removed) values (" + id.ToString() + ", "+ parentId.ToString() + ", \"" + name + "\", \"" + displayName + "\", " + points + ", " + pointPrice + ", " + position + ", \"False\");";
            command.CommandText = query;
            command.ExecuteNonQuery();

            if (node.HasChildNodes && node.FirstChild.Name == "Variations")
            {
                int newPosition = 1;
                foreach (XmlNode childNode in node.FirstChild.ChildNodes)
                {
                    InsertVariation(id, childNode, newPosition);
                    ++newPosition;
                }

                if (node.ChildNodes.Count == 2 && node.ChildNodes[1].Name == "Prices")
                    InsertPrices(id, 0, node.ChildNodes[1].ChildNodes);
                else if(node.ChildNodes.Count == 3 && node.ChildNodes[1].Name == "Prices")
                    InsertPrices(id, 0, node.ChildNodes[1].ChildNodes);
            }
            else if (node.HasChildNodes && node.FirstChild.Name == "Components")
            {
                int varCompPosition = 1;
                foreach (XmlNode childNode in node.FirstChild.ChildNodes)
                {
                    InsertVariationComponent(id, childNode, varCompPosition);

                    variationComponentId++;
                    ++varCompPosition;
                }

                if (node.ChildNodes.Count == 2 && node.ChildNodes[1].Name == "Prices")
                    InsertPrices(id, 0, node.ChildNodes[1].ChildNodes);
                else if (node.ChildNodes.Count == 3 && node.ChildNodes[1].Name == "Prices")
                    InsertPrices(id, 0, node.ChildNodes[1].ChildNodes);
            }
            else if (node.HasChildNodes && node.FirstChild.Name == "Prices")
                InsertPrices(id, 0, node.FirstChild.ChildNodes);
        }

        private static void InsertComponentComponent(int id, XmlNode node, int position)
        {
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;

            int componentId = Int32.Parse(node.Attributes.GetNamedItem("Id").Value);
            string isDefault = "False";

            if (node.Attributes.GetNamedItem("Default") != null && (node.Attributes.GetNamedItem("Default").Value == "true" || node.Attributes.GetNamedItem("Default").Value == "True"))
                isDefault = "True";

            int group = 0;
            if (node.Attributes.GetNamedItem("Group") != null)
                group = int.Parse(node.Attributes.GetNamedItem("Group").Value);

            string query = "insert into ComponentComponents (ParentId, ComponentId, IsDefault, SelectionGroup, Position) values (" + id + ", " + componentId.ToString() + ", \"" + isDefault + "\", " + group + ", " + position + ")";
            command.CommandText = query;
            command.ExecuteNonQuery();
        }

        private static void InsertPrices(int id, int parentType, XmlNodeList nodes)
        {
            foreach (XmlNode childNode in nodes)
            {
                int priceId = Int32.Parse(childNode.Attributes.GetNamedItem("Id").Value);
                DateTime startDate = DateTime.Parse(childNode.Attributes.GetNamedItem("StartDate").Value);
                decimal cost = decimal.Parse(childNode.Attributes.GetNamedItem("Cost").Value);
                decimal price = decimal.Parse(childNode.Attributes.GetNamedItem("Price").Value);

                if (!(childNode.Attributes.GetNamedItem("EndDate") == null))
                    InsertPrice(priceId, parentType, id, startDate, DateTime.Parse(childNode.Attributes.GetNamedItem("EndDate").Value), cost, price);
                else
                    InsertPrice(priceId, parentType, id, startDate, null, cost, price);
            }
        }

        private static void InsertPrice(int id, int parentType, int variationId, DateTime startDate, DateTime? endDate, decimal cost, decimal price)
        {
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;

            string query;

            if (endDate == null)
                query = "insert into Prices (Id, ParentType, ParentId, StartDate, Cost, Price) values (" + id.ToString() + ", " + parentType.ToString() + ", " + variationId.ToString() + ", \"" + startDate.ToString() + "\", " + cost + ", " + price + ");";
            else
                query = "insert into Prices (Id, ParentType, ParentId, StartDate, EndDate, Cost, Price) values (" + id.ToString() + ", " + parentType.ToString() + ", " + variationId.ToString() + ", \"" + startDate.ToString() + "\", \"" + endDate.ToString() + "\", " + cost + ", " + price + ");";

            command.CommandText = query;
            command.ExecuteNonQuery();
        }

        //private static void AddOrder(int id, string name, Boolean live, Boolean current, bool log)
        //{
        //    SqliteCommand command = new SqliteCommand();
        //    command.Connection = connection;
        //    command.CommandType = CommandType.Text;

        //    string query = "insert into Orders (Id, Name, Live, Current) values (" + id.ToString() + ", \"" + name + "\", \"" + live.ToString() + "\", \"" + current.ToString() + "\")";
        //    command.CommandText = query;
        //    command.ExecuteNonQuery();

        //    if (log)
        //        Log.AddOrder(id, name, live, current);
        //}

        private static void AddOrderItem(OrderItemModel model, bool log, bool bluetooth)
        {
            SqliteConnection connection2 = new SqliteConnection();
            connection2.ConnectionString = "Data Source = " + System.Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/" + fileName + ";Version=3;";

            connection2.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection2;
            command.CommandType = CommandType.Text;
            command.CommandText = "delete from OrderItemComponents where OrderItemId = " + model.Id;
            //command.ExecuteNonQuery();

            string state = ((int)model.State).ToString();
            string query = "insert into OrderItems (Id, OrderId, VariationId, InOutStatus, Discount, State) values (" + model.Id.ToString() + ", " + model.OrderId.ToString() + ", " + model.VariationId.ToString() + ", " + model.InOutStatus + ", " + model.DiscountId.ToString() + ", " + state + ")";
            command.CommandText = query;
            command.ExecuteNonQuery();

            if (log)
                Log.AddOrderItem(model);

            if (bluetooth)
                BlueTooth.AddOrderItem(model);

            foreach (OrderItemComponentModel componentModel in model.ComponentModels)
            {
                query = "insert into OrderItemComponents (Id, OrderItemId, VariationId, ComponentId, Portions, Position) values (" + componentModel.Id.ToString() + ", " + model.Id.ToString() + ", " + model.VariationId + ", " + componentModel.ComponentId.ToString() + ", " + componentModel.Portions.ToString() + ", " + componentModel.Position.ToString() + ")";
                command.CommandText = query;
                command.ExecuteNonQuery();

                if (log)
                    Log.AddOrderItemComponent(componentModel);

                if (bluetooth)
                    BlueTooth.AddOrderItemComponent(componentModel);

                command.CommandText = "delete from OrderItemComponentComponents where OrderItemComponentsId = " + componentModel.Id;
                //command.ExecuteNonQuery();

                foreach (OrderItemComponentComponentModel subComponentModel in componentModel.Components)
                {
                    query = "insert into OrderItemComponentComponents (OrderItemComponentsId, ComponentId, Portions) values (" + componentModel.Id.ToString() + ", " + subComponentModel.ComponentId.ToString() + ", " + subComponentModel.Portions.ToString() + ")";
                    command.CommandText = query;
                    command.ExecuteNonQuery();

                    if (log)
                        Log.AddOrderItemComponentComponent(subComponentModel);

                    if (bluetooth)
                        BlueTooth.AddOrderItemComponentComponent(subComponentModel);
                }

                //OrderItemComponent newComponent = new OrderItemComponent(newId, model.Id, componentModel.Id, componentModel.Portions);
                //components.Add(newComponent);

                //++newId;
            }

            connection2.Close();
        }

        public static bool DeleteOrder(OrderModel model, bool log, bool bluetooth)
        {
            if (bluetooth)
            {
                if (BlueTooth.Test())
                    return true;
            }

            SqliteConnection connection2 = new SqliteConnection();
            connection2.ConnectionString = "Data Source = " + System.Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/" + fileName + ";Version=3;";

            connection2.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection2;

            string query;

            foreach (OrderItemModel item in model.OrderItems)
            {
                query = "delete from OrderItemComponents where OrderItemId = " + item.Id;
                command.CommandText = query;
                command.ExecuteNonQuery();

                foreach (OrderItemComponentModel component in item.ComponentModels)
                {
                    query = "delete from OrderItemComponentComponents where OrderItemComponentsId = " + component.Id;
                    command.CommandText = query;
                    command.ExecuteNonQuery();
                }
            }

            query = "delete from OrderItems where OrderId = " + model.Id;
            command.CommandText = query;
            command.ExecuteNonQuery();

            query = "delete from Orders where Id = " + model.Id;
            command.CommandText = query;
            command.ExecuteNonQuery();
            
            connection2.Close();

            if (log)
                Log.DeleteOrder(model);

            if(bluetooth)
                BlueTooth.DeleteOrder(model);

            return false;
        }

        public static int GetCurrentOrderId()
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "select Id from Orders where Current = \"True\"";

            SqliteDataReader reader = command.ExecuteReader();

            int currentOrderId;

            if (reader.Read())
                currentOrderId = reader.GetInt32(0);
            else
                currentOrderId = GetNewOrderId();
            
            connection.Close();

            return currentOrderId;
        }

        public static int GetNewOrderId()
        {
            SqliteConnection connection2 = new SqliteConnection();
            connection2.ConnectionString = "Data Source = " + System.Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/" + fileName + ";Version=3;";

            connection2.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection2;
            command.CommandType = CommandType.Text;
            command.CommandText = "select max(Id) from Orders";

            SqliteDataReader reader = command.ExecuteReader();

            int newId;

            reader.Read();

            try
            {
                newId = reader.GetInt32(0) + 1;
            }
            catch (InvalidCastException)
            {
                newId = 1;
            }

            connection2.Close();

            return newId;
        }

        public static Variation GetVariation(int id)
        {
            SqliteConnection connection2 = new SqliteConnection();
            connection2.ConnectionString = "Data Source = " + System.Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/" + fileName + ";Version=3;";

            connection2.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection2;
            command.CommandType = CommandType.Text;
            command.CommandText = "select Id, ParentId, Name, DisplayName, Points, PointPrice, Position from Variations where id = " + id.ToString() + " and Removed = \"False\"";

            SqliteDataReader reader = command.ExecuteReader();

            int parentId = 0;
            string variationName = "";
            string variationDisplayName = "";
            decimal variationCost;
            decimal variationPrice;
            float points = 0;
            decimal pointPrice = 0;
            int position = 1;

            if (reader.Read())
            {
                parentId = reader.GetInt32(1);
                variationName = reader.GetString(2);
                variationDisplayName = reader.GetString(3);
                //variationCost = reader.GetDecimal(4);
                //variationPrice = reader.GetDecimal(5);
                points = reader.GetFloat(4);
                pointPrice = reader.GetDecimal(5);
                position = reader.GetInt32(6);
            }
            else
                throw new Exception("Invalid Variation!");

            reader.Close();
            
            GetItemPrices(0, id, out variationCost, out variationPrice);

            connection2.Close();

            Variation newVariation = new Variation(id, parentId, variationName, variationDisplayName, variationCost, variationPrice, points, pointPrice, position);
            GetSubVariations(ref newVariation);

            if (newVariation.HasVariations())
                return newVariation;
            else
                GetComponents(ref newVariation);

            return newVariation;
        }

        private static void GetItemPrices(int parentType, int itemId, out decimal cost, out decimal price)
        {
            SqliteConnection connection2 = new SqliteConnection();
            connection2.ConnectionString = "Data Source = " + System.Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/" + fileName + ";Version=3;";

            connection2.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection2;
            command.CommandType = CommandType.Text;
            //command.CommandText = "select Cost, Price from Prices where ParentId = " + itemId + " and EndDate = ''";
            command.CommandText = "select Price from Prices where ParentId = " + itemId + " and ParentType = " + parentType.ToString() + " Order By StartDate Desc";

            SqliteDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                //cost = reader.GetDecimal(0);
                cost = 0;
                price = reader.GetDecimal(0);
            }
            else
            {
                cost = 0;
                price = 0;
            }

            reader.Close();
            connection2.Close();
        }

        private static void GetSubVariations(ref Variation variation)
        {
            SqliteConnection connection2 = new SqliteConnection();
            connection2.ConnectionString = "Data Source = " + System.Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/" + fileName + ";Version=3;";

            connection2.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection2;
            command.CommandType = CommandType.Text;
            command.CommandText = "select Id, ParentId, Name, DisplayName, Points, PointPrice, Position from Variations where ParentId = " + variation.Id.ToString() + " and Removed = \"False\"";

            SqliteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int variationId = reader.GetInt32(0);
                int parentId = reader.GetInt32(1);
                string variationName = reader.GetString(2);
                string variationDisplayName = reader.GetString(3);
                decimal variationCost;
                decimal variationPrice;

                //At the moment, points are only one level deep...
                float points = 0;
                decimal pointPrice = 0;
                int position = reader.GetInt32(6);

                GetItemPrices(0, variationId, out variationCost, out variationPrice);

                variation.AddVariation(variationId, parentId, variationName, variationDisplayName, variationCost, variationPrice, points, pointPrice, position);
            }

            reader.Close();
            connection2.Close();
        }

        public static Component GetComponent(int componentId)
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "select Name, DisplayName from Components Where Id = " + componentId.ToString();

            SqliteDataReader reader = command.ExecuteReader();
            reader.Read();

            //int componentId = reader.GetInt32(0);
            string name = reader.GetString(0);
            string displayName = reader.GetString(1);
            decimal cost = 0;// = reader.GetDecimal(3);
            decimal price = 0;// = reader.GetDecimal(4);
            //bool isDefault = bool.Parse(reader.GetString(2));

            GetItemPrices(1, componentId, out cost, out price);

            reader.Close();
            connection.Close();

            Component newComponent = new Component(componentId, variationComponentId, name, displayName, cost, price, 1, false, 0, 0, 0);

            //command.CommandType = CommandType.Text;
            //command.CommandText = "select Components.Id, Components.Name, Components.DisplayName, ComponentComponents.IsDefault from ComponentComponents inner join Components on ComponentComponents.ComponentId = Components.Id Where ComponentComponents.VariationComponentId = " + variationComponentId.ToString();

            //reader = command.ExecuteReader();

            //while (reader.Read())
            //{
            //    int id = reader.GetInt32(0);
            //    int parentId = variationComponentId;
            //   string componentName = reader.GetString(1);
            //    string componentDisplayName = reader.GetString(2);
            //    decimal componentCost = 0;// = reader.GetDecimal(3);
            //    decimal componentPrice = 0;// = reader.GetDecimal(4);
            //    bool componentIsDefault = bool.Parse(reader.GetString(3));

            //    GetItemPrices(1, id, out componentCost, out componentPrice);

            //    newComponent.AddComponent(id, componentId, componentName, componentDisplayName, componentCost, componentPrice, 1, componentIsDefault);
            //}

            //reader.Close();
            //connection.Close();

            GetSubComponents(componentId, ref newComponent);

            return newComponent;
        }

        public static ComponentModel GetComponentModel(int id)
        {
            SqliteConnection connection2 = new SqliteConnection();
            connection2.ConnectionString = "Data Source = " + System.Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/" + fileName + ";Version=3;";

            connection2.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection2;
            command.CommandType = CommandType.Text;
            //command.CommandText = "select VariationComponents.Id, VariationComponents.VariationId, Components.Name, Components.DisplayName, VariationComponents.IsDefault from VariationComponents inner join Components on VariationComponents.ComponentId = Components.Id where Components.Id = " + id.ToString();
            command.CommandText = "select Name, DisplayName from Components where Id = " + id.ToString();

            SqliteDataReader reader = command.ExecuteReader();

            Component component = null;

            if (reader.Read())
            {
                //int variationComponentId = reader.GetInt32(0);
                //int parentId = reader.GetInt32(1);
                string componentName = reader.GetString(0);
                string componentDisplayName = reader.GetString(1);
                decimal componentCost = 0;// = reader.GetDecimal(3);
                decimal componentPrice = 0;// = reader.GetDecimal(4);
                //bool isDefault = bool.Parse(reader.GetString(4));
                //int portions = reader.GetInt32(5);

                GetItemPrices(1, id, out componentCost, out componentPrice);

                component = new Component(id, 0, componentName, componentDisplayName, componentCost, componentPrice, 1, false, 0, 0, 0);
                GetSubComponents(id, ref component);
            }
            else
                throw new Exception("Invalid Component Requested");

            ComponentModel newComponentModel = new ComponentModel(component);

            connection2.Close();

            return newComponentModel;
        }

        private static void GetComponents(ref Variation variation)
        {
            SqliteConnection connection2 = new SqliteConnection();
            connection2.ConnectionString = "Data Source = " + System.Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/" + fileName + ";Version=3;";

            connection2.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection2;
            command.CommandType = CommandType.Text;
            command.CommandText = "select VariationComponents.Id, Components.Id, VariationComponents.VariationId, Components.Name, Components.DisplayName, VariationComponents.IsDefault, VariationComponents.Portions, VariationComponents.SelectionGroup, VariationComponents.Points, VariationComponents.Position from VariationComponents inner join Components on VariationComponents.ComponentId = Components.Id where VariationComponents.VariationId = " + variation.Id.ToString();

            SqliteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int variationComponentId = reader.GetInt32(0);
                int componentId = reader.GetInt32(1);
                int parentId = reader.GetInt32(2);
                string componentName = reader.GetString(3);
                string componentDisplayName = reader.GetString(4);
                decimal componentCost;
                decimal componentPrice;
                bool isDefault = bool.Parse(reader.GetString(5));
                int portions = reader.GetInt32(6);
                int group = reader.GetInt32(7);
                float points = reader.GetFloat(8);
                int position = reader.GetInt32(9);

                GetItemPrices(1, componentId, out componentCost, out componentPrice);
                
                Component newComponent = variation.AddComponent(componentId, parentId, componentName, componentDisplayName, componentCost, componentPrice, portions, isDefault, group, points, position);
                GetSubComponents(componentId, ref newComponent);
            }

            reader.Close();
            connection2.Close();
        }

        private static void GetSubComponents(int parentId, ref Component component)
        {
            SqliteConnection connection2 = new SqliteConnection();
            connection2.ConnectionString = "Data Source = " + System.Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/" + fileName + ";Version=3;";

            connection2.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection2;
            command.CommandType = CommandType.Text;
            command.CommandText = "select Components.Id, Components.Name, Components.DisplayName, ComponentComponents.IsDefault, ComponentComponents.SelectionGroup, ComponentComponents.Position from ComponentComponents inner join Components on ComponentComponents.ComponentId = Components.Id where ComponentComponents.ParentId = " + parentId.ToString();

            SqliteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int componentId = reader.GetInt32(0);
                //int parentId = reader.GetInt32(1);
                string componentName = reader.GetString(1);
                string componentDisplayName = reader.GetString(2);
                decimal componentCost;
                decimal componentPrice;
                bool isDefault = bool.Parse(reader.GetString(3));
                int group = reader.GetInt32(4);
                int position = reader.GetInt32(5);

                GetItemPrices(1, componentId, out componentCost, out componentPrice);

                component.AddComponent(componentId, parentId, componentName, componentDisplayName, componentCost, componentPrice, 1, isDefault, group, 0, position);
            }

            reader.Close();
            connection2.Close();
        }

        public static Order GetOrder(int id, Boolean deep)
        {
            Order newOrder = new Order(id);
            string name = "";
            int customerType = 1;
            DateTime date = DateTime.Now;
            int orderNumber = 0;

            GetOrderDetails(id, ref name, ref customerType, ref date, ref orderNumber);

            newOrder.Name = name;
            newOrder.CustomerType = customerType;
            newOrder.Date = date;
            newOrder.OrderNumber = orderNumber;

            if (deep)
                GetOrderItems(ref newOrder);

            return newOrder;
        }

        private static void GetOrderDetails(int id, ref string name, ref int customerType, ref DateTime date, ref int orderNumber)
        {
            SqliteConnection connection2 = new SqliteConnection();
            connection2.ConnectionString = "Data Source = " + System.Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/" + fileName + ";Version=3;";

            connection2.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection2;
            command.CommandType = CommandType.Text;
            command.CommandText = "select Name, CustomerType, Date, OrderNumber from Orders where Id = " + id.ToString();

            SqliteDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                name = reader.GetString(0);
                customerType = reader.GetInt32(1);
                date = DateTime.Parse(reader.GetString(2));
                //date = reader.GetDateTime(2);
                orderNumber = reader.GetInt32(3);
            }

            connection2.Close();
        }

        //public static OrderItemModel GetOrderItemModel(int id)
        //{
        //    connection.Open();

        //    SqliteCommand command = new SqliteCommand();
        //    command.Connection = connection;
        //    command.CommandType = CommandType.Text;
        //    command.CommandText = "select OrderId, VariationID, InOutStatus, Discount from OrderItems where Id = " + id.ToString();

        //    SqliteDataReader reader = command.ExecuteReader();
        //    reader.Read();

        //    int orderId = reader.GetInt32(0);
        //    int variationId = reader.GetInt32(1);
        //    int inOutStatus = reader.GetInt32(2);
        //    int discount = reader.GetInt32(3);

        //    Variation variation = GetVariation(variationId);

        //    OrderItem orderItem = new OrderItem(id, orderId, variationId, variation.DisplayName, inOutStatus, discount);

        //    AddOrderItemComponents(ref orderItem);

        //    OrderItemModel newModel = new OrderItemModel(orderItem);

        //    reader.Close();
        //    connection.Close();

        //    return newModel;
        //}

        private static void GetOrderItems(ref Order order)
        {
            SqliteConnection connection2 = new SqliteConnection();
            connection2.ConnectionString = "Data Source = " + System.Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/" + fileName + ";Version=3;";

            connection2.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection2;
            command.CommandType = CommandType.Text;
            command.CommandText = "select Id, VariationId, InOutStatus, Discount, State from OrderItems where OrderId = " + order.Id;

            SqliteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int itemId = reader.GetInt32(0);
                int variationId = reader.GetInt32(1);
                int inOutStatus = reader.GetInt32(2);
                int discount = reader.GetInt32(3);
                State state = (State)reader.GetInt32(4);
                Variation variation = GetVariation(variationId);

                OrderItem orderItem = new OrderItem(itemId, order.Id, variationId, variation.DisplayName, inOutStatus, discount, state);

                GetOrderItemComponents(ref orderItem);

                order.AddOrderItem(orderItem);
            }

            reader.Close();
            connection2.Close();
        }

        private static void GetOrderItemComponents(ref OrderItem item)
        {
            SqliteConnection connection2 = new SqliteConnection();
            connection2.ConnectionString = "Data Source = " + System.Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/" + fileName + ";Version=3;";

            connection2.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection2;
            command.CommandType = CommandType.Text;
            command.CommandText = "select OrderItemComponents.Id, Components.Id, OrderItemComponents.VariationId, Components.Name, Components.DisplayName, OrderItemComponents.Portions, OrderItemComponents.Position from OrderItemComponents inner join Components on Components.Id = OrderItemComponents.ComponentId where OrderItemComponents.OrderItemId = " + item.Id.ToString();

            SqliteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                int componentId = reader.GetInt32(1);
                int parentId = reader.GetInt32(2);
                string name = reader.GetString(3);
                string displayName = reader.GetString(4);
                decimal cost;
                decimal price;
                int portions = reader.GetInt32(5);
                //bool isDefault = reader.GetBoolean(6);
                int position = reader.GetInt32(6);

                GetItemPrices(1, componentId, out cost, out price);

                OrderItemComponent component = new OrderItemComponent(id, parentId, name, displayName, item.Id, componentId, cost, price, portions, false, position);
                item.AddComponent(component);

                GetOrderItemComponentComponents(ref component);
            }

            reader.Close();
            connection2.Close();
        }

        private static void GetOrderItemComponentComponents(ref OrderItemComponent component)
        {
            SqliteConnection connection2 = new SqliteConnection();
            connection2.ConnectionString = "Data Source = " + System.Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/" + fileName + ";Version=3;";

            connection2.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection2;
            command.CommandType = CommandType.Text;
            command.CommandText = "select OrderItemComponentComponents.OrderItemComponentsId, Components.Id, Components.Name, Components.DisplayName, OrderItemComponentComponents.Portions from OrderItemComponentComponents inner join Components on Components.Id = OrderItemComponentComponents.ComponentId where OrderItemComponentComponents.OrderItemComponentsId = " + component.Id.ToString();

            SqliteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                //int id = reader.GetInt32(0);
                int parentId = reader.GetInt32(0);
                int componentId = reader.GetInt32(1);
                string name = reader.GetString(2);
                string displayName = reader.GetString(3);
                decimal cost;
                decimal price;
                int portions = reader.GetInt32(4);

                GetItemPrices(1, componentId, out cost, out price);

                OrderItemComponentComponent newComponent = new OrderItemComponentComponent(parentId, componentId, name, displayName, cost, price, portions);
                component.AddComponent(newComponent);
            }

            reader.Close();
            connection2.Close();
        }

        public static LiveOrders GetLiveOrders()
        {
            LiveOrders orders = new LiveOrders();

            connection.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            //command.CommandText = "select Id, Name from Orders where Live = \"True\"";
            command.CommandText = "select Id from Orders where Live = \"True\"";

            SqliteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Order newOrder = GetOrder(reader.GetInt32(0), false);
                //newOrder.Name = reader.GetString(1);
                orders.AddOrder(newOrder);
            }

            connection.Close();

            return orders;
        }

        //public static int CreateNewOrderItem(int orderId, int variationId, int inOutStatus, int discount, bool log)
        public static int GetNewOrderItemId()
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "select max(Id) from OrderItems";

            SqliteDataReader reader = command.ExecuteReader();

            int newId;

            reader.Read();

            try
            {
                newId = reader.GetInt32(0) + 1;
            }
            catch (InvalidCastException)
            {
                newId = 1;
            }
            connection.Close();

            //OrderItemModel model = new OrderItemModel(newId, orderId, variationId, inOutStatus, discount);
            //AddOrderItem(model, false);

            //if (log)
            //    Log.CreateNewOrderItem(model);

            return newId;
        }

        public static bool SaveOrder(OrderModel model, bool log, bool bluetooth)
        {
            if (bluetooth)
            {
                if (BlueTooth.Test())
                    return true;
            }

            connection.Open();

            OrderModel oldOrder = new OrderModel(model.Id);
            DeleteOrder(oldOrder, false, false);

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "delete from Orders where Id = " + model.Id;
            //command.ExecuteNonQuery();

            command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "delete from OrderItems where OrderId = " + model.Id;
            //command.ExecuteNonQuery();

            command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "insert into Orders (Id, Name, CustomerType, Live, Current, Date, Uploaded, OrderNumber) Values (" + model.Id.ToString() + ", " + "\"" + model.Name + "\", " + model.CustomerType + ", \"True\", \"True\", '" + model.Date.ToShortDateString() + "', \"False\", "+ model.OrderNumber + ")";
            command.ExecuteNonQuery();

            if (log)
                Log.SaveOrder(model);

            if(bluetooth)
                BlueTooth.SaveOrder(model);

            foreach (OrderItemModel itemModel in model.OrderItems)
                AddOrderItem(itemModel, log, bluetooth);

            if(bluetooth)
                BlueTooth.SendRecord("END_OF_ORDER");

            connection.Close();

            return false;
        }

        public static void SetCurrentOrder(int id, bool log)
        {
            ClearCurrentOrder(log);

            connection.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "update Orders set Current = \"True\" where Id = " + id;
            command.ExecuteNonQuery();

            connection.Close();

            if (log)
                Log.SetCurrentOrder(id);
        }

        public static void ClearCurrentOrder(bool log)
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "update Orders set Current = \"False\"";
            command.ExecuteNonQuery();

            connection.Close();

            if (log)
                Log.ClearCurrentOrder();
        }

        public static void PayOrder(OrderModel model, bool log)
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "update Orders set Live = \"False\", Current = \"False\" where Id = " + model.Id.ToString();
            command.ExecuteNonQuery();

            connection.Close();

            if(log)
                Log.PayOrder(model);
        }

        public static int GetFirstLiveOrderModelId()
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "select Id from Orders where Live = \"True\"";

            SqliteDataReader reader = command.ExecuteReader();

            int firstLiveOrderId;

            if (reader.Read())
                firstLiveOrderId = reader.GetInt32(0);
            else
                firstLiveOrderId = 0;

            connection.Close();

            return firstLiveOrderId;
        }

        public static List<PriceValue> GetPrices(int id)
        {
            List<PriceValue> prices = new List<PriceValue>();

            connection.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "select Id, StartDate, EndDate, Cost, Price from Prices where VariationId = " + id;

            SqliteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                string startDate = reader.GetString(1);
                string endDate;
                PriceValue newPrice;

                try
                {
                    endDate = reader.GetString(2);
                    newPrice = new PriceValue(reader.GetInt32(0), id, DateTime.Parse(startDate), DateTime.Parse(endDate), 0, reader.GetDecimal(4));
                }
                catch (InvalidCastException)
                {
                    newPrice = new PriceValue(reader.GetInt32(0), id, DateTime.Parse(startDate), null, 0, reader.GetDecimal(4));
                }

                prices.Add(newPrice);
            }

            connection.Close();

            return prices;
        }

        public static int GetNewVariationId()
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "select max(Id) from Variations";

            SqliteDataReader reader = command.ExecuteReader();

            int newId;
            reader.Read();

            try
            {
                newId = reader.GetInt32(0) + 1;
            }
            catch (InvalidCastException)
            {
                newId = 1;
            }

            connection.Close();

            return newId;
        }

        public static void SaveVariationEditModel(VariationEditModel model)
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "select Id from Variations where Id = " + model.Id;

            SqliteDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                connection.Close();
                UpdateVariation(model);
                return;
            }

            command.CommandText = "insert into Variations (Id, Name, DisplayName) values (" + model.Id + ", \"" + model.Name + "\", \"" + model.DisplayName + "\")";
            command.ExecuteNonQuery();

            connection.Close();

            SavePrices(model);
        }

        private static void UpdateVariation(VariationEditModel model)
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "update Variations set Name = \"" + model.Name + "\", DisplayName = \"" + model.DisplayName + "\" where Id = " + model.Id;
            command.ExecuteNonQuery();

            connection.Close();

            SavePrices(model);
        }

        private static void SavePrices(VariationEditModel model)
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "delete from Prices where VariationId = " + model.Id;
            command.ExecuteNonQuery();

            command.CommandText = "select Max(Id) from Prices";
            SqliteDataReader reader = command.ExecuteReader();

            reader.Read();

            int newId;

            try
            {
                newId = reader.GetInt32(0) + 1;
            }
            catch (InvalidCastException)
            {
                newId = 1;
            }

            reader.Close();

            foreach (PriceModel price in model.Prices)
            {
                if (price.EndDate == null)
                    command.CommandText = "insert into Prices (Id, VariationId, StartDate, Cost, Price) values (" + newId + ", " + model.Id + ", " + price.StartDate.ToString() + ", " + price.Cost + ", " + price.Price + ")";
                else
                    command.CommandText = "insert into Prices (Id, VariationId, StartDate, EndDate, Cost, Price) values (" + newId + ", " + model.Id + ", " + price.StartDate.ToString() + ", " + price.EndDate.ToString() + ", " + price.Cost + ", " + price.Price + ")";

                command.ExecuteNonQuery();
                ++newId;
            }

            connection.Close();
        }

        public static List<DiscountModel> GetDiscountModels()
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "select Id, Description, DiscountType, Discount from Discounts";
            SqliteDataReader reader = command.ExecuteReader();

            List<DiscountModel> models = new List<DiscountModel>();

            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string description = reader.GetString(1);
                int discountType = reader.GetInt32(2);
                decimal discount = reader.GetDecimal(3);

                DiscountModel newModel = new DiscountModel(id, description, discountType, discount);
                models.Add(newModel);
            }

            reader.Close();
            connection.Close();

            return models;
        }

        public static List<CustomerTypeModel> GetCustomerTypeModels()
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "select Id, Description from CustomerTypes";
            SqliteDataReader reader = command.ExecuteReader();

            List<CustomerTypeModel> models = new List<CustomerTypeModel>();

            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string description = reader.GetString(1);

                CustomerTypeModel newModel = new CustomerTypeModel(id, description);
                models.Add(newModel);
            }

            reader.Close();
            connection.Close();

            return models;
        }

        public static string GetCustomerTypeDescription(int id)
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "select Description from CustomerTypes where id = " + id;
            SqliteDataReader reader = command.ExecuteReader();

            reader.Read();

            string description = reader.GetString(0);

            reader.Close();
            connection.Close();

            return description;
        }

        public static DiscountModel GetDiscountModel(int id)
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "select Description, DiscountType, Discount from Discounts where id = " + id;

            SqliteDataReader reader = command.ExecuteReader();
            reader.Read();

            string description = reader.GetString(0);
            int discountType = reader.GetInt32(1);
            decimal discount = reader.GetDecimal(2);
            
            DiscountModel model = new DiscountModel(id, description, discountType, discount);

            reader.Close();
            connection.Close();

            return model;
        }

        public static void AddTransactionFile(string name)
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "insert into Transactions (Filename) values (\"" + name + "\")";
            command.ExecuteNonQuery();

            connection.Close();
        }

        public static bool TransactionFileExists(string name)
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "select Filename from Transactions where Filename = \"" + name + "\"";
            SqliteDataReader reader = command.ExecuteReader();

            if(reader.Read())
            {
                reader.Close();
                connection.Close();

                return true;
            }

            reader.Close();
            connection.Close();

            return false;
        }

        public static void SaveOrderItemState(int id, State state)
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;

            string stateString = ((int)state).ToString();
            command.CommandText = "update OrderItems Set State = " + stateString + " Where Id = " + id.ToString();
            command.ExecuteNonQuery();

            connection.Close();
        }

        public static List<Order> GetUnuploadedOrders(Context context, TextView uploadProgress)
        {
            List<Order> orders = new List<Order>();

            connection.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;

            command.CommandText = "select count(id) from Orders where Uploaded = \"False\"";
            int rowCount = Convert.ToInt32(command.ExecuteScalar());

            if (rowCount == 0)
                return null;

            command.CommandText = "select Id from Orders where Uploaded = \"False\"";
            SqliteDataReader reader = command.ExecuteReader();

            int counter = 0;

            while (reader.Read())
            {
                int id = reader.GetInt32(0);

                Order thisOrder = GetOrder(id, true);

                int percentage = (counter / rowCount) * 100;

                string message = "Preparing Orders for upload: " + percentage + "% done...";

                ((Activity)uploadProgress.Context).RunOnUiThread(() => 
                    ((Activity)uploadProgress.Context).RunOnUiThread(() =>
                { uploadProgress.Text = message; }));

                orders.Add(thisOrder);

                ++counter;
            }

            connection.Close();

            return orders;
        }

        public static void OrderUploaded(int id, bool log)
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "update Orders set Uploaded = \"True\" where Id = " + id.ToString();
            command.ExecuteNonQuery();

            connection.Close();

            //if (log)
            //    Log.OrderUploaded(id);
        }

        //public static void ProcessUpdate(string update)
        //{
        //    if (update.StartsWith("ADD_VARIATION_COMPONENT"))
        //        AddVariationComponent(update);
        //    else if (update.StartsWith("UPDATE_VARIATION_COMPONENT"))
        //        UpdateVariationComponent(update);
        //    else if (update.StartsWith("REMOVE_VARIATION_COMPONENT"))
        //        RemoveVariationComponent(update);
        //}

        public static void AddVariationComponent(int variationId, int componentId, int position)
        {
            int newId = GetNewVariationComponentId();

            connection.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "insert into VariationComponents (Id, VariationId, ComponentId, IsDefault, SelectionGroup, Portions, Points, Position) values (" + newId + "," + variationId.ToString() + ", " + componentId.ToString() + ", \"False\", 0, 0, 0, " + position + ")";
            command.ExecuteNonQuery();

            connection.Close();
        }

        private static int GetNewVariationComponentId()
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "Select Max(Id) from VariationComponents";
            SqliteDataReader reader = command.ExecuteReader();

            reader.Read();

            int newId = 0;
            try
            {
                newId = reader.GetInt32(0) + 1;
            }
            catch
            {
                newId = 1;
            }

            reader.Close();
            connection.Close();

            return newId;
        }

        public static void UpdateVariationComponentDefault(int variationId, int componentId, bool isDefault)
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;

            string newDefault = "";
            if (isDefault)
                newDefault = "True";
            else
                newDefault = "False";

            command.CommandText = "update VariationComponents set IsDefault = \"" + newDefault + "\" where VariationId = " + variationId.ToString() + " and ComponentId = " + componentId.ToString();
            command.ExecuteNonQuery();

            connection.Close();
        }

        public static void UpdateComponentComponentDefault(int parentId, int componentId, bool isDefault)
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "update ComponentComponents set IsDefault = \"" + isDefault.ToString() + "\" where ParentId = " + parentId.ToString() + " and ComponentId = " + componentId.ToString();
            command.ExecuteNonQuery();

            connection.Close();
        }

        public static void RemoveVariationComponent(int variationId, int componentId, int position)
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "delete from VariationComponents where VariationId = " + variationId + " and ComponentId = " + componentId;
            command.ExecuteNonQuery();

            command.CommandText = "select ComponentId, Position from VariationComponents Where VariationId = " + variationId + " and Position > " + position;
            SqliteDataReader reader = command.ExecuteReader();

            List<int> ComponentIds = new List<int>();
            List<int> NewPositions = new List<int>();

            while (reader.Read())
            {
                ComponentIds.Add(reader.GetInt32(0));
                NewPositions.Add(reader.GetInt32(1) - 1);
            }

            reader.Close();
            connection.Close();

            for(int i = 0; i < ComponentIds.Count(); i++)
                UpdateVariationComponentPosition(variationId, ComponentIds[i], NewPositions[i]);
        }

        private static void UpdateVariationComponentPosition(int parentId, int componentId, int position)
        {
            SqliteConnection connection2 = new SqliteConnection();
            connection2.ConnectionString = "Data Source = " + System.Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/" + fileName + ";Version=3;";

            connection2.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection2;
            command.CommandType = CommandType.Text;
            command.CommandText = "update VariationComponents Set Position = " + position + " where VariationId = " + parentId + " and ComponentId = " + componentId;
            command.ExecuteNonQuery();

            connection2.Close();
        }

        public static List<OrderModel> GetOrderQueue()
        {
            List<OrderModel> list = new List<OrderModel>();

            connection.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "select * from QueuedOrders";
            SqliteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                int originalId = reader.GetInt32(1);
                string name = reader.GetString(2);
                int customerType = reader.GetInt32(3);
                DateTime date = DateTime.Parse(reader.GetString(4));
                bool isVoid = bool.Parse(reader.GetString(5));
                int orderNumber = reader.GetInt32(6);

                OrderModel newModel = new OrderModel(originalId, name, customerType, date, orderNumber);
                newModel.Void = isVoid;

                if(!isVoid)
                    GetQueuedOrderItems(id, ref newModel);

                list.Add(newModel);
            }

            reader.Close();
            connection.Close();

            return list;
        }

        private static void GetQueuedOrderItems(int queuedOrderId, ref OrderModel model)
        {
            SqliteConnection connection2 = new SqliteConnection();
            connection2.ConnectionString = "Data Source = " + System.Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/" + fileName + ";Version=3;";

            connection2.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection2;
            command.CommandType = CommandType.Text;
            command.CommandText = "select * from QueuedOrderItems where QueuedOrderId = " + queuedOrderId;

            SqliteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int queuedOrderItemId;
                //int queuedOrderId;
                int originalId;
                int originalOrderId;
                int variationId;

                queuedOrderItemId = reader.GetInt32(0);
                //queuedOrderId = reader.GetInt32(1);
                originalId = reader.GetInt32(2);
                originalOrderId = reader.GetInt32(3);
                variationId = reader.GetInt32(4);

                OrderItemModel itemModel = new OrderItemModel(originalId, originalOrderId, variationId);
                GetQueuedOrderItemComponents(queuedOrderItemId, ref itemModel);

                model.AddOrderItemModel(itemModel);
            }

            connection2.Close();
        }

        private static void GetQueuedOrderItemComponents(int queuedOrderItemId, ref OrderItemModel model)
        {
            SqliteConnection connection2 = new SqliteConnection();
            connection2.ConnectionString = "Data Source = " + System.Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/" + fileName + ";Version=3;";

            connection2.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection2;
            command.CommandType = CommandType.Text;
            command.CommandText = "select * from QueuedOrderItemComponents where QueuedOrderItemId = " + queuedOrderItemId;

            SqliteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int queuedOrderItemComponentId;
                int originalId;
                int originalOrderItemId;
                int originalOrderId;
                int variationId;
                int componentId;
                int portions;
                int position;

                queuedOrderItemComponentId = reader.GetInt32(0);
                originalId = reader.GetInt32(2);
                originalOrderItemId = reader.GetInt32(3);
                originalOrderId = reader.GetInt32(4);
                variationId = reader.GetInt32(5);
                componentId = reader.GetInt32(6);
                portions = reader.GetInt32(7);
                position = reader.GetInt32(8);

                ComponentModel component = GetComponentModel(componentId);
                component.Portions = portions;

                OrderItemComponentModel componentModel = new OrderItemComponentModel(originalId, originalOrderItemId, componentId, component.Name, component.DisplayName, component.Cost, component.Price, portions, position);

                GetQueuedOrderItemComponentComponents(queuedOrderItemComponentId, ref componentModel);

                model.AddComponentModel(componentModel);
            }

            reader.Close();
            connection2.Close();
        }

        private static void GetQueuedOrderItemComponentComponents(int queuedOrderItemComponentId, ref OrderItemComponentModel model)
        {
            SqliteConnection connection2 = new SqliteConnection();
            connection2.ConnectionString = "Data Source = " + System.Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/" + fileName + ";Version=3;";

            connection2.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection2;
            command.CommandType = CommandType.Text;
            command.CommandText = "select * from QueuedOrderItemComponentComponents where QueuedOrderItemComponentsId = " + queuedOrderItemComponentId;

            SqliteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int originalOrderItemComponentsId;
                int componentId;
                int portions;

                originalOrderItemComponentsId = reader.GetInt32(1);
                componentId = reader.GetInt32(2);
                portions = reader.GetInt32(3);

                ComponentModel component = GetComponentModel(componentId);
                component.Portions = portions;

                OrderItemComponentComponentModel componentModel = new OrderItemComponentComponentModel(originalOrderItemComponentsId, componentId, component.Name, component.DisplayName, component.Cost, component.Price, portions, component.IsDefault);
                model.AddComponent(componentModel);
            }

            reader.Close();
            connection2.Close();
        }

        //public static void AddVoidOrderToQueue(int id)
        //{
        //    connection.Open();

        //    SqliteCommand command = new SqliteCommand();
        //    command.Connection = connection;
        //    command.CommandType = CommandType.Text;

        //    int queuedOrderId = GetNewQueuedOrderId();

            //command.CommandText = "insert into QueuedOrders (Id, OriginalId, Void) values (" + queuedOrderId + ", " + id + ", '" +  + "')";
        //    command.ExecuteNonQuery();

        //    connection.Close();
        //}

        public static void AddOrderToQueue(OrderModel model)
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;

            int queuedOrderId = GetNewQueuedOrderId();

            command.CommandText = "insert into QueuedOrders (Id, OriginalId, Name, CustomerType, Date, Void, OrderNumber) values (" + queuedOrderId + ", " + model.Id + ", \"" + model.Name + "\", " + model.CustomerType + ", '" + model.Date.ToShortDateString() + "', '" + model.Void.ToString() + "', " + model.OrderNumber + ")";
            command.ExecuteNonQuery();

            if(model.Void)
            {
                connection.Close();
                return;
            }

            foreach (OrderItemModel itemModel in model.OrderItems)
            {
                int queuedOrderItemId = GetNewQueuedOrderItemId();

                command.CommandText = "insert into QueuedOrderItems (Id, QueuedOrderId, OriginalId, OriginalOrderId, VariationId) values (" + queuedOrderItemId  + ", " + queuedOrderId + ", " + itemModel.Id + ", " + model.Id + ", " + itemModel.VariationId + ")";
                command.ExecuteNonQuery();

                foreach (OrderItemComponentModel componentModel in itemModel.ComponentModels)
                {
                    int queuedOrderItemComponentsId = GetNewQueuedOrderItemComponentId();

                    command.CommandText = "insert into QueuedOrderItemComponents (Id, QueuedOrderItemId, OriginalId, OriginalItemId, OriginalOrderId, VariationId, ComponentId, Portions, Position) values (" + queuedOrderItemComponentsId + ", " + queuedOrderItemId + ", " + componentModel.Id + ", " + itemModel.Id + ", " + model.Id + ", " + itemModel.VariationId + ", " + componentModel.ComponentId + "," + componentModel.Portions +  ", " + componentModel.Position + ")";
                    command.ExecuteNonQuery();

                    foreach(OrderItemComponentComponentModel subComponentModel in componentModel.Components)
                    {
                        //QueuedOrderItemComponentsId integer, OriginalId, OriginalItemComponentsId integer, ComponentId integer, Portions integer)";

                        command.CommandText = "insert into QueuedOrderItemComponentComponents (QueuedOrderItemComponentsId, OriginalItemComponentsId, ComponentId, Portions) values (" + queuedOrderItemComponentsId + ", " + componentModel.Id + ", " + subComponentModel.ComponentId + "," + subComponentModel.Portions + ")";
                        command.ExecuteNonQuery();
                    }
                }
            }

            connection.Close();
        }

        private static int GetNewQueuedOrderId()
        {
            SqliteConnection connection2 = new SqliteConnection();
            connection2.ConnectionString = "Data Source = " + System.Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/" + fileName + ";Version=3;";

            connection2.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection2;
            command.CommandType = CommandType.Text;
            command.CommandText = "select max(Id) from QueuedOrders";

            SqliteDataReader reader = command.ExecuteReader();

            int newId;

            reader.Read();

            try
            {
                newId = reader.GetInt32(0) + 1;
            }
            catch (InvalidCastException)
            {
                newId = 1;
            }

            reader.Close();
            connection2.Close();

            return newId;
        }

        private static int GetNewQueuedOrderItemId()
        {
            SqliteConnection connection2 = new SqliteConnection();
            connection2.ConnectionString = "Data Source = " + System.Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/" + fileName + ";Version=3;";

            connection2.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection2;
            command.CommandType = CommandType.Text;
            command.CommandText = "select max(Id) from QueuedOrderItems";

            SqliteDataReader reader = command.ExecuteReader();

            int newId;

            reader.Read();

            try
            {
                newId = reader.GetInt32(0) + 1;
            }
            catch (InvalidCastException)
            {
                newId = 1;
            }

            reader.Close();
            connection2.Close();

            return newId;
        }

        private static int GetNewQueuedOrderItemComponentId()
        {
            SqliteConnection connection2 = new SqliteConnection();
            connection2.ConnectionString = "Data Source = " + System.Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/" + fileName + ";Version=3;";

            connection2.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection2;
            command.CommandType = CommandType.Text;
            command.CommandText = "select max(Id) from QueuedOrderItemComponents";

            SqliteDataReader reader = command.ExecuteReader();

            int newId;

            reader.Read();

            try
            {
                newId = reader.GetInt32(0) + 1;
            }
            catch (InvalidCastException)
            {
                newId = 1;
            }

            reader.Close();
            connection2.Close();

            return newId;
        }

        public static void ClearOrderQueue()
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "delete from QueuedOrders";
            command.ExecuteNonQuery();

            command.CommandText = "delete from QueuedOrderItems";
            command.ExecuteNonQuery();

            command.CommandText = "delete from QueuedOrderItemComponents";
            command.ExecuteNonQuery();

            command.CommandText = "delete from QueuedOrderItemComponentComponents";
            command.ExecuteNonQuery();

            connection.Close();
        }

        public static int GetNewOrderItemComponentId()
        {
            SqliteConnection connection2 = new SqliteConnection();
            connection2.ConnectionString = "Data Source = " + System.Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/" + fileName + ";Version=3;";

            connection2.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection2;
            command.CommandType = CommandType.Text;
            command.CommandText = "select max(Id) from OrderItemComponents";

            SqliteDataReader reader = command.ExecuteReader();

            int newId;

            reader.Read();

            try
            {
                newId = reader.GetInt32(0) + 1;
            }
            catch (InvalidCastException)
            {
                newId = 1;
            }

            reader.Close();
            connection2.Close();

            return newId;
        }

        public static List<OrderModel> GetTodaysOrders()
        {
            List <OrderModel> todaysOrders = new List<OrderModel>();

            connection.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "select Id from Orders where Date = '" + DateTime.Now.ToShortDateString() + "'";

            SqliteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                OrderModel newOrder = new OrderModel(reader.GetInt32(0));
                todaysOrders.Add(newOrder);
            }

            connection.Close();

            return todaysOrders;
        }

        public static void DeleteVariation(int id)
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "delete from Variations where Id = " + id;

            command.ExecuteNonQuery();

            connection.Close();
        }

        public static void UpdateVariation(int id, int parentId, string name, string displayName, float points, decimal pointPrice)
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "update Variations set ParentId = " + parentId + ", Name = \"" + name + "\", DisplayName = \"" + displayName + "\", Points = " + points + ", PointPrice = " + pointPrice + " where Id = " + id;

            command.ExecuteNonQuery();

            connection.Close();
        }

        public static void AddPrice(int id, int parentType, DateTime date, decimal price)
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "delete from Prices where ParentType = " + parentType + " and ParentId = " + id;

            command.ExecuteNonQuery();

            connection.Close();

            int newId = GetNewPriceId();

            connection.Open();

            command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "insert into Prices (Id, ParentType, ParentId, StartDate, Price) values (" + newId + "," + parentType + "," + id + ",\"" + date.ToShortDateString() + "\", " + price + ")";

            command.ExecuteNonQuery();

            connection.Close();
        }

        private static int GetNewPriceId()
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "select max(id) from Prices";

            SqliteDataReader reader = command.ExecuteReader();

            int newId;

            reader.Read();

            try
            {
                newId = reader.GetInt32(0) + 1;
            }
            catch (InvalidCastException)
            {
                newId = 1;
            }

            reader.Close();
            connection.Close();

            return newId;
        }

        public static void UpdateVariationComponent(int variationId, int componentId, int portions, int group, float points)
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "update VariationComponents set Portions = " + portions + ", SelectionGroup = " + group + ", Points = " + points + " where VariationId = " + variationId + " and ComponentId = " + componentId;

            command.ExecuteNonQuery();

            connection.Close();
        }

        public static void UpdateComponentComponent(int parentId, int itemId, int group)
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "update ComponentComponents set SelectionGroup = " + group + " where ParentId = " + parentId + " and ComponentId = " + itemId;

            command.ExecuteNonQuery();

            connection.Close();
        }

        public static void AddComponentComponent(int parentId, int componentId, int position)
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "insert into ComponentComponents (ParentId, ComponentId, IsDefault, SelectionGroup, Position) values (" + parentId.ToString() + ", " + componentId.ToString() + ", \"False\", 0, " + position + ")";
            command.ExecuteNonQuery();

            connection.Close();
        }

        public static void RemoveComponentComponent(int parentId, int componentId, int position)
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "delete from ComponentComponents Where ParentId = " + parentId + " and ComponentId = " + componentId;
            command.ExecuteNonQuery();

            command.CommandText = "select ComponentId, Position from ComponentComponents Where ParentId = " + parentId + " and Position > " + position;
            SqliteDataReader reader = command.ExecuteReader();

            List<int> ComponentIds = new List<int>();
            List<int> NewPositions = new List<int>();

            while (reader.Read())
            {
                ComponentIds.Add(reader.GetInt32(0));
                NewPositions.Add(reader.GetInt32(1) - 1);
            }

            reader.Close();
            connection.Close();

            for (int i = 0; i < ComponentIds.Count(); i++)
                UpdateComponentComponentPosition(parentId, ComponentIds[i], NewPositions[i]);
        }

        private static void UpdateComponentComponentPosition(int parentId, int componentId, int position)
        {
            SqliteConnection connection2 = new SqliteConnection();
            connection2.ConnectionString = "Data Source = " + System.Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/" + fileName + ";Version=3;";

            connection2.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection2;
            command.CommandType = CommandType.Text;
            command.CommandText = "update ComponentComponents Set Position = " + position + " where ParentId = " + parentId + " and ComponentId = " + componentId;
            command.ExecuteNonQuery();

            connection2.Close();
        }

        public static void AddComponent(int id, string name, string displayName)
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "insert into Components (Id, Name, DisplayName) values (" + id + ", \"" + name + "\", " + "\"" + displayName + "\")";
            command.ExecuteNonQuery();

            connection.Close();
        }

        public static void UpdateComponent(int id, string name, string displayName)
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "update Components set Name = \"" + name + "\", DisplayName = \"" + displayName + "\" where Id = " + id;
            command.ExecuteNonQuery();

            connection.Close();
        }

        public static int GetNewOrderNumber()
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "select Max(OrderNumber) From Orders Where Date = '" + DateTime.Now.ToShortDateString() + "'";

            SqliteDataReader reader = command.ExecuteReader();
            reader.Read();

            int newOrderNumber = 0;

            try
            {
                newOrderNumber = reader.GetInt32(0) + 1;
            }
            catch
            {
                newOrderNumber = 1;
            }

            reader.Close();
            connection.Close();

            return newOrderNumber;
        }

        public static void SaveUpdate(Update update)
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "insert into Updates (Id, UpdateText) Values (" + update.Id + ", \"" + update.UpdateText + "\")";
            command.ExecuteNonQuery();

            connection.Close();
        }

        public static List<Update> GetUpdates()
        {
            List<Update> updates = new List<Update>();

            connection.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "select Id, UpdateText From Updates";

            SqliteDataReader reader = command.ExecuteReader();

            while(reader.Read())
            {
                Update newUpdate = new Update(reader.GetInt32(0), reader.GetString(1));
                updates.Add(newUpdate);
            }

            connection.Close();

            return updates;
        }

        public static void ClearUpdates()
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "Delete From Updates";
            command.ExecuteNonQuery();

            connection.Close();
        }

        public static void AddRemoveVariation(int id, bool remove)
        {
            connection.Open();

            string removed = "False";

            if (remove)
                removed = "True";

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "Update Variations Set Removed = \"" + removed + "\" Where Id = " + id;
            command.ExecuteNonQuery();

            connection.Close();
        }

        public static void SwapVariationPositions(int model1Id, int model1Position, int model2Id, int model2Position)
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;

            command.CommandText = "Update Variations Set Position = " + model2Position + " Where Id = " + model1Id;
            command.ExecuteNonQuery();

            command.CommandText = "Update Variations Set Position = " + model1Position + " Where Id = " + model2Id;
            command.ExecuteNonQuery();

            connection.Close();
        }

        public static void SwapVariationComponentPositions(int variationId, int model1Id, int model1Position, int model2Id, int model2Position)
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;

            command.CommandText = "Update VariationComponents Set Position = " + model2Position + " Where VariationId = " + variationId + " and ComponentId = " + model1Id;
            command.ExecuteNonQuery();

            command.CommandText = "Update VariationComponents Set Position = " + model1Position + " Where VariationId = " + variationId + " and ComponentId = " + model2Id;
            command.ExecuteNonQuery();

            connection.Close();
        }

        public static void SwapComponentComponentPositions(int parentId, int model1Id, int model1Position, int model2Id, int model2Position)
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;

            command.CommandText = "Update ComponentComponents Set Position = " + model2Position + " Where ParentId = " + parentId + " and ComponentId = " + model1Id;
            command.ExecuteNonQuery();

            command.CommandText = "Update ComponentComponents Set Position = " + model1Position + " Where ParentId = " + parentId + " and ComponentId = " + model2Id;
            command.ExecuteNonQuery();

            connection.Close();
        }

        private static List<int> GetUploadedOrderIds()
        {
            List<int> orderIds = new List<int>();

            connection.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;

            command.CommandText = "Select Max(Id) from Orders";
            SqliteDataReader reader = command.ExecuteReader();

            reader.Read();
            int maxId = reader.GetInt32(0);

            reader.Close();

            command.CommandText = "Select Id from Orders Where Uploaded = \"True\" and Id NOT = " + maxId + " order by Id";
            reader = command.ExecuteReader();

            while (reader.Read())
                orderIds.Add(reader.GetInt32(0));

            reader.Close();
            connection.Close();

            return orderIds;
        }

        public static void DeleteOldOrders()
        {
            List<int> orders = GetUploadedOrderIds();

            DateTime compareTime = DateTime.Now - new TimeSpan(7, 0, 0, 0);

            foreach (int id in orders)
            {
                Order thisOrder = GetOrder(id, false);

                if (thisOrder.Date < compareTime)
                    DeleteOrder(new OrderModel(id), false, false);
            }
        }
    }
}
