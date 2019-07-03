using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Data;
using System.Data.SqlClient;

using LakesideLoungeAdmin.Application;
using LakesideLoungeAdmin.Domain;
using LakesideLoungeAdmin.Helpers;
using LakesideLoungeAdmin.Presentation.EventArgs;

namespace LakesideLoungeAdmin.Infrastructure
{
    public static class Database
    {
        private static SqlConnection connection = new SqlConnection();
        private static string conString = "Data Source=.\\SSMS; Initial Catalog=LakesideLounge; Integrated Security=false; uid=LakesideLounge; password=k^hUe9%2!jeIhj&^";
        //private static string conString = "Data Source=(localdb)\\.\\SharedLounge; Initial Catalog=LakesideLounge; Integrated Security=True";
        //private static string conString = "LakesideLounge";

        public static event EventHandler<ProgressChangedEventArgs> ProgressChanged;

        static Database()
        {
            connection.ConnectionString = conString;
        }

        public static Variation GetVariation(int id, bool deep)
        {
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "dbo.GetVariation";

            SqlParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@Id";
            parameter.Value = id;

            command.Parameters.Add(parameter);

            SqlDataReader reader = command.ExecuteReader();

            int parentId = 0;
            string variationName = "";
            string variationDisplayName = "";
            //decimal variationCost = 0;
            decimal variationPrice = 0;
            float points = 0;
            decimal pointPrice = 0.0M;
            bool removed;
            int position;
            int vatStatus;

            if (reader.Read())
            {
                parentId = reader.GetInt32(0);
                variationName = Helper.StripSpaces(reader.GetString(1));
                variationDisplayName = Helper.StripSpaces(reader.GetString(2));
                points = reader.GetFloat(3);
                pointPrice = reader.GetDecimal(4);
                removed = reader.GetBoolean(5);
                position = reader.GetInt32(6);
                vatStatus = reader.GetInt32(7);
            }
            else
                throw new Exception("Invalid Variation!");

            GetPrice(id, 0, out variationPrice);

            reader.Close();
            connection.Close();

            Variation newVariation = new Variation(id, parentId, variationName, variationDisplayName, variationPrice, points, pointPrice, removed, position, vatStatus);
            newVariation.Ingredients = GetIngredients(id, 0);

            if (!deep)
                return newVariation;

            GetSubVariations(ref newVariation);

            if (newVariation.HasChildren())
                return newVariation;
            else
                GetVariationComponents(ref newVariation);

            return newVariation;
        }

        public static void SaveVariation(int id, int parentId, string name, string displayName, int position, int vatStatus)
        {
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "dbo.InsertVariation";

            SqlParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@id";
            parameter.Value = id;

            SqlParameter parameter2 = command.CreateParameter();
            parameter2.ParameterName = "@parentId";
            parameter2.Value = parentId;

            SqlParameter parameter3 = command.CreateParameter();
            parameter3.ParameterName = "@name";
            parameter3.Value = name;

            SqlParameter parameter4 = command.CreateParameter();
            parameter4.ParameterName = "@displayName";
            parameter4.Value = displayName;

            SqlParameter parameter5 = command.CreateParameter();
            parameter5.ParameterName = "@position";
            parameter5.Value = position;

            SqlParameter parameter6 = command.CreateParameter();
            parameter6.ParameterName = "@vatStatus";
            parameter6.Value = vatStatus;

            command.Parameters.Add(parameter);
            command.Parameters.Add(parameter2);
            command.Parameters.Add(parameter3);
            command.Parameters.Add(parameter4);
            command.Parameters.Add(parameter5);
            command.Parameters.Add(parameter6);

            command.ExecuteNonQuery();

            connection.Close();
        }

        public static void SaveComponent(int id, string name, string displayName)
        {
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "dbo.InsertComponent";

            SqlParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@id";
            parameter.Value = id;

            SqlParameter parameter2 = command.CreateParameter();
            parameter2.ParameterName = "name";
            parameter2.Value = name;

            SqlParameter parameter3 = command.CreateParameter();
            parameter3.ParameterName = "displayName";
            parameter3.Value = displayName;

            command.Parameters.Add(parameter);
            command.Parameters.Add(parameter2);
            command.Parameters.Add(parameter3);

            command.ExecuteNonQuery();

            connection.Close();
        }

        public static int GetNewVariationId()
        {
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "dbo.GetNewVariationId";

            SqlDataReader reader = command.ExecuteReader();
            reader.Read();

            int id = 0;

            try
            {
                id = reader.GetInt32(0) + 1;
            }
            catch
            {
                id = 1;
            }
            
            reader.Close();
            connection.Close();

            return id;
        }

        public static int GetNewComponentId()
        {
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "dbo.GetNewComponentId";

            SqlDataReader reader = command.ExecuteReader();
            reader.Read();

            int id = 0;
            
            try
            {
                id = reader.GetInt32(0) + 1;
            }
            catch
            {
                id = 1;
            }

            reader.Close();
            connection.Close();

            return id;
        }

