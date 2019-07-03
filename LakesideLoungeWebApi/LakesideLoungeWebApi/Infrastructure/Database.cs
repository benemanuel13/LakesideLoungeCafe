using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;

using LakesideLoungeWebApi.Application;
using LakesideLoungeWebApi.Domain;
using LakesideLoungeWebApi.Helpers;

namespace LakesideLoungeWebApi.Infrastructure
{
    public static class Database
    {
        private static SqlConnection connection = new SqlConnection();
        //private static string conString = "Data Source=(localdb)\\LakesideLounge; Initial Catalog=LakesideLounge; Integrated Security=True";
        //private static string conString = "Data Source=(localdb)\\.\\SharedLounge; Initial Catalog=LakesideLounge; Integrated Security=True";
        //private static string conString = "Data Source=.\\SSMS; Initial Catalog=LakesideLounge; Integrated Security=True";
        private static string conString = "Data Source=.\\SSMS; Initial Catalog=LakesideLounge; Integrated Security=false; uid=LakesideLounge; password=k^hUe9%2!jeIhj&^";

        static Database()
        {
            connection.ConnectionString = conString;
        }

        //public static Variation GetVariation(int id, bool deep)
        //{
        //    connection.Open();

        //    SqlCommand command = new SqlCommand();
        //    command.Connection = connection;
        //    command.CommandType = CommandType.StoredProcedure;
        //    command.CommandText = "dbo.GetVariation";

        //    SqlParameter parameter = command.CreateParameter();
        //    parameter.ParameterName = "@Id";
        //    parameter.Value = id;

        //    command.Parameters.Add(parameter);

        //    SqlDataReader reader = command.ExecuteReader();

        //    int parentId = 0;
        //    string variationName = "";
        //    string variationDisplayName = "";
        //    //decimal variationCost = 0;
        //    decimal variationPrice = 0;

        //    if (reader.Read())
        //    {
        //        parentId = reader.GetInt32(0);
        //        variationName = Helper.StripSpaces(reader.GetString(1));
        //        variationDisplayName = Helper.StripSpaces(reader.GetString(2));
        //    }
        //    else
        //        throw new Exception("Invalid Variation!");

        //    GetPrice(id, 0, out variationPrice);

        //    reader.Close();
        //    connection.Close();

        //    Variation newVariation = new Variation(id, parentId, variationName, variationDisplayName, variationPrice);
        //    newVariation.Ingredients = GetIngredients(id, 0);

        //    GetSubVariations(ref newVariation);

        //    if (newVariation.HasChildren())
        //        return newVariation;
        //    else
        //        GetComponents(ref newVariation);

        //    return newVariation;
        //}

        //private static void GetSubVariations(ref Variation variation)
        //{
        //    connection.Open();

        //    SqlCommand command = new SqlCommand();
        //    command.Connection = connection;
        //    command.CommandType = CommandType.StoredProcedure;
        //    command.CommandText = "dbo.GetSubVariations";

        //    SqlParameter parameter = command.CreateParameter();
        //    parameter.ParameterName = "@parentId";
        //    parameter.Value = variation.Id;

        //    command.Parameters.Add(parameter);

        //    SqlDataReader reader = command.ExecuteReader();

        //    int count = 0;

        //    while (reader.Read())
        //    {
        //        int variationId = reader.GetInt32(0);
        //        int parentId = variation.Id;
        //        string variationName = Helper.StripSpaces(reader.GetString(1));
        //        string variationDisplayName = Helper.StripSpaces(reader.GetString(2));
        //        //decimal variationCost = reader.GetDecimal(3);
        //        decimal variationPrice;

        //        GetPrice(variationId, 0, out variationPrice);

        //        Variation newVariation = variation.AddVariation(variationId, parentId, variationName, variationDisplayName, variationPrice);
        //        variation.Children[count].Ingredients = GetIngredients(variationId, 0);

        //        ++count;
        //    }

        //    reader.Close();
        //    connection.Close();
        //}

        //public static bool PriceExists(int parentId, int parentType)
        //{
        //    connection.Open();

        //    SqlCommand command = new SqlCommand();
        //    command.Connection = connection;
        //    command.CommandType = CommandType.Text;
        //    command.CommandText = "Select [Price] from [Prices] Where [ParentId] = " + parentId + " and [ParentType] = " + parentType + " and [StartDate] = '" + Helper.GetDate(DateTime.Now) + "';";

        //    SqlDataReader reader = command.ExecuteReader();

