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

using System.Data;
using System.IO;
using System.Xml;
using Mono.Data.Sqlite;

using LakesideLoungeKitchenAndroid.Application;
using LakesideLoungeKitchenAndroid.Domain;

namespace LakesideLoungeKitchenAndroid.Infrastructure
{
    public class Database
    {
        private static SqliteConnection connection = new SqliteConnection();
        private static string fileName = "LakesideLoungeKitchenDB.db3";

        static Database()
        {
            connection.ConnectionString = "Data Source = " + System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + "/" + fileName + ";Version=3;";
        }

        public static void InitializeDatabase(Context context)
        {
            SqliteConnection.CreateFile(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + "/" + fileName);
            connection.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;

            string query = "create table Orders (Id integer primary key not null, Name text, CustomerType integer, Live boolean, Current boolean, Date datetime, OrderNumber integer)";
            command.CommandText = query;
            command.ExecuteNonQuery();

            query = "create table OrderItems (Id integer primary key not null, OrderId integer, VariationId integer, InOutStatus integer, Discount integer, State integer)";
            command.CommandText = query;
            command.ExecuteNonQuery();

            query = "create table Variations (Id integer primary key not null, ParentId integer not null, Name text not null, DisplayName text)";
            command.CommandText = query;
            command.ExecuteNonQuery();

            //query = "create table VariationComponents (VariationId integer, ComponentId integer, IsDefault boolean)";
            //command.CommandText = query;
            //command.ExecuteNonQuery();

            query = "create table Components (Id integer primary key not null, Name text not null, DisplayName text)";
            command.CommandText = query;
            command.ExecuteNonQuery();

            query = "create table OrderItemComponents (Id integer primary key not null, OrderItemId integer not null, VariationId integer, ComponentId integer not null, Portions integer, Position integer)";
            command.CommandText = query;
            command.ExecuteNonQuery();

            query = "create table OrderItemComponentComponents (OrderItemComponentsId integer not null, ComponentId integer not null, Portions integer)";
            command.CommandText = query;
            command.ExecuteNonQuery();

            //query = "create table Prices (Id integer primary key not null, ParentType integer, ParentId integer not null, StartDate datetime not null, EndDate datetime null, Cost money null, Price money null)";
            //command.CommandText = query;
            //command.ExecuteNonQuery();

            //query = "create table Discounts (Id integer primary key not null, Description text null, Discount integer not null)";
            //command.CommandText = query;
            //command.ExecuteNonQuery();

            //query = "create table CustomerTypes (Id integer primary key not null, Description text null)";
            //command.CommandText = query;
            //command.ExecuteNonQuery();

            //query = "create table Transactions (Filename text)";
            //command.CommandText = query;
            //command.ExecuteNonQuery();

            //query = "create table Ingredients (Id integer, Name text, DisplayName text)";
            //command.CommandText = query;
            //command.ExecuteNonQuery();

            //query = "create table ItemIngredients (ItemId integer, ItemType integer, IngredientId integer, Portions integer)";
            //command.CommandText = query;
            //command.ExecuteNonQuery();

            //query = "create table Costs (IngredientId integer, StartDate datetime, Cost money)";
            //command.CommandText = query;
            //command.ExecuteNonQuery();

            //InsertIngredients(context);
            InsertComponents(context);
            InsertVariations(context);
            //InsertDiscounts();
            //InsertCustomerTypes();

            connection.Close();
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

        private static void InsertVariations(Context context)
        {
            Stream input = context.Assets.Open("Variations.xml");

            XmlDocument doc = new XmlDocument();
            doc.Load(input);
            input.Close();

            InsertVariation(0, doc.FirstChild);
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

            //InsertPrices(id, 1, node.FirstChild.ChildNodes);
        }

        private static void InsertVariation(int parentId, XmlNode node)
        {
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;

            int id = Int32.Parse(node.Attributes.GetNamedItem("Id").Value);

            string name = node.Attributes.GetNamedItem("Name").Value;
            string displayName = node.Attributes.GetNamedItem("DisplayName").Value;

            string query = "insert into Variations (Id, ParentId, Name, DisplayName) values (" + id.ToString() + ", " + parentId.ToString() + ", \"" + name + "\", \"" + displayName + "\");";
            command.CommandText = query;
            command.ExecuteNonQuery();

            if (node.HasChildNodes && node.FirstChild.Name == "Variations")
            {
                foreach (XmlNode childNode in node.FirstChild.ChildNodes)
                    InsertVariation(id, childNode);

                //if (node.ChildNodes.Count == 2)
                //    InsertPrices(id, 0, node.ChildNodes[1].ChildNodes);
            }
            //else if (node.HasChildNodes && node.FirstChild.Name == "Components")
            //{
            //    foreach (XmlNode childNode in node.FirstChild.ChildNodes)
            //        InsertVariationComponent(id, childNode);

            //    if (node.ChildNodes.Count == 2)
            //        InsertPrices(id, 0, node.ChildNodes[1].ChildNodes);
            //}
            //else if (node.HasChildNodes && node.FirstChild.Name == "Prices")
            //{
            //    InsertPrices(id, 0, node.FirstChild.ChildNodes);
            //}
        }

        public static ComponentModel GetComponentModel(int id)
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "select Components.Name, Components.DisplayName from Components where Components.Id = " + id.ToString();

            SqliteDataReader reader = command.ExecuteReader();

            Component component = null;

            if (reader.Read())
            {
                string componentName = reader.GetString(0);
                string componentDisplayName = reader.GetString(1);

                component = new Component(id, componentName, componentDisplayName, 1);
            }
            else
                throw new Exception("Invalid Component Requested");

            ComponentModel newComponentModel = new ComponentModel(component);

            connection.Close();

            return newComponentModel;
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
            connection2.ConnectionString = "Data Source = " + System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + "/" + fileName + ";Version=3;";

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

        public static OrderItem GetOrderItem(int id)
        {
            SqliteConnection connection2 = new SqliteConnection();
            connection2.ConnectionString = "Data Source = " + System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + "/" + fileName + ";Version=3;";

            connection2.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection2;
            command.CommandType = CommandType.Text;
            command.CommandText = "select Id, VariationId, InOutStatus, Discount, State from OrderItems where Id = " + id;

            SqliteDataReader reader = command.ExecuteReader();
            reader.Read();

            int itemId = reader.GetInt32(0);
            int variationId = reader.GetInt32(1);
            int inOutStatus = reader.GetInt32(2);
            int discount = reader.GetInt32(3);
            Variation variation = GetVariation(variationId);
            State state = (State)reader.GetInt32(4);

            reader.Close();

            OrderItem orderItem = new OrderItem(itemId, 0, variationId, variation.DisplayName, inOutStatus, discount, state);

            GetOrderItemComponents(ref orderItem);

            connection2.Close();

            return orderItem;
        }

        private static void GetOrderItems(ref Order order)
        {
            SqliteConnection connection2 = new SqliteConnection();
            connection2.ConnectionString = "Data Source = " + System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + "/" + fileName + ";Version=3;";

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
                Variation variation = GetVariation(variationId);
                State state = (State)reader.GetInt32(4);

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
            connection2.ConnectionString = "Data Source = " + System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + "/" + fileName + ";Version=3;";

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
                int portions = reader.GetInt32(5);
                int position = reader.GetInt32(6);

                OrderItemComponent component = new OrderItemComponent(id, componentId, parentId, name, displayName, portions, position);

                GetOrderItemComponentComponents(ref component);

                item.AddComponent(component);
            }

            reader.Close();
            connection2.Close();
        }