        private static void GetSubVariations(ref Variation variation)
        {
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "dbo.GetSubVariations";

            SqlParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@parentId";
            parameter.Value = variation.Id;

            command.Parameters.Add(parameter);

            SqlDataReader reader = command.ExecuteReader();

            int count = 0;

            while (reader.Read())
            {
                int variationId = reader.GetInt32(0);
                int parentId = variation.Id;
                string variationName = Helper.StripSpaces(reader.GetString(1));
                string variationDisplayName = Helper.StripSpaces(reader.GetString(2));
                //decimal variationCost = reader.GetDecimal(3);
                decimal variationPrice;
                float points = reader.GetFloat(3);
                decimal pointPrice = reader.GetDecimal(4);
                bool removed = reader.GetBoolean(5);
                int position = reader.GetInt32(6);
                int vatStatus = reader.GetInt32(7);

                GetPrice(variationId, 0, out variationPrice);

                Variation newVariation = variation.AddVariation(variationId, parentId, variationName, variationDisplayName, variationPrice, points, pointPrice, removed, position, vatStatus);
                variation.Children[count].Ingredients = GetIngredients(variationId, 0);

                ++count;
            }

            reader.Close();
            connection.Close();
        }

        public static bool PriceExists(int parentId, int parentType)
        {
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "Select [Price] from [Prices] Where [ParentId] = " + parentId + " and [ParentType] = " + parentType + " and [StartDate] = '" + Helper.GetDate(DateTime.Now) + "';";

            SqlDataReader reader = command.ExecuteReader();

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

        public static void AddPrice(int parentId, int parentType, DateTime startDate, decimal price)
        {
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "insert into [Prices] ([ParentType], [ParentId], [StartDate], [Price]) values (" + parentType + ", " + parentId + ", '" + Helper.GetDate(startDate) + "', " + price.ToString() + ")";
            command.ExecuteNonQuery();

            connection.Close();
        }

        public static void AddCost(int ingredientId, DateTime startDate, decimal cost)
        {
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "insert into [Costs] ([IngredientId], [StartDate], [Cost]) values (" + ingredientId + ", '" + Helper.GetDate(startDate) + "', " + cost.ToString() + ")";
            command.ExecuteNonQuery();

            connection.Close();
        }

        public static void DeletePrice(int parentId, int parentType, DateTime startDate)
        {
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "delete from [Prices] where [ParentType] = " + parentType + " and [ParentId] = " + parentId + " and [StartDate] = '" + Helper.GetDate(startDate) + "'";
            command.ExecuteNonQuery();

            connection.Close();
        }

        public static void DeleteCost(int ingredientId, DateTime startDate)
        {
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "delete from [Costs] where [IngredientId] = " + ingredientId + " and [StartDate] = '" + Helper.GetDate(startDate) + "'";
            command.ExecuteNonQuery();

            connection.Close();
        }

        private static void GetPrice(int parentId, int parentType, out decimal price)
        {
            SqlConnection connection2 = new SqlConnection();
            connection2.ConnectionString = conString;

            connection2.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection2;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "dbo.GetPrice";

            SqlParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@parentId";
            parameter.Value = parentId;
            command.Parameters.Add(parameter);

            SqlParameter parameter2 = command.CreateParameter();
            parameter2.ParameterName = "@parentType";
            parameter2.Value = parentType;
            command.Parameters.Add(parameter2);

            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
                price = reader.GetDecimal(0);
            else
                price = 0;

            connection2.Close();
        }

        private static void GetVariationComponents(ref Variation variation)
        {
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "dbo.GetVariationComponents";

            SqlParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@parentId";
            parameter.Value = variation.Id;

            command.Parameters.Add(parameter);

            SqlDataReader reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string name = Helper.StripSpaces(reader.GetString(1));
                string displayName = Helper.StripSpaces(reader.GetString(2));
                decimal price;
                bool isDefault = reader.GetBoolean(3);
                //--------------------------------------------------
                int portions = reader.GetInt32(4);
                int group = reader.GetInt32(5);
                float points = reader.GetFloat(6);
                int position = reader.GetInt32(7);

                GetPrice(id, 1, out price);
                
                variation.AddComponent(id, name, displayName, price, isDefault, points, position, portions, group);
            }

            reader.Close();
            connection.Close();
        }

        public static List<ItemModelBase> GetAllComponents(int parentId = 0, int filterId = 0)
        {
            List<ItemModelBase> components = new List<ItemModelBase>();

            SqlConnection connection2 = new SqlConnection();
            connection2.ConnectionString = conString;

            connection2.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection2;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "dbo.GetAllComponents";

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int id = reader.GetInt32(0);

                ComponentModel newComponent = new ComponentModel(parentId, GetComponent(id));

                if (filterId == 0)
                    components.Add(newComponent);
                else if (filterId != 0 && !newComponent.HasChildren && filterId != id)
                    components.Add(newComponent);
            }

            connection2.Close();

            return components;
        }