        //    if(reader.Read())
        //    {
        //        reader.Close();
        //        connection.Close();
        //        return true;
        //    }

        //    reader.Close();
        //    connection.Close();

        //    return false;
        //}

        //public static bool CostExists(int ingredientId)
        //{
        //    connection.Open();

        //    SqlCommand command = new SqlCommand();
        //    command.Connection = connection;
        //    command.CommandType = CommandType.Text;
        //    command.CommandText = "Select [Cost] from [Costs] Where [IngredientId] = " + ingredientId + " and [StartDate] = '" + Helper.GetDate(DateTime.Now) + "';";

        //    SqlDataReader reader = command.ExecuteReader();

        //    if (reader.Read())
        //    {
        //        reader.Close();
        //        connection.Close();
        //        return true;
        //    }

        //    reader.Close();
        //    connection.Close();

        //    return false;
        //}

        //public static void AddPrice(int parentId, int parentType, DateTime startDate, decimal price)
        //{
        //    connection.Open();

        //    SqlCommand command = new SqlCommand();
        //    command.Connection = connection;
        //    command.CommandType = CommandType.Text;
        //    command.CommandText = "insert into [Prices] ([ParentType], [ParentId], [StartDate], [Price]) values (" + parentType + ", " + parentId + ", '" + Helper.GetDate(startDate) + "', " + price.ToString() + ")";
        //    command.ExecuteNonQuery();

        //    connection.Close();
        //}

        //public static void AddCost(int ingredientId, DateTime startDate, decimal cost)
        //{
        //    connection.Open();

        //    SqlCommand command = new SqlCommand();
        //    command.Connection = connection;
        //    command.CommandType = CommandType.Text;
        //    command.CommandText = "insert into [Costs] ([IngredientId], [StartDate], [Cost]) values (" + ingredientId + ", '" + Helper.GetDate(startDate) + "', " + cost.ToString() + ")";
        //    command.ExecuteNonQuery();

        //    connection.Close();
        //}

        //public static void DeletePrice(int parentId, int parentType, DateTime startDate)
        //{
        //    connection.Open();

        //    SqlCommand command = new SqlCommand();
        //    command.Connection = connection;
        //    command.CommandType = CommandType.Text;
        //    command.CommandText = "delete from [Prices] where [ParentType] = " + parentType + " and [ParentId] = " + parentId + " and [StartDate] = '" + Helper.GetDate(startDate) + "'";
        //    command.ExecuteNonQuery();

        //    connection.Close();
        //}

        //public static void DeleteCost(int ingredientId, DateTime startDate)
        //{
        //    connection.Open();

        //    SqlCommand command = new SqlCommand();
        //    command.Connection = connection;
        //    command.CommandType = CommandType.Text;
        //    command.CommandText = "delete from [Costs] where [IngredientId] = " + ingredientId + " and [StartDate] = '" + Helper.GetDate(startDate) + "'";
        //    command.ExecuteNonQuery();

        //    connection.Close();
        //}

        //private static void GetPrice(int parentId, int parentType, out decimal price)
        //{
        //    SqlConnection connection2 = new SqlConnection();
        //    connection2.ConnectionString = conString;

        //    connection2.Open();

        //    SqlCommand command = new SqlCommand();
        //    command.Connection = connection2;
        //    command.CommandType = CommandType.StoredProcedure;
        //    command.CommandText = "dbo.GetPrice";

        //    SqlParameter parameter = command.CreateParameter();
        //    parameter.ParameterName = "@parentId";
        //    parameter.Value = parentId;
        //    command.Parameters.Add(parameter);

        //    SqlParameter parameter2 = command.CreateParameter();
        //    parameter2.ParameterName = "@parentType";
        //    parameter2.Value = parentType;
        //    command.Parameters.Add(parameter2);

        //    SqlDataReader reader = command.ExecuteReader();

        //    if (reader.Read())
        //        price = reader.GetDecimal(0);
        //    else
        //        price = 0;

        //    connection2.Close();
        //}

        //private static void GetComponents(ref Variation variation)
        //{
        //    connection.Open();

        //    SqlCommand command = new SqlCommand();
        //    command.Connection = connection;
        //    command.CommandType = CommandType.StoredProcedure;
        //    command.CommandText = "dbo.GetComponents";

        //    SqlParameter parameter = command.CreateParameter();
        //    parameter.ParameterName = "@parentId";
        //    parameter.Value = variation.Id;

        //    command.Parameters.Add(parameter);

        //    SqlDataReader reader = command.ExecuteReader();
            