        private static void GetOrderItemComponentComponents(ref OrderItemComponent component)
        {
            SqliteConnection connection2 = new SqliteConnection();
            connection2.ConnectionString = "Data Source = " + System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + "/" + fileName + ";Version=3;";

            connection2.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection2;
            command.CommandType = CommandType.Text;
            command.CommandText = "select OrderItemComponentComponents.OrderItemComponentsId, Components.Id, Components.Name, Components.DisplayName, OrderItemComponentComponents.Portions from OrderItemComponentComponents inner join Components on Components.Id = OrderItemComponentComponents.ComponentId where OrderItemComponentComponents.OrderItemComponentsId = " + component.Id.ToString();

            SqliteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int parentId = reader.GetInt32(0);
                int componentId = reader.GetInt32(1);
                string name = reader.GetString(2);
                string displayName = reader.GetString(3);
                int portions = reader.GetInt32(4);

                OrderItemComponentComponent newComponent = new OrderItemComponentComponent(parentId, componentId, name, displayName, portions);
                component.AddComponent(newComponent);
            }

            reader.Close();
            connection2.Close();
        }

        public static Variation GetVariation(int id)
        {
            SqliteConnection connection2 = new SqliteConnection();
            connection2.ConnectionString = "Data Source = " + System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + "/" + fileName + ";Version=3;";

            connection2.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection2;
            command.CommandType = CommandType.Text;
            command.CommandText = "select Id, ParentId, Name, DisplayName from Variations where id = " + id.ToString();

            SqliteDataReader reader = command.ExecuteReader();

            int parentId = 0;
            string variationName = "";
            string variationDisplayName = "";
            //decimal variationCost;
            //decimal variationPrice;

            if (reader.Read())
            {
                parentId = reader.GetInt32(1);
                variationName = reader.GetString(2);
                variationDisplayName = reader.GetString(3);
                //variationCost = reader.GetDecimal(4);
                //variationPrice = reader.GetDecimal(5);
            }
            else
                throw new Exception("Invalid Variation!");

            reader.Close();

            //GetItemPrices(0, id, out variationCost, out variationPrice);

            connection2.Close();

            Variation newVariation = new Variation(id, parentId, variationName, variationDisplayName);
            //GetSubVariations(ref newVariation);

            //if (newVariation.HasVariations())
            //    return newVariation;
            //else
            //GetComponents(ref newVariation);

            return newVariation;
        }