        public static Component GetSubComponent(int parentId, int componentId)
        {
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "dbo.GetSubComponent";

            SqlParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@parentId";
            parameter.Value = parentId;
            command.Parameters.Add(parameter);

            SqlParameter parameter2 = command.CreateParameter();
            parameter2.ParameterName = "@componentId";
            parameter2.Value = componentId;
            command.Parameters.Add(parameter2);

            SqlDataReader reader = command.ExecuteReader();
            reader.Read();

            int group = reader.GetInt32(0);
            int position = reader.GetInt32(1);

            reader.Close();
            connection.Close();

            Component component = new Component(0, "", "", 0, false, 0, position, 0, group, false);
            return component;
        }

        public static Component GetComponent(int id)
        {
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "dbo.GetComponent";

            SqlParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@id";
            parameter.Value = id;

            command.Parameters.Add(parameter);

            SqlDataReader reader = command.ExecuteReader();

            reader.Read();

            string name = Helper.StripSpaces(reader.GetString(0));
            string displayName = Helper.StripSpaces(reader.GetString(1));
            decimal price;
            bool removed = reader.GetBoolean(2);

            GetPrice(id, 1, out price);

            reader.Close();
            connection.Close();

            Component newComponent = new Component(id, name, displayName, price, false, 0, 0, 0, 0, removed);

            GetComponentComponents(ref newComponent);

            return newComponent;
        }

        private static void GetComponentComponents(ref Component component)
        {
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "dbo.GetComponentComponents";

            SqlParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@parentId";
            parameter.Value = component.Id;

            command.Parameters.Add(parameter);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int componentId = reader.GetInt32(0);
                string name = Helper.StripSpaces(reader.GetString(1));
                string displayName = Helper.StripSpaces(reader.GetString(2));
                decimal price;
                bool isDefault = reader.GetBoolean(3);
                int position = reader.GetInt32(4);

                GetPrice(componentId, 1, out price);

                component.AddComponent(new Component(componentId, name, displayName, price, isDefault, 0, position));
            }