        //    while (reader.Read())
        //    {
        //        int id = reader.GetInt32(0);
        //        string name = Helper.StripSpaces(reader.GetString(1));
        //        string displayName = Helper.StripSpaces(reader.GetString(2));
        //        decimal price;
        //        bool isDefault = reader.GetBoolean(3);

        //        GetPrice(id, 1, out price);
                
        //        variation.AddComponent(id, name, displayName, price, isDefault);
        //    }

        //    reader.Close();
        //    connection.Close();
        //}

        //public static List<ItemModelBase> GetAllComponents(int parentId = 0)
        //{
        //    List<ItemModelBase> components = new List<ItemModelBase>();

        //    SqlConnection connection2 = new SqlConnection();
        //    connection2.ConnectionString = conString;

        //    connection2.Open();

        //    SqlCommand command = new SqlCommand();
        //    command.Connection = connection2;
        //    command.CommandType = CommandType.StoredProcedure;
        //    command.CommandText = "dbo.GetAllComponents";

        //    SqlDataReader reader = command.ExecuteReader();

        //    while (reader.Read())
        //    {
        //        ComponentModel newComponent = new ComponentModel(parentId, GetComponent(reader.GetInt32(0)));
        //        components.Add(newComponent);
        //    }

        //    connection2.Close();

        //    return components;
        //}

        //public static Component GetComponent(int id)
        //{
        //    connection.Open();

        //    SqlCommand command = new SqlCommand();
        //    command.Connection = connection;
        //    command.CommandType = CommandType.StoredProcedure;
        //    command.CommandText = "dbo.GetComponent";

        //    SqlParameter parameter = command.CreateParameter();
        //    parameter.ParameterName = "@id";
        //    parameter.Value = id;

        //    command.Parameters.Add(parameter);

        //    SqlDataReader reader = command.ExecuteReader();

        //    reader.Read();

        //    string name = Helper.StripSpaces(reader.GetString(0));
        //    string displayName = Helper.StripSpaces(reader.GetString(1));
        //    decimal price;

        //    GetPrice(id, 1, out price);

        //    reader.Close();
        //    connection.Close();

        //    return new Component(id, name, displayName, price, false);
        //}

        //public static List<Ingredient> GetIngredients()
        //{
        //    List<Ingredient> ingredients = new List<Ingredient>();

        //    connection.Open();

        //    SqlCommand command = new SqlCommand();
        //    command.Connection = connection;
        //    command.CommandType = CommandType.StoredProcedure;
        //    command.CommandText = "dbo.GetIngredients";

        //    SqlDataReader reader = command.ExecuteReader();

        //    while (reader.Read())
        //    {
        //        int id = reader.GetInt32(0);
        //        string name = Helper.StripSpaces(reader.GetString(1));
        //        string displayName = Helper.StripSpaces(reader.GetString(2));

        //        decimal cost = GetIngredientCost(id);

        //        ingredients.Add(new Ingredient(id, name, displayName, cost, 1));
        //    }

        //    reader.Close();
        //    connection.Close();

        //    return ingredients;
        //}

        //public static Ingredient GetIngredient(int id)
        //{
        //    connection.Open();

        //    SqlCommand command = new SqlCommand();
        //    command.Connection = connection;
        //    command.CommandType = CommandType.StoredProcedure;
        //    command.CommandText = "dbo.GetIngredient";

        //    SqlParameter parameter = command.CreateParameter();
        //    parameter.ParameterName = "@id";
        //    parameter.Value = id;

        //    command.Parameters.Add(parameter);

        //    SqlDataReader reader = command.ExecuteReader();
        //    reader.Read();

        //    string name = Helper.StripSpaces(reader.GetString(0));
        //    string displayName = Helper.StripSpaces(reader.GetString(1));
        //    decimal cost = GetIngredientCost(id);
        //    int portions = 0;

        //    Ingredient ingredient = new Ingredient(id, name, displayName, cost, portions);

        //    reader.Close();
        //    connection.Close();

        //    return ingredient;
        //}

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

        //public static List<Ingredient> GetIngredients(int itemId, int itemType)
        //{
        //    List<Ingredient> ingredients = new List<Ingredient>();

        //    SqlConnection connection2 = new SqlConnection();
        //    connection2.ConnectionString = conString;

        //    connection2.Open();

        //    SqlCommand command = new SqlCommand();
        //    command.Connection = connection2;
        //    command.CommandType = CommandType.StoredProcedure;
        //    command.CommandText = "dbo.GetItemIngredients";

        //    SqlParameter parameter = command.CreateParameter();
        //    parameter.ParameterName = "@itemId";
        //    parameter.Value = itemId;

        //    command.Parameters.Add(parameter);

        //    SqlParameter parameter2 = command.CreateParameter();
        //    parameter2.ParameterName = "@itemType";
        //    parameter2.Value = itemType;

        //    command.Parameters.Add(parameter2);

        //    SqlDataReader reader = command.ExecuteReader();

        //    while (reader.Read())
        //    {
        //        int id = reader.GetInt32(0);
        //        int portions = reader.GetInt32(1);
        //        ingredients.Add(new Ingredient(id, null, null, 0, portions));
        //    }

        //    reader.Close();
        //    connection2.Close();

        //    return ingredients;
        //}

        //public static void InsertItemIngredients(int itemId, int itemType, IngredientsModel model)
        //{
        //    connection.Open();

        //    SqlCommand command = new SqlCommand();
        //    command.Connection = connection;
        //    command.CommandType = CommandType.Text;

        //    foreach (IngredientModel ingModel in model.Children)
        //    {
        //        string query = "insert into [dbo].[ItemIngredients] ([ItemId], [ItemType], [IngredientId], [Portions]) values (" + itemId.ToString() + ", " + itemType.ToString() + ", " + ingModel.Id.ToString() + ", " + ingModel.Portions.ToString() + ");";
        //        command.CommandText = query;
        //        command.ExecuteNonQuery();
        //    }

        //    connection.Close();
        //}

        //public static void DeleteItemIngredients(int itemId, int itemType)
        //{
        //    connection.Open();

        //    SqlCommand command = new SqlCommand();
        //    command.Connection = connection;
        //    command.CommandType = CommandType.StoredProcedure;
        //    command.CommandText = "dbo.DeleteItemIngredients";

        //    SqlParameter parameter = command.CreateParameter();
        //    parameter.ParameterName = "@itemId";
        //    parameter.Value = itemId;
        //    command.Parameters.Add(parameter);

        //    SqlParameter parameter2 = command.CreateParameter();
        //    parameter2.ParameterName = "@itemType";
        //    parameter2.Value = itemType;
        //    command.Parameters.Add(parameter2);

        //    command.ExecuteNonQuery();

        //    connection.Close();
        //}

        //public static void UpdateVariation(int id, string name, string displayName)
        //{
        //    connection.Open();

        //    SqlCommand command = new SqlCommand();
        //    command.Connection = connection;
        //    command.CommandType = CommandType.StoredProcedure;
        //    command.CommandText = "dbo.UpdateVariation";

        //    SqlParameter parameter = command.CreateParameter();
        //    parameter.ParameterName = "@itemId";
        //    parameter.Value = id;
        //    command.Parameters.Add(parameter);

        //    SqlParameter parameter2 = command.CreateParameter();
        //    parameter2.ParameterName = "@name";
        //    parameter2.Value = name;
        //    command.Parameters.Add(parameter2);

        //    SqlParameter parameter3 = command.CreateParameter();
        //    parameter3.ParameterName = "@displayName";
        //    parameter3.Value = displayName;
        //    command.Parameters.Add(parameter3);

        //    command.ExecuteNonQuery();

        //    connection.Close();
        //}

        //public static void UpdateComponent(int id, string name, string displayName)
        //{
        //    connection.Open();

        //    SqlCommand command = new SqlCommand();
        //    command.Connection = connection;
        //    command.CommandType = CommandType.StoredProcedure;
        //    command.CommandText = "dbo.UpdateComponent";

        //    SqlParameter parameter = command.CreateParameter();
        //    parameter.ParameterName = "@itemId";
        //    parameter.Value = id;
        //    command.Parameters.Add(parameter);

        //    SqlParameter parameter2 = command.CreateParameter();
        //    parameter2.ParameterName = "@name";
        //    parameter2.Value = name;
        //    command.Parameters.Add(parameter2);

        //    SqlParameter parameter3 = command.CreateParameter();
        //    parameter3.ParameterName = "@displayName";
        //    parameter3.Value = displayName;
        //    command.Parameters.Add(parameter3);

        //    command.ExecuteNonQuery();

        //    connection.Close();
        //}

        //public static void UpdateIngredient(int id, string name, string displayName)
        //{
        //    connection.Open();

        //    SqlCommand command = new SqlCommand();
        //    command.Connection = connection;
        //    command.CommandType = CommandType.StoredProcedure;
        //    command.CommandText = "dbo.UpdateIngredient";

        //    SqlParameter parameter = command.CreateParameter();
        //    parameter.ParameterName = "@itemId";
        //    parameter.Value = id;
        //    command.Parameters.Add(parameter);

        //    SqlParameter parameter2 = command.CreateParameter();
        //    parameter2.ParameterName = "@name";
        //    parameter2.Value = name;
        //    command.Parameters.Add(parameter2);

        //    SqlParameter parameter3 = command.CreateParameter();
        //    parameter3.ParameterName = "@displayName";
        //    parameter3.Value = displayName;
        //    command.Parameters.Add(parameter3);

        //    command.ExecuteNonQuery();

        //    connection.Close();
        //}

        public static bool SaveOrder(Order order)
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

            command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "insert into [dbo].[Orders] ([Id], [Name], [CustomerType], [Date], [Processed]) Values (" + order.Id.ToString() + ", '" + order.Name + "', " + order.CustomerType + ", '" + order.Date.ToLongDateString() + "', 0)";
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
            command.ExecuteNonQuery();

            string query = "insert into OrderItems (Id, OrderId, VariationId, InOutStatus, Discount) values (" + item.Id.ToString() + ", " + item.OrderId.ToString() + ", " + item.VariationId.ToString() + ", " + item.InOutStatus + ", " + item.DiscountId.ToString() + ")";
            command.CommandText = query;
            command.ExecuteNonQuery();

            foreach (OrderItemComponent component in item.Components)
            {
                query = "insert into OrderItemComponents (Id, OrderItemId, VariationId, ComponentId, Portions) values (" + component.Id.ToString() + "," + item.Id.ToString() + ", " + item.VariationId + "," + component.ComponentId.ToString() + ", " + component.Portions.ToString() + ")";
                command.CommandText = query;
                command.ExecuteNonQuery();

                query = "delete from OrderItemComponentComponents where OrderItemComponentId = " + component.Id;
                command.CommandText = query;
                command.ExecuteNonQuery();

                foreach(OrderItemComponentComponent subComponent in component.Components)
                {
                    query = "insert into OrderItemComponentComponents (OrderItemComponentId, ComponentId, Portions) values (" + component.Id.ToString() + "," + subComponent.ComponentId.ToString() + ", " + component.Portions.ToString() + ")";
                    command.CommandText = query;
                    command.ExecuteNonQuery();
                }
            }

            connection2.Close();
        }

        public static void SetComponentSelected(int parentId, int componentId)
        {
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;

            command.CommandText = "insert into [VariationComponents] ([VariationId], [ComponentId], [Default]) values (" + parentId.ToString() + ", " + componentId.ToString() + ", 0)";
            command.ExecuteNonQuery();

            connection.Close();
        }

        public static void SetComponentDeselected(int parentId, int componentId)
        {
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "delete from [VariationComponents] where VariationId = " + parentId.ToString() + " and ComponentId = " + componentId.ToString();
            command.ExecuteNonQuery();

            connection.Close();
        }

        public static void SetComponentDefault(int parentId, int componentId)
        {
            UpdateComponentDefaultStatus(parentId, componentId, 1);
        }

        public static void SetComponentUnDefault(int parentId, int componentId)
        {
            UpdateComponentDefaultStatus(parentId, componentId, 0);
        }

        private static void UpdateComponentDefaultStatus(int parentId, int componentId, int isDefault)
        {
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "update [VariationComponents] set [Default] = " + isDefault.ToString() + " where VariationId = " + parentId.ToString() + " and ComponentId = " + componentId.ToString();
            command.ExecuteNonQuery();

            connection.Close();
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

        public static List<Update> GetUpdates()
        {
            List<Update> updates = new List<Update>();

            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "dbo.GetUpdates";

            SqlDataReader reader = command.ExecuteReader();

            while(reader.Read())
            {
                int id = reader.GetInt32(0);
                string updateText = Helper.StripSpaces(reader.GetString(1));

                Update newUpdate = new Update(id, updateText);
                updates.Add(newUpdate);

                SendUpdate(id);
            }

            reader.Close();
            connection.Close();

            return updates;
        }

        private static void SendUpdate(int id)
        {
            SqlConnection connection2 = new SqlConnection();
            connection2.ConnectionString = conString;

            connection2.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection2;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "dbo.SendUpdate";

            SqlParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@id";
            parameter.Value = id;

            command.Parameters.Add(parameter);

            command.ExecuteNonQuery();

            connection2.Close();
        }

        public static void Reset()
        {
            SqlConnection.ClearAllPools();
        }
    }
}