        public static void SaveOrder(OrderModel model)
        {
            connection.Open();

            OrderModel oldOrder = new OrderModel(model.Id);
            DeleteOrder(oldOrder);

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "delete from Orders where Id = " + model.Id;
            //command.ExecuteNonQuery();

            //List<int> completedOrderItems = GetCompletedOrderItems(model.Id);

            command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "delete from OrderItems where OrderId = " + model.Id;
            //command.ExecuteNonQuery();

            command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "insert into Orders (Id, Name, CustomerType, Live, Current, Date, OrderNumber) Values (" + model.Id.ToString() + ", " + "\"" + model.Name + "\", " + model.CustomerType + ", \"True\", \"True\", '" + model.Date.ToShortDateString() + "', " + model.OrderNumber + ")";
            command.ExecuteNonQuery();

            foreach (OrderItemModel itemModel in model.OrderItems.Values)
                AddOrderItem(itemModel);

            connection.Close();
        }

        private static List<int> GetCompletedOrderItems(int orderId)
        {
            List<int> completedOrders = new List<int>();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "select Id from OrderItems where OrderId = " + orderId + " and State = 2";
            SqliteDataReader reader = command.ExecuteReader();

            while (reader.Read())
                completedOrders.Add(reader.GetInt32(0));

            return completedOrders;
        }

        private static void AddOrderItem(OrderItemModel model)
        {
            SqliteConnection connection2 = new SqliteConnection();
            connection2.ConnectionString = "Data Source = " + System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + "/" + fileName + ";Version=3;";

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

            foreach (OrderItemComponentModel componentModel in model.ComponentModels)
            {
                query = "insert into OrderItemComponents (Id, OrderItemId, VariationId, ComponentId, Portions, Position) values (" + componentModel.Id.ToString() + ", " + model.Id.ToString() + ", " + model.VariationId + "," + componentModel.ComponentId.ToString() + ", " + componentModel.Portions.ToString() + ", " + componentModel.Position.ToString() + ")";
                command.CommandText = query;
                command.ExecuteNonQuery();

                command.CommandText = "delete from OrderItemComponentComponents where OrderItemComponentsId = " + componentModel.Id;
                //command.ExecuteNonQuery();

                foreach (OrderItemComponentComponentModel subComponentModel in componentModel.Components)
                {
                    query = "insert into OrderItemComponentComponents (OrderItemComponentsId, ComponentId, Portions) values (" + componentModel.Id.ToString() + ", " + subComponentModel.ComponentId.ToString() + ", " + subComponentModel.Portions.ToString() + ")";
                    command.CommandText = query;
                    command.ExecuteNonQuery();
                }
            }

            connection2.Close();
        }

        public static List<OrderModel> GetIncompleteOrderModels()
        {
            List<OrderModel> models = new List<OrderModel>();

            connection.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "select Orders.Id From Orders inner join OrderItems on Orders.Id = OrderItems.OrderId Where OrderItems.State <> 2";
            SqliteDataReader reader = command.ExecuteReader();

            while(reader.Read())
            {
                int id = reader.GetInt32(0);

                if(models.Where(m => m.Id == id).Count() == 0)
                    models.Add(new OrderModel(id));
            }

            connection.Close();

            return models;
        }

        private static bool OrderItemContainsComponents(int id)
        {
            SqliteConnection connection2 = new SqliteConnection();
            connection2.ConnectionString = "Data Source = " + System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + "/" + fileName + ";Version=3;";

            connection2.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection2;
            command.CommandType = CommandType.Text;
            command.CommandText = "select Count(id) from OrderItemComponents Where OrderItemId = " + id;
            SqliteDataReader reader = command.ExecuteReader();

            reader.Read();
            int count = reader.GetInt32(0);

            connection2.Close();

            return count > 0;
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

        public static void DeleteOrder(OrderModel model)
        {
            SqliteConnection connection2 = new SqliteConnection();
            connection2.ConnectionString = "Data Source = " + System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + "/" + fileName + ";Version=3;";

            connection2.Open();
            //connection.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection2;

            string query;

            foreach (OrderItemModel item in model.OrderItems.Values)
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
        }

        public static void AddVariation(int id, int parentId, string name, string displayName, decimal price, float points, decimal pointPrice)
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;

            string query = "insert into Variations (Id, ParentId, Name, DisplayName) values (" + id.ToString() + ", " + parentId.ToString() + ", \"" + name + "\", \"" + displayName + "\");";
            command.CommandText = query;
            command.ExecuteNonQuery();

            connection.Close();
        }

        public static void UpdateVariation(int id, int parentId, string name, string displayName, float points, decimal pointPrice)
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "update Variations set ParentId = " + parentId + ", Name = \"" + name + "\", DisplayName = \"" + displayName + "\" where Id = " + id;

            command.ExecuteNonQuery();

            connection.Close();
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
    }
}