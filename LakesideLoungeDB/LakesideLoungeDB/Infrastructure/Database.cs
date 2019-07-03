using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Xml;

namespace LakesideLoungeDB.Infrastructure
{
    public static class Database
    {
        private static SqlConnection connection = new SqlConnection();
        //private static string conString = "Data Source=(localdb)\\.\\SharedLounge; Integrated Security=True";
        //private static string conString = "Data Source=TBENSERVER\\SQLEXPRESS; Integrated Security=True";
        //private static string conString = "Data Source=.\\SSMS; Integrated Security=True";
        private static string conString = "Data Source=.\\SSMS; Integrated Security=false; uid=LakesideLounge; password=k^hUe9%2!jeIhj&^";

        static Database()
        {
            connection.ConnectionString = conString;
        }

        public static void CreateDatabase()
        {
            Lakeside set = Lakeside.Default;
            string path = set.UserPath;

            //if (File.Exists(path + "\\LakesideLounge.Mdf"))
            //{
            //    File.Delete(path + "\\LakesideLounge.Mdf");
            //    File.Delete(path + "\\LakesideLounge_log.ldf");
            //}

            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;

            command.CommandText = "drop database LakesideLounge";

            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Drop Failed: " + ex.Message);
            }

            command.CommandText = "create database LakesideLounge";
            command.ExecuteNonQuery();

            connection.Close();
        }

        public static void CreateTables()
        {
            conString = "Data Source=.\\SSMS; Initial Catalog=LakesideLounge; Integrated Security=false; uid=LakesideLounge; password=k^hUe9%2!jeIhj&^";
            //conString = "Data Source=(localdb)\\.\\SharedLounge; Initial Catalog=LakesideLounge; Integrated Security=True";
            //conString = "Data Source=TBENSERVER\\SQLEXPRESS; Initial Catalog=LakesideLounge; Integrated Security=True";
            //conString = "Data Source=.\\SSMS; Initial Catalog=LakesideLounge; Integrated Security=True";

            connection.ConnectionString = conString;

            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;

            string query = "create table [dbo].[Orders] ([Id] int NOT NULL, [Name] nchar(35), [CustomerType] int, [Date] datetime, [Processed] bit, PRIMARY KEY CLUSTERED ([Id] ASC));";
            command.CommandText = query;
            command.ExecuteNonQuery();

            query = "create table [dbo].[OrderItems] ([Id] int NOT NULL, [OrderId] int, [VariationId] int, [InOutStatus] int, [Discount] int, PRIMARY KEY CLUSTERED ([Id] ASC));";
            command.CommandText = query;
            command.ExecuteNonQuery();

            query = "create table [dbo].[OrderItemComponents] ([Id] int NOT NULL, [OrderItemId] int NOT NULL, [VariationId] int NOT NULL, [ComponentId] int NOT NULL, [Portions] int)";
            command.CommandText = query;
            command.ExecuteNonQuery();

            query = "create table [dbo].[OrderItemComponentComponents] ([OrderItemComponentId] int NOT NULL, [ComponentId] int NOT NULL, [Portions] int)";
            command.CommandText = query;
            command.ExecuteNonQuery();

            query = "create table [dbo].[Variations] ([Id] int NOT NULL, [ParentId] int NOT NULL, [Name] nchar(35), [DisplayName] nchar(50), [Points] float(1), [PointPrice] money, [Removed] bit, [Position] int PRIMARY KEY CLUSTERED ([Id] ASC));";
            command.CommandText = query;
            command.ExecuteNonQuery();

            query = "create table [dbo].[Components] ([Id] int NOT NULL, [Name] nchar(35), [DisplayName] nchar(35), [Removed] bit, PRIMARY KEY CLUSTERED ([Id] ASC));";
            command.CommandText = query;
            command.ExecuteNonQuery();

            query = "create table [dbo].[VariationComponents] ([VariationId] int, [ComponentId] int, [Default] bit, [Portions] int, [Group] int, [Points] float(1), [Position] int)";
            command.CommandText = query;
            command.ExecuteNonQuery();

            query = "create table [dbo].[ComponentComponents] ([ParentId] int, [ComponentId] int, [Default] bit, [Group] int, [Position] int)";
            command.CommandText = query;
            command.ExecuteNonQuery();

            query = "create table [dbo].[Prices] ([ParentType] int, [ParentId] int, [StartDate] datetime, [Price] money)";
            command.CommandText = query;
            command.ExecuteNonQuery();

            query = "create table [dbo].[Ingredients] ([Id] int, [Name] nchar(35), [DisplayName] nchar(35), [PortionSize] int, [PortionType] int)";
            command.CommandText = query;
            command.ExecuteNonQuery();

            query = "create table [dbo].[ItemIngredients] ([ItemId] int, [ItemType] int, [IngredientId] int, [Portions] int)";
            command.CommandText = query;
            command.ExecuteNonQuery();

            //query = "create table [dbo].[Costs] ([IngredientId] int, [StartDate] datetime, [Cost] money)";
            //command.CommandText = query;
            //command.ExecuteNonQuery();

            query = "create table [dbo].[Transactions] ([Filename] nchar(35))";
            command.CommandText = query;
            command.ExecuteNonQuery();

            query = "create table [dbo].[Stock] ([IngredientId] int, [OriginalItems] int, [CurrentItems] int, [CurrentPortions] int, [PortionsPerItem] int, [CostPerItem] money, [Date] datetime)";
            command.CommandText = query;
            command.ExecuteNonQuery();

            query = "create table [dbo].[Updates] ([Id] int NOT NULL IDENTITY, [UpdateText] nchar(125), [Sent] bit)";
            command.CommandText = query;
            command.ExecuteNonQuery();

            connection.Close();
        }