            connection.Close();
        }

        public static List<Ingredient> GetIngredients()
        {
            List<Ingredient> ingredients = new List<Ingredient>();

            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "dbo.GetIngredients";

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string name = Helper.StripSpaces(reader.GetString(1));
                string displayName = Helper.StripSpaces(reader.GetString(2));
                int portionSize = reader.GetInt32(3);
                int portionType = reader.GetInt32(4);
                //decimal cost = GetIngredientCost(id);

                ingredients.Add(new Ingredient(id, name, displayName, portionSize, portionType, 1));
            }

            reader.Close();
            connection.Close();

            return ingredients;
        }

        public static Ingredient GetIngredient(int id)
        {
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "dbo.GetIngredient";

            SqlParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@id";
            parameter.Value = id;

            command.Parameters.Add(parameter);

            SqlDataReader reader = command.ExecuteReader();
            reader.Read();

            string name = Helper.StripSpaces(reader.GetString(0));
            string displayName = Helper.StripSpaces(reader.GetString(1));
            //decimal cost = GetIngredientCost(id);
            int portionSize = reader.GetInt32(2);
            int portionType = reader.GetInt32(3);
            int portions = 0;

            Ingredient ingredient = new Ingredient(id, name, displayName, portionSize, portionType, portions);

            reader.Close();
            connection.Close();

            return ingredient;
        }

        //private static decimal GetIngredientCost(int ingredientId)
        //{
        //    SqlConnection connection2 = new SqlConnection();
        //    connection2.ConnectionString = conString;

        //    connection2.Open();

        //    SqlCommand command = new SqlCommand();
        //    command.Connection = connection2;
        //    command.CommandType = CommandType.StoredProcedure;
        //    command.CommandText = "dbo.GetCost";

        //    SqlParameter parameter = command.CreateParameter();
        //    parameter.ParameterName = "@ingredientId";
        //    parameter.Value = ingredientId;

        //    command.Parameters.Add(parameter);

        //    SqlDataReader reader = command.ExecuteReader();
        //    reader.Read();

        //    decimal cost = reader.GetDecimal(0);

        //    reader.Close();
        //    connection2.Close();

        //    return cost;
        //}

        public static List<Ingredient> GetIngredients(int itemId, int itemType)
        {
            List<Ingredient> ingredients = new List<Ingredient>();

            SqlConnection connection2 = new SqlConnection();
            connection2.ConnectionString = conString;

            connection2.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection2;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "dbo.GetItemIngredients";

            SqlParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@itemId";
            parameter.Value = itemId;

            command.Parameters.Add(parameter);

            SqlParameter parameter2 = command.CreateParameter();
            parameter2.ParameterName = "@itemType";
            parameter2.Value = itemType;

            command.Parameters.Add(parameter2);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                int portions = reader.GetInt32(1);
                ingredients.Add(new Ingredient(id, null, null, 0, 0, portions));
            }

            reader.Close();
            connection2.Close();

            return ingredients;
        }

        public static void InsertItemIngredients(int itemId, int itemType, IngredientsModel model)
        {
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;

            foreach (IngredientModel ingModel in model.Children)
            {
                string query = "insert into [dbo].[ItemIngredients] ([ItemId], [ItemType], [IngredientId], [Portions]) values (" + itemId.ToString() + ", " + itemType.ToString() + ", " + ingModel.Id.ToString() + ", " + ingModel.Portions.ToString() + ");";
                command.CommandText = query;
                command.ExecuteNonQuery();
            }

            connection.Close();
        }

        public static void DeleteItemIngredients(int itemId, int itemType)
        {
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "dbo.DeleteItemIngredients";

            SqlParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@itemId";
            parameter.Value = itemId;
            command.Parameters.Add(parameter);

            SqlParameter parameter2 = command.CreateParameter();
            parameter2.ParameterName = "@itemType";
            parameter2.Value = itemType;
            command.Parameters.Add(parameter2);

            command.ExecuteNonQuery();

            connection.Close();
        }

        public static void UpdateVariation(int id, int parentId, string name, string displayName, float points, decimal pointPrice, int vatStatus)
        {
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "dbo.UpdateVariation";

            SqlParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@itemId";
            parameter.Value = id;
            command.Parameters.Add(parameter);

            SqlParameter parameter2 = command.CreateParameter();
            parameter2.ParameterName = "@name";
            parameter2.Value = name;
            command.Parameters.Add(parameter2);

            SqlParameter parameter3 = command.CreateParameter();
            parameter3.ParameterName = "@displayName";
            parameter3.Value = displayName;
            command.Parameters.Add(parameter3);

            SqlParameter parameter4 = command.CreateParameter();
            parameter4.ParameterName = "@points";
            parameter4.Value = points;
            command.Parameters.Add(parameter4);

            SqlParameter parameter5 = command.CreateParameter();
            parameter5.ParameterName = "@pointPrice";
            parameter5.Value = pointPrice;
            command.Parameters.Add(parameter5);

            SqlParameter parameter6 = command.CreateParameter();
            parameter6.ParameterName = "@vatStatus";
            parameter6.Value = vatStatus;
            command.Parameters.Add(parameter6);

            command.ExecuteNonQuery();

            connection.Close();

            SaveUpdate("UPDATE_VARIATION," + id + "," + parentId + "," + name + "," + displayName + "," + points + "," + pointPrice);
        }

        public static void UpdateComponent(int id, string name, string displayName)
        {
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "dbo.UpdateComponent";

            SqlParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@itemId";
            parameter.Value = id;
            command.Parameters.Add(parameter);

            SqlParameter parameter2 = command.CreateParameter();
            parameter2.ParameterName = "@name";
            parameter2.Value = name;
            command.Parameters.Add(parameter2);

            SqlParameter parameter3 = command.CreateParameter();
            parameter3.ParameterName = "@displayName";
            parameter3.Value = displayName;
            command.Parameters.Add(parameter3);

            command.ExecuteNonQuery();

            connection.Close();

            SaveUpdate("UPDATE_COMPONENT," + id + "," + name + "," + displayName);
        }

        public static void UpdateIngredient(int id, string name, string displayName, int portionSize, int portionType)
        {
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "dbo.UpdateIngredient";

            SqlParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@itemId";
            parameter.Value = id;
            command.Parameters.Add(parameter);

            SqlParameter parameter2 = command.CreateParameter();
            parameter2.ParameterName = "@name";
            parameter2.Value = name;
            command.Parameters.Add(parameter2);

            SqlParameter parameter3 = command.CreateParameter();
            parameter3.ParameterName = "@displayName";
            parameter3.Value = displayName;
            command.Parameters.Add(parameter3);

            SqlParameter parameter4 = command.CreateParameter();
            parameter4.ParameterName = "@portionSize";
            parameter4.Value = portionSize;
            command.Parameters.Add(parameter4);

            SqlParameter parameter5 = command.CreateParameter();
            parameter5.ParameterName = "@portionType";
            parameter5.Value = portionType;
            command.Parameters.Add(parameter5);

            command.ExecuteNonQuery();

            connection.Close();
        }

        public static bool SaveOrder(Order order)
        {
            DeleteOrder(order);

            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "delete from Orders where Id = " + order.Id;
            //command.ExecuteNonQuery();

            command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "delete from OrderItems where OrderId = " + order.Id;
            //command.ExecuteNonQuery();

            command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "insert into [dbo].[Orders] ([Id], [Name], [CustomerType], [Date]) Values (" + order.Id.ToString() + ", '" + order.Name + "', " + order.CustomerType + ", '" + order.Date.ToLongDateString() + "')";
            command.ExecuteNonQuery();

            foreach (OrderItem item in order.OrderItems)
                AddOrderItem(item);

            connection.Close();

            return false;
        }

        private static void AddOrderItem(OrderItem item)
        {
            SqlConnection connection2 = new SqlConnection();
            connection2.ConnectionString = conString;

            connection2.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection2;
            command.CommandType = CommandType.Text;
            command.CommandText = "delete from OrderItemComponents where OrderItemId = " + item.Id;
            //command.ExecuteNonQuery();

            string query = "insert into OrderItems (Id, OrderId, VariationId, InOutStatus, Discount) values (" + item.Id.ToString() + ", " + item.OrderId.ToString() + ", " + item.VariationId.ToString() + ", " + item.InOutStatus + ", " + item.DiscountId.ToString() + ")";
            command.CommandText = query;
            command.ExecuteNonQuery();

            foreach (OrderItemComponent component in item.Components)
            {
                query = "insert into OrderItemComponents (OrderItemId, VariationId, ComponentId, Portions) values (" + item.Id.ToString() + ", " + item.VariationId + "," + component.Id.ToString() + ", " + component.Portions.ToString() + ")";
                command.CommandText = query;
                command.ExecuteNonQuery();
            }

            connection2.Close();
        }

        public static void SetComponentSelected(string parentType, int parentId, int componentId, int position)
        {
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;

            if(parentType == "Variation")
                command.CommandText = "insert into [" + parentType + "Components] ([VariationId], [ComponentId], [Default], [Portions], [Group], [Points], [Position]) values (" + parentId.ToString() + ", " + componentId.ToString() + ", 0, 1 ,0 , 0, " + position + ")";
            else
                command.CommandText = "insert into [" + parentType + "Components] ([ParentId], [ComponentId], [Default], [Group], [Position]) values (" + parentId.ToString() + ", " + componentId.ToString() + ", 0, 0, " + position + ")";

            command.ExecuteNonQuery();

            connection.Close();

            string line = "ADD_" + parentType.ToUpper() + "_COMPONENT," + parentId.ToString() + "," + componentId + "," + position;
            SaveUpdate(line);
        }

        public static void SetComponentDeselected(string parentType, int parentId, int componentId, int position)
        {
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;

            if(parentType == "Variation")
                command.CommandText = "delete from [" + parentType + "Components] where VariationId = " + parentId.ToString() + " and ComponentId = " + componentId.ToString();
            else
                command.CommandText = "delete from [" + parentType + "Components] where ParentId = " + parentId.ToString() + " and ComponentId = " + componentId.ToString();

            command.ExecuteNonQuery();

            if(parentType == "Variation")
                command.CommandText = "select [ComponentId], [Position] from [" + parentType + "Components] where VariationId = " + parentId + " and [Position] > " + position;
            else
                command.CommandText = "select [ComponentId], [Position] from [" + parentType + "Components] where ParentId = " + parentId + " and [Position] > " + position;

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
                UpdateComponent(parentType, parentId, reader.GetInt32(0), reader.GetInt32(1) - 1);

            reader.Close();
            connection.Close();

            string line = "REMOVE_" + parentType.ToUpper() + "_COMPONENT," + parentId.ToString() + "," + componentId + "," + position;
            SaveUpdate(line);
        }

        private static void UpdateComponent(string parentType, int parentId, int componentId, int position)
        {
            SqlConnection connection2 = new SqlConnection();
            connection2.ConnectionString = conString;

            connection2.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection2;
            command.CommandType = CommandType.Text;

            if (parentType == "Variation")
                command.CommandText = "update [VariationComponents] set [Position] = " + position + " where VariationId = " + parentId + " and ComponentId = " + componentId;
            else
                command.CommandText = "update [ComponentComponents] set [Position] = " + position + " where ParentId = " + parentId + " and ComponentId = " + componentId;

            command.ExecuteNonQuery();

            connection2.Close();
        }

        public static void SetComponentDefault(string parentType, int parentId, int componentId)
        {
            UpdateComponentDefaultStatus(parentType, parentId, componentId, 1);
        }

        public static void SetComponentUnDefault(string parentType, int parentId, int componentId)
        {
            UpdateComponentDefaultStatus(parentType, parentId, componentId, 0);
        }

        private static void UpdateComponentDefaultStatus(string parentType, int parentId, int componentId, int isDefault)
        {
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;

            if(parentType == "Variation")
                command.CommandText = "update [" + parentType + "Components] set [Default] = " + isDefault.ToString() + " where VariationId = " + parentId.ToString() + " and ComponentId = " + componentId.ToString();
            else
                command.CommandText = "update [" + parentType + "Components] set [Default] = " + isDefault.ToString() + " where ParentId = " + parentId.ToString() + " and ComponentId = " + componentId.ToString();

            command.ExecuteNonQuery();

            connection.Close();

            string line;
            string newDefault = "";

            if (isDefault == 1)
                newDefault = "True";
            else
                newDefault = "False";

            if(parentType == "Variation")
                line = "UPDATE_VARIATION_COMPONENT_DEFAULT," + parentId.ToString() + "," + componentId + "," + newDefault;
            else
                line = "UPDATE_COMPONENT_COMPONENT_DEFAULT," + parentId.ToString() + "," + componentId + "," + newDefault;

            SaveUpdate(line);
        }

        public static void AddUploadedTransactionFile(string name)
        {
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "insert into [dbo].[Transactions] ([Filename]) values ('" + name + "')";
            command.ExecuteNonQuery();

            connection.Close();
        }

        public static bool TransactionFileUploaded(string name)
        {
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "select [Filename] from [dbo].[Transactions] where [Filename] = '" + name + "'";
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                reader.Close();
                connection.Close();

                return true;
            }

            reader.Close();
            connection.Close();

            return false;
        }

        public static void AddStockItem(int ingredientId, int items, int portionsPerItem, decimal costPerItem)
        {
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "InsertStockItem";

            SqlParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@ingredientId";
            parameter.Value = ingredientId;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@items";
            parameter.Value = items;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@portionsPerItem";
            parameter.Value = portionsPerItem;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@costPerItem";
            parameter.Value = costPerItem;
            command.Parameters.Add(parameter);

            command.ExecuteNonQuery();

            connection.Close();
        }

        public static List<StockItemModel> GetCurrentStock(int ingredientId)
        {
            List<StockItemModel> stockItems = new List<StockItemModel>();

            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "GetCurrentStock";

            SqlParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@id";
            parameter.Value = ingredientId;

            command.Parameters.Add(parameter);

            SqlDataReader reader = command.ExecuteReader();

            while(reader.Read())
            {
                StockItemModel newModel = new StockItemModel(ingredientId, reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetDecimal(4));
                stockItems.Add(newModel);
            }

            reader.Close();
            connection.Close();

            return stockItems;
        }

        public static void SaveUpdate(string updateText)
        {
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "InsertUpdate";

            SqlParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@updateText";
            parameter.Value = updateText;
            command.Parameters.Add(parameter);

            command.ExecuteNonQuery();

            connection.Close();
        }

        public static void UpdateVariationComponent(int variationId, int componentId, int portions, int group, float points)
        {
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "UpdateVariationComponent";

            SqlParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@variationId";
            parameter.Value = variationId;
            command.Parameters.Add(parameter);

            SqlParameter parameter2 = command.CreateParameter();
            parameter2.ParameterName = "@componentId";
            parameter2.Value = componentId;
            command.Parameters.Add(parameter2);

            SqlParameter parameter3 = command.CreateParameter();
            parameter3.ParameterName = "@portions";
            parameter3.Value = portions;
            command.Parameters.Add(parameter3);

            SqlParameter parameter4 = command.CreateParameter();
            parameter4.ParameterName = "@group";
            parameter4.Value = group;
            command.Parameters.Add(parameter4);

            SqlParameter parameter5 = command.CreateParameter();
            parameter5.ParameterName = "@points";
            parameter5.Value = points;
            command.Parameters.Add(parameter5);

            command.ExecuteNonQuery();

            connection.Close();
        }

        public static void UpdateComponentComponent(int parentId, int componentId, int group)
        {
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "UpdateComponentComponent";

            SqlParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@parentId";
            parameter.Value = parentId;
            command.Parameters.Add(parameter);

            SqlParameter parameter2 = command.CreateParameter();
            parameter2.ParameterName = "@componentId";
            parameter2.Value = componentId;
            command.Parameters.Add(parameter2);

            SqlParameter parameter3 = command.CreateParameter();
            parameter3.ParameterName = "@group";
            parameter3.Value = group;
            command.Parameters.Add(parameter3);

            command.ExecuteNonQuery();

            connection.Close();
        }

        public static void AddRemoveVariation(int id, bool remove)
        {
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "AddRemoveVariation";

            SqlParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@id";
            parameter.Value = id;
            command.Parameters.Add(parameter);

            SqlParameter parameter2 = command.CreateParameter();
            parameter2.ParameterName = "@remove";
            parameter2.Value = remove;
            command.Parameters.Add(parameter2);

            command.ExecuteNonQuery();

            connection.Close();
        }

        public static void AddRemoveComponent(int id, bool remove)
        {
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "AddRemoveComponent";

            SqlParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@id";
            parameter.Value = id;
            command.Parameters.Add(parameter);

            SqlParameter parameter2 = command.CreateParameter();
            parameter2.ParameterName = "@remove";
            parameter2.Value = remove;
            command.Parameters.Add(parameter2);

            command.ExecuteNonQuery();

            connection.Close();
        }

        public static void DeleteVariation(VariationModel model)
        {
            SqlConnection connection2 = new SqlConnection();
            connection2.ConnectionString = conString;

            connection2.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection2;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "DeleteVariation";

            SqlParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@id";
            parameter.Value = model.Id;
            command.Parameters.Add(parameter);

            command.ExecuteNonQuery();

            if (model.HasChildren && model.Children[0] is VariationModel)
                foreach (VariationModel variationModel in model.Children)
                    DeleteVariation(variationModel);
            else if (model.HasChildren)
                DeleteVariationComponents(model);

            connection2.Close();
        }

        private static void DeleteVariationComponents(VariationModel model)
        {
            SqlConnection connection2 = new SqlConnection();
            connection2.ConnectionString = conString;

            connection2.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection2;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "DeleteVariationComponents";

            SqlParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@variationId";
            parameter.Value = model.Id;
            command.Parameters.Add(parameter);

            command.ExecuteNonQuery();

            connection2.Close();
        }

        public static void BackupDatabase(string fileName)
        {
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "Backup database LakesideLounge to disk='" + fileName + "'";

            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            connection.Close();
        }

        public static bool RestoreDatabase(string fileName)
        {
            SqlConnection.ClearAllPools();

            SqlConnection connection2 = new SqlConnection();
            connection2.ConnectionString = "Data Source=.\\SSMS; Integrated Security=false; uid=LakesideLounge; password=k^hUe9%2!jeIhj&^";

            connection2.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection2;
            command.CommandType = CommandType.Text;

            command.CommandText = "Drop database LakesideLounge";

            try
            {
                command.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                return false;
            }

            command.CommandText = "Restore database LakesideLounge from disk = '" + fileName + "'";

            try
            {
                command.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            connection2.Close();

            return true;
        }

        public static int GetNewIngredientId()
        {
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "GetMaxIngredientId";

            SqlDataReader reader = command.ExecuteReader();
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

        public static void AddIngredient(int id)
        {
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "AddIngredient";

            SqlParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@id";
            parameter.Value = id;
            command.Parameters.Add(parameter);

            command.ExecuteNonQuery();

            connection.Close();
        }

        public static void SwapComponents(string parentType, int parentId, ComponentModel movingItem, ComponentModel otherItem)
        {
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;

            if (parentType == "Variation")
            {
                command.CommandText = "update [VariationComponents] Set Position = " + otherItem.Position + " Where VariationId = " + parentId + " and ComponentId = " + movingItem.Id;
                command.ExecuteNonQuery();

                command.CommandText = "update [VariationComponents] Set Position = " + movingItem.Position + " Where VariationId = " + parentId + " and ComponentId = " + otherItem.Id;
                command.ExecuteNonQuery();
            }
            else
            {
                command.CommandText = "update [ComponentComponents] Set Position = " + otherItem.Position + " Where ParentId = " + parentId + " and ComponentId = " + movingItem.Id;
                command.ExecuteNonQuery();

                command.CommandText = "update [ComponentComponents] Set Position = " + movingItem.Position + " Where ParentId = " + parentId + " and ComponentId = " + otherItem.Id;
                command.ExecuteNonQuery();
            }

            connection.Close();
        }

        public static void SwapVariationPositions(VariationModel model1, VariationModel model2)
        {
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;

            command.CommandText = "update [Variations] Set [Position] = " + model2.Position + " where Id = " + model1.Id;
            command.ExecuteNonQuery();

            command.CommandText = "update [Variations] Set [Position] = " + model1.Position + " where Id = " + model2.Id;
            command.ExecuteNonQuery();

            connection.Close();
        }

        private static void DeleteOrder(Order order)
        {
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "delete from Orders where Id = " + order.Id;
            command.ExecuteNonQuery();

            command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "delete from OrderItems where OrderId = " + order.Id;
            command.ExecuteNonQuery();

            //foreach(OrderItemCom)

            connection.Close();
        }

        public static int ProcessOrders()
        {
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "select Count(Id) from Orders where Processed = 0";

            SqlDataReader reader = command.ExecuteReader();
            reader.Read();

            int count = reader.GetInt32(0);

            command.CommandText = "select Id, CustomerType, Date from Orders where Processed = 0";

            reader.Close();
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                int processCount = 1;

                int id = reader.GetInt32(0);
                int customerType = reader.GetInt32(1);
                DateTime date = reader.GetDateTime(2);

                ProcessOrder(id, customerType, date);
                ProgressChanged(null, new ProgressChangedEventArgs(processCount));

                ++processCount;
            }

            reader.Close();
            connection.Close();

            return count;
        }

        private static void ProcessOrder(int id, int customerType, DateTime date)
        {
            Order thisOrder = GetOrder(id, customerType, date);

            List<Ingredient> ingredientsUsed = thisOrder.IngredientsUsed();

            foreach (Ingredient ingredient in ingredientsUsed)
                DeductIngredient(ingredient);
        }

        private static void DeductIngredient(Ingredient ingredient)
        {
            List<StockItemModel> stockItems = GetCurrentStock(ingredient.Id);

            int portionsLeftToRemove = ingredient.Portions;

            foreach (StockItemModel model in stockItems)
            {
                if (portionsLeftToRemove == 0)
                    break;

                if(portionsLeftToRemove >= model.CurrentPortions)
                {
                    while (portionsLeftToRemove > 0)
                    {
                        if (model.CurrentItems == 0)
                        {
                            portionsLeftToRemove = portionsLeftToRemove - model.CurrentPortions;
                            model.CurrentPortions = 0;

                            //Delete Model
                            break;
                        }
                        else
                        {
                            while (portionsLeftToRemove > model.PortionsPerItem && model.CurrentItems > 0)
                            {
                                portionsLeftToRemove -= model.PortionsPerItem;
                                --model.CurrentItems;

                                //Update Model
                            }

                            if (model.CurrentItems == 0)
                            {
                                if (portionsLeftToRemove > model.CurrentPortions)
                                {
                                    portionsLeftToRemove -= model.CurrentPortions;
                                    model.CurrentPortions = 0;

                                    //Delete Model
                                }
                                else
                                {
                                    portionsLeftToRemove = 0;
                                    model.CurrentPortions -= portionsLeftToRemove;

                                    //Update Model
                                }

                                break;
                            }
                            else if (portionsLeftToRemove == model.CurrentPortions)
                            {
                                model.CurrentPortions = 0;
                                portionsLeftToRemove = 0;

                                //Update Model
                                break;
                            }
                            else if (portionsLeftToRemove <= model.PortionsPerItem)
                            {
                                model.CurrentPortions = (model.PortionsPerItem - portionsLeftToRemove) + model.CurrentPortions;
                                portionsLeftToRemove = 0;
                                --model.CurrentItems;

                                //Update model
                                break;
                            }
                        }
                    }
                }
                else
                {
                    model.CurrentPortions = model.CurrentPortions - portionsLeftToRemove;

                    //Update Model
                    portionsLeftToRemove = 0;
                    break;
                }
            }
        }

        private static Order GetOrder(int id, int customerType, DateTime date)
        {
            Order newOrder = new Order(id, customerType, date);
            GetOrderItems(ref newOrder);

            return newOrder;
        }

        private static void GetOrderItems(ref Order order)
        {
            SqlConnection connection2 = new SqlConnection();
            connection2.ConnectionString = conString;

            connection2.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection2;
            command.CommandType = CommandType.Text;
            command.CommandText = "select Id, VariationId, InOutStatus, Discount from OrdersItems where OrderId = " + order.Id;

            SqlDataReader reader = command.ExecuteReader();

            while(reader.Read())
            {
                int id = reader.GetInt32(0);
                int variationId = reader.GetInt32(1);
                int inOutStatus = reader.GetInt32(2);
                int discount = reader.GetInt32(3);

                OrderItem newItem = new OrderItem(id, order.Id, variationId, inOutStatus, discount);
                order.AddOrderItem(newItem);

                AddOrderItemComponents(ref newItem);
            }

            reader.Close();
            connection2.Close();
        }

        private static void AddOrderItemComponents(ref OrderItem item)
        {
            SqlConnection connection2 = new SqlConnection();
            connection2.ConnectionString = conString;

            connection2.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection2;
            command.CommandType = CommandType.Text;
            command.CommandText = "select Id, VariationId, ComponentId, Portions from OrdersItemComponents where OrderItemId = " + item.Id;

            SqlDataReader reader = command.ExecuteReader();

            while(reader.Read())
            {
                int id = reader.GetInt32(0);
                int variationId = reader.GetInt32(1);
                int componentId = reader.GetInt32(2);
                int portions = reader.GetInt32(3);

                OrderItemComponent component = new OrderItemComponent(id, item.Id, variationId, componentId, portions);
                item.AddComponent(component);

                AddOrderItemComponentComponents(ref component);
            }

            reader.Close();
            connection2.Close();
        }

        private static void AddOrderItemComponentComponents(ref OrderItemComponent component)
        {
            SqlConnection connection2 = new SqlConnection();
            connection2.ConnectionString = conString;

            connection2.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection2;
            command.CommandType = CommandType.Text;
            command.CommandText = "select ComponentId, Portions from OrdersItemComponentComponents where OrderItemComponentId = " + component.Id;

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int componentId = reader.GetInt32(0);
                int portions = reader.GetInt32(1);

                OrderItemComponentComponent subComponent = new OrderItemComponentComponent(component.Id, componentId, portions);
                component.AddSubComponent(subComponent);
            }

            reader.Close();
            connection2.Close();
        }

        public static List<Ingredient> GetComponentIngredients(int id)
        {
            List<Ingredient> ingredients = new List<Ingredient>();

            return ingredients;
        }

        public static int OrderCount
        {
            get
            {
                return GetOrderCount();
            }
        }

        private static int GetOrderCount()
        {
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "select Count(Id) from Orders";

            SqlDataReader reader = command.ExecuteReader();
            reader.Read();

            int count = reader.GetInt32(0);

            reader.Close();
            connection.Close();

            return count;
        }

        public static int ProcessedOrderCount
        {
            get
            {
                return GetProcessedOrderCount();
            }
        }

        private static int GetProcessedOrderCount()
        {
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "select Count(Id) from Orders where Processed = 1";

            SqlDataReader reader = command.ExecuteReader();
            reader.Read();

            int count = reader.GetInt32(0);

            reader.Close();
            connection.Close();

            return count;
        }
    }
}