        public static void CreateStoredProcedures()
        {
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;

            string query = "CREATE PROCEDURE [dbo].[GetVariation] @Id int AS SELECT [ParentId], [Name], [DisplayName], [Points], [PointPrice], [Removed], [Position] From Variations Where Id = @Id RETURN 0";
            command.CommandText = query;
            command.ExecuteNonQuery();

            query = "CREATE PROCEDURE [dbo].[GetSubVariations] @parentId int AS SELECT [Id], [Name], [DisplayName], [Points], [PointPrice], [Removed], [Position] From Variations Where ParentId = @parentId RETURN 0";
            command.CommandText = query;
            command.ExecuteNonQuery();

            query = "CREATE PROCEDURE [dbo].[GetVariationComponents] @parentId int AS SELECT [Components].[Id], [Components].[Name], [Components].[DisplayName], [VariationComponents].[Default], [VariationComponents].[Portions], [VariationComponents].[Group], [VariationComponents].[Points], [VariationComponents].[Position] From [VariationComponents] inner join [Components] on [VariationComponents].[ComponentId] = [Components].[Id] Where [VariationComponents].[VariationId] = @parentId RETURN 0";
            command.CommandText = query;
            command.ExecuteNonQuery();

            query = "CREATE PROCEDURE [dbo].[GetComponentComponents] @parentId int AS SELECT [Components].[Id], [Components].[Name], [Components].[DisplayName], [ComponentComponents].[Default], [ComponentComponents].[Position] From [ComponentComponents] inner join [Components] on [ComponentComponents].[ComponentId] = [Components].[Id] Where [ComponentComponents].[ParentId] = @parentId RETURN 0";
            command.CommandText = query;
            command.ExecuteNonQuery();

            query = "CREATE PROCEDURE [dbo].[GetAllComponents] AS SELECT [Components].[Id] from [Components] RETURN 0";
            command.CommandText = query;
            command.ExecuteNonQuery();

            query = "CREATE PROCEDURE [dbo].[GetPrice] @parentId int, @parentType int AS SELECT [Prices].[Price] From [Prices] WHERE @parentId = [Prices].[ParentId] AND @parentType = [Prices].[ParentType] Order By [Prices].[StartDate] DESC RETURN 0";
            command.CommandText = query;
            command.ExecuteNonQuery();

            query = "CREATE PROCEDURE [dbo].[GetIngredients] AS SELECT [Ingredients].[Id], [Ingredients].[Name], [Ingredients].[DisplayName], [Ingredients].[PortionSize], [Ingredients].[PortionType] From [Ingredients] ORDER BY [Ingredients].[Name] RETURN 0";
            command.CommandText = query;
            command.ExecuteNonQuery();

            //query = "CREATE PROCEDURE [dbo].[GetCost] @ingredientId int AS SELECT [Costs].[Cost] From [Costs] WHERE @ingredientId = [Costs].[IngredientId] Order By [Costs].[StartDate] DESC RETURN 0";
            //command.CommandText = query;
            //command.ExecuteNonQuery();

            query = "CREATE PROCEDURE [dbo].[GetItemIngredients] @itemId int, @itemType int AS SELECT [ItemIngredients].[IngredientId], [ItemIngredients].[Portions] From [ItemIngredients] WHERE @itemId = [ItemIngredients].[ItemId] AND [ItemIngredients].[ItemType] = @itemType RETURN 0";
            command.CommandText = query;
            command.ExecuteNonQuery();

            query = "CREATE PROCEDURE [dbo].[DeleteItemIngredients] @itemId int, @itemType int AS DELETE From [ItemIngredients] WHERE @itemId = [ItemIngredients].[ItemId] AND [ItemIngredients].[ItemType] = @itemType RETURN 0";
            command.CommandText = query;
            command.ExecuteNonQuery();

            query = "CREATE PROCEDURE [dbo].[UpdateVariation] @itemId int, @name nchar(25), @displayName nchar(50), @points float(1), @pointPrice money AS UPDATE [Variations] Set [Name] = @name, [DisplayName] = @displayName, [Points] = @points, [PointPrice] = @pointPrice WHERE @itemId = [Variations].[Id] RETURN 0";
            command.CommandText = query;
            command.ExecuteNonQuery();

            query = "CREATE PROCEDURE [dbo].[UpdateComponent] @itemId int, @name nchar(25), @displayName nchar(50) AS UPDATE [Components] Set [Name] = @name, [DisplayName] = @displayName WHERE @itemId = [Components].[Id] RETURN 0";
            command.CommandText = query;
            command.ExecuteNonQuery();

            query = "CREATE PROCEDURE [dbo].[GetComponent] @id int AS SELECT [Components].[Name], [Components].[DisplayName], [Components].[Removed] From [Components] Where [Components].[Id] = @id RETURN 0";
            command.CommandText = query;
            command.ExecuteNonQuery();

            query = "CREATE PROCEDURE [dbo].[UpdateIngredient] @itemId int, @name nchar(25), @displayName nchar(50), @portionSize int, @portionType int AS UPDATE [Ingredients] Set [Name] = @name, [DisplayName] = @displayName, [PortionSize] = @portionSize, [PortionType] = @portionType WHERE @itemId = [Ingredients].[Id] RETURN 0";
            command.CommandText = query;
            command.ExecuteNonQuery();

            query = "CREATE PROCEDURE [dbo].[GetIngredient] @id int AS SELECT [Ingredients].[Name], [Ingredients].[DisplayName], [Ingredients].[PortionSize], [Ingredients].[PortionType] From [Ingredients] Where [Ingredients].[Id] = @id RETURN 0";
            command.CommandText = query;
            command.ExecuteNonQuery();

            query = "CREATE PROCEDURE [dbo].[InsertStockItem] @ingredientId int, @items int, @portionsPerItem int, @costPerItem money AS INSERT INTO [Stock] ([IngredientId], [OriginalItems], [CurrentItems], [CurrentPortions], [PortionsPerItem], [CostPerItem]) VALUES (@ingredientId, @items, @items, 0, @portionsPerItem, @costPerItem)";
            command.CommandText = query;
            command.ExecuteNonQuery();

            query = "CREATE PROCEDURE [dbo].[GetCurrentStock] @id int AS SELECT [Stock].[OriginalItems], [Stock].[CurrentItems], [Stock].[CurrentPortions], [Stock].[PortionsPerItem], [Stock].[CostPerItem] From [Stock] Where [Stock].[IngredientId] = @id AND ([Stock].[CurrentItems] > 0 OR [Stock].[CurrentPortions] > 0) RETURN 0";
            command.CommandText = query;
            command.ExecuteNonQuery();

            query = "CREATE PROCEDURE [dbo].[InsertUpdate] @updateText nchar(125) AS INSERT INTO [Updates] ([UpdateText], [Sent]) VALUES (@updateText, 0)";
            command.CommandText = query;
            command.ExecuteNonQuery();

            query = "CREATE PROCEDURE [dbo].[GetUpdates] AS SELECT * From [dbo].[Updates] Where [Updates].[Sent] = 0 RETURN 0";
            command.CommandText = query;
            command.ExecuteNonQuery();

            query = "CREATE PROCEDURE [dbo].[SendUpdate] @id int AS UPDATE [dbo].[Updates] Set [Sent] = 1 Where [Updates].[Id] = @id";
            command.CommandText = query;
            command.ExecuteNonQuery();

            query = "CREATE PROCEDURE [dbo].[GetNewVariationId] AS SELECT Max([Id]) From [dbo].[Variations]";
            command.CommandText = query;
            command.ExecuteNonQuery();

            query = "CREATE PROCEDURE [dbo].[InsertVariation] @id int, @parentId int, @name nchar(35), @displayName nchar(50), @position int AS INSERT INTO [dbo].[Variations] ([Id], [ParentId], [Name], [DisplayName], [Points], [PointPrice], [Removed], [Position]) VALUES (@id, @parentId, @name, @displayName, 0, 0.0, 0, @position)";
            command.CommandText = query;
            command.ExecuteNonQuery();

            query = "CREATE PROCEDURE [dbo].[GetNewComponentId] AS SELECT Max([Id]) From [dbo].[Components]";
            command.CommandText = query;
            command.ExecuteNonQuery();

            query = "CREATE PROCEDURE [dbo].[InsertComponent] @id int, @name nchar(35), @displayName nchar(50) AS INSERT INTO [dbo].[Components] ([Id], [Name], [DisplayName], [Removed]) VALUES (@id, @name, @displayName, 0)";
            command.CommandText = query;
            command.ExecuteNonQuery();

            query = "CREATE PROCEDURE [dbo].[UpdateVariationComponent] @variationId int, @componentId int, @portions int, @group int, @points float(1) AS UPDATE [dbo].[VariationComponents] Set [Points] = @points, [Portions] = @portions, [Group] = @group Where [VariationComponents].[VariationId] = @variationId and [VariationComponents].[ComponentId] = @componentId";
            command.CommandText = query;
            command.ExecuteNonQuery();

            query = "CREATE PROCEDURE [dbo].[AddRemoveVariation] @id int, @remove bit AS UPDATE [dbo].[Variations] Set [Removed] = @remove Where [Variations].[Id] = @id";
            command.CommandText = query;
            command.ExecuteNonQuery();

            query = "CREATE PROCEDURE [dbo].[AddRemoveComponent] @id int, @remove bit AS UPDATE [dbo].[Components] Set [Removed] = @remove Where [Components].[Id] = @id";
            command.CommandText = query;
            command.ExecuteNonQuery();

            query = "CREATE PROCEDURE [dbo].[DeleteVariation] @id int AS DELETE From [Variations] Where [Variations].[Id] = @id";
            command.CommandText = query;
            command.ExecuteNonQuery();

            query = "CREATE PROCEDURE [dbo].[DeleteVariationComponents] @variationId int AS DELETE From [VariationComponents] Where [VariationComponents].[VariationId] = @variationId";
            command.CommandText = query;
            command.ExecuteNonQuery();

            query = "CREATE PROCEDURE [dbo].[UpdateComponentComponent] @parentId int, @componentId int, @group int AS UPDATE [ComponentComponents] Set [Group] = @group Where [ParentId] = @parentId and [ComponentId] = @componentId";
            command.CommandText = query;
            command.ExecuteNonQuery();

            query = "CREATE PROCEDURE [dbo].[GetSubComponent] @parentId int, @componentId int AS Select [Group], [Position] From [ComponentComponents] Where [ParentId] = @parentId and [ComponentId] = @componentId";
            command.CommandText = query;
            command.ExecuteNonQuery();

            query = "CREATE PROCEDURE [dbo].[GetMaxIngredientId] As Select Max(Id) From Ingredients";
            command.CommandText = query;
            command.ExecuteNonQuery();

            query = "CREATE PROCEDURE [dbo].[AddIngredient] @id int AS Insert Into [dbo].[Ingredients] ([Id], [Name], [DisplayName], [PortionSize], [PortionType]) Values (@id, 'New Ingredient', 'New Ingredient', 0, 0)";
            command.CommandText = query;
            command.ExecuteNonQuery();

            connection.Close();
        }

        public static void PopulateDatabase()
        {
            InsertIngredients();
            InsertComponents();
            InsertVariations();
        }

        private static void InsertVariations()
        {
            XmlDocument doc = new XmlDocument();
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Assets\\Variations.xml";
            doc.Load(path);

            connection.Open();
            InsertVariation(0, doc.FirstChild, 1);
            connection.Close();
        }

        private static void InsertVariation(int parentId, XmlNode node, int variationPosition)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;

            int id = Int32.Parse(node.Attributes.GetNamedItem("Id").Value);

            string name = node.Attributes.GetNamedItem("Name").Value;
            string displayName = node.Attributes.GetNamedItem("DisplayName").Value;

            float points = 0;
            decimal pointPrice = 0.0M;

            if (node.Attributes.GetNamedItem("Points") != null)
            {
                points = float.Parse(node.Attributes.GetNamedItem("Points").Value);
                pointPrice = Decimal.Parse(node.Attributes.GetNamedItem("PointPrice").Value);
            }

            string query = "insert into [dbo].[Variations] ([Id], [ParentId], [Name], [DisplayName], [Points], [PointPrice], [Removed], [Position]) values (" + id.ToString() + ", " + parentId.ToString() + ", '" + name + "', '" + displayName + "', " + points + ", "+ pointPrice + ", 0, " + variationPosition + ");";
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
                else if (node.ChildNodes.Count == 2 && node.ChildNodes[1].Name == "Ingredients")
                    InsertItemIngredients(id, 0, node.ChildNodes[1]);
                else if (node.ChildNodes.Count == 3)
                {
                    InsertPrices(id, 0, node.ChildNodes[1].ChildNodes);
                    InsertItemIngredients(id, 0, node.ChildNodes[2]);
                }
            }
            else if (node.HasChildNodes && node.FirstChild.Name == "Components")
            {
                int position = 1;
                foreach (XmlNode childNode in node.FirstChild.ChildNodes)
                {
                    InsertVariationComponent(id, childNode, position);
                    ++position;
                }

                if (node.ChildNodes.Count == 2 && node.ChildNodes[1].Name == "Prices")
                    InsertPrices(id, 0, node.ChildNodes[1].ChildNodes);
                else if(node.ChildNodes.Count == 2 && node.ChildNodes[1].Name == "Ingredients")
                    InsertItemIngredients(id, 0, node.ChildNodes[1]);
                else if (node.ChildNodes.Count == 3)
                {
                    InsertPrices(id, 0, node.ChildNodes[1].ChildNodes);
                    InsertItemIngredients(id, 0, node.ChildNodes[2]);
                }
            }
            else if (node.HasChildNodes && node.FirstChild.Name == "Prices")
            {
                InsertPrices(id, 0, node.FirstChild.ChildNodes);

                if (node.ChildNodes.Count == 2 && node.ChildNodes[1].Name == "Ingredients")
                    InsertItemIngredients(id, 0, node.ChildNodes[1]);
            }
            else if (node.HasChildNodes && node.FirstChild.Name == "Ingredients")
            {
                InsertItemIngredients(id, 0, node.FirstChild);
            }
        }

        private static void InsertIngredients()
        {
            XmlDocument doc = new XmlDocument();
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Assets\\Ingredients.xml";
            doc.Load(path);

            connection.Open();

            foreach (XmlNode node in doc.ChildNodes[1])
                InsertIngredient(node);

            connection.Close();
        }

        private static void InsertIngredient(XmlNode node)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;

            int id = Int32.Parse(node.Attributes.GetNamedItem("Id").Value);
            string name = node.Attributes.GetNamedItem("Name").Value;
            string displayName = node.Attributes.GetNamedItem("DisplayName").Value;

            string query = "insert into [dbo].[Ingredients] ([Id], [Name], [DisplayName], [PortionSize], [PortionType]) values (" + id.ToString() + ", '" + name + "', '" + displayName + "', 0, 0);";
            command.CommandText = query;
            command.ExecuteNonQuery();

            //InsertCosts(id, node.FirstChild);
        }

        //private static void InsertCosts(int ingredientId, XmlNode node)
        //{
        //    foreach (XmlNode costNode in node)
        //    {
                //int id = Int32.Parse(costNode.Attributes.GetNamedItem("Id").Value);
        //        DateTime startDate = DateTime.Parse(costNode.Attributes.GetNamedItem("StartDate").Value);
        //        decimal cost = decimal.Parse(costNode.Attributes.GetNamedItem("Cost").Value);

        //        InsertCost(ingredientId, startDate, cost);
        //    }
        //}

        //private static void InsertCost(int ingredientId, DateTime startDate, decimal cost)
        //{
        //    SqlCommand command = new SqlCommand();
        //    command.Connection = connection;
        //    command.CommandType = CommandType.Text;
            
        //    string query = "insert into [dbo].[Costs] ([IngredientId], [StartDate], [Cost]) values (" + ingredientId + ", '" + startDate.ToShortDateString() + "', " + cost.ToString() + ");";
        //    command.CommandText = query;
        //    command.ExecuteNonQuery();
        //}

        private static void InsertComponents()
        {
            XmlDocument doc = new XmlDocument();
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Assets\\Components.xml";
            doc.Load(path);

            connection.Open();

            foreach (XmlNode node in doc.ChildNodes[1])
                InsertComponent(node);

            connection.Close();
        }

        private static void InsertComponent(XmlNode node)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;

            int id = Int32.Parse(node.Attributes.GetNamedItem("Id").Value);
            string name = node.Attributes.GetNamedItem("Name").Value;
            string displayName = node.Attributes.GetNamedItem("DisplayName").Value;

            string query = "insert into [dbo].[Components] ([Id], [Name], [DisplayName], [Removed]) values (" + id.ToString() + ", '" + name + "', '" + displayName + "', 0);";
            command.CommandText = query;
            command.ExecuteNonQuery();

            InsertPrices(id, 1, node.FirstChild.ChildNodes);

            if (node.ChildNodes.Count == 2 && node.ChildNodes[1].Name == "Ingredients")
                InsertItemIngredients(id, 1, node.ChildNodes[1]);
            else if (node.ChildNodes.Count == 2 && node.ChildNodes[1].Name == "Components")
                InsertComponentComponents(id, node.ChildNodes[1]);

            if (node.ChildNodes.Count == 3)
                InsertComponentComponents(id, node.ChildNodes[2]);
        }

        private static void InsertComponentComponents(int parentId, XmlNode node)
        {
            int position = 1;

            foreach (XmlNode childNode in node.ChildNodes)
            {
                InsertComponentComponent(parentId, childNode, position);
                ++position;
            }
        }

        private static void InsertVariationComponent(int variationId, XmlNode node, int position)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;

            int id = Int32.Parse(node.Attributes.GetNamedItem("Id").Value);
            int isDefault = 0;

            if (node.Attributes.GetNamedItem("Default") != null && (node.Attributes.GetNamedItem("Default").Value == "true" || node.Attributes.GetNamedItem("Default").Value == "True"))
                isDefault = 1;

            int portions = 1;

            if (node.Attributes.GetNamedItem("Portions") != null)
                portions = int.Parse(node.Attributes.GetNamedItem("Portions").Value);

            int group = 0;
            if (node.Attributes.GetNamedItem("Group") != null)
                group = int.Parse(node.Attributes.GetNamedItem("Group").Value);

            float points = 0;

            if (node.Attributes.GetNamedItem("Points") != null)
                points = float.Parse(node.Attributes.GetNamedItem("Points").Value);

            string query = "insert into [dbo].[VariationComponents] ([VariationId], [ComponentId], [Default], [Portions], [Group], [Points], [Position]) values (" + variationId.ToString() + ", " + id.ToString() + ", " + isDefault.ToString() + ", " + portions.ToString() + ", " + group + ", " + points + ", " + position + ");";
            command.CommandText = query;
            command.ExecuteNonQuery();
        }

        private static void InsertComponentComponent(int parentId, XmlNode node, int position)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;

            int id = Int32.Parse(node.Attributes.GetNamedItem("Id").Value);
            int isDefault = 0;

            if (node.Attributes.GetNamedItem("Default") != null && (node.Attributes.GetNamedItem("Default").Value == "true" || node.Attributes.GetNamedItem("Default").Value == "True"))
                isDefault = 1;

            string query = "insert into [dbo].[ComponentComponents] ([ParentId], [ComponentId], [Default], [Position]) values (" + parentId.ToString() + ", " + id.ToString() + ", " + isDefault.ToString() + ", " + position + ");";
            command.CommandText = query;
            command.ExecuteNonQuery();
        }

        private static void InsertPrices(int id, int parentType, XmlNodeList nodes)
        {
            foreach (XmlNode childNode in nodes)
            {
                //int priceId = Int32.Parse(childNode.Attributes.GetNamedItem("Id").Value);
                DateTime startDate = DateTime.Parse(childNode.Attributes.GetNamedItem("StartDate").Value);
                //decimal cost = decimal.Parse(childNode.Attributes.GetNamedItem("Cost").Value);
                decimal price = decimal.Parse(childNode.Attributes.GetNamedItem("Price").Value);

                //if (!(childNode.Attributes.GetNamedItem("EndDate") == null))
                //    InsertPrice(priceId, parentType, id, startDate, DateTime.Parse(childNode.Attributes.GetNamedItem("EndDate").Value), cost, price);
                //else
                InsertPrice(parentType, id, startDate, price);
            }
        }

        private static void InsertPrice(int parentType, int parentId, DateTime startDate, decimal price)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;

            string query;

            //if (endDate == null)
                query = "insert into [dbo].[Prices] ([ParentType], [ParentId], [StartDate], [Price]) values (" + parentType.ToString() + ", " + parentId.ToString() + ", '" + startDate.ToShortDateString() + "', " + price + ");";
            //else
            //    query = "insert into [dbo].[Prices] ([Id], [ParentType], [ParentId], [StartDate], [EndDate], [Price]) values (" + id.ToString() + ", " + parentType.ToString() + ", " + variationId.ToString() + ", '" + startDate.ToShortDateString() + "', '" + DateTime.Parse(endDate.ToString()).ToShortDateString() + "', " + price + ");";

            command.CommandText = query;
            command.ExecuteNonQuery();
        }

        private static void InsertItemIngredients(int itemId, int parentType, XmlNode node)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;

            foreach (XmlNode childNode in node.ChildNodes)
            {
                int ingredientId = Int32.Parse(childNode.Attributes.GetNamedItem("Id").Value);
                int portions = 1;

                if (childNode.Attributes.GetNamedItem("Portions") != null)
                    portions = Int32.Parse(childNode.Attributes.GetNamedItem("Portions").Value);

                string query = "insert into [dbo].[ItemIngredients] ([ItemId], [ItemType], [IngredientId], [Portions]) values (" + itemId.ToString() + ", " + parentType.ToString() + ", " + ingredientId.ToString() + ", " + portions.ToString() + ");";

                command.CommandText = query;
                command.ExecuteNonQuery();
            }
        }
    }
}
