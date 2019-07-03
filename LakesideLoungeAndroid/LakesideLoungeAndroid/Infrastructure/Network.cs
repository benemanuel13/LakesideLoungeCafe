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

using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using BensJson;

using LakesideLoungeAndroid.Application;
using LakesideLoungeAndroid.Domain;

namespace LakesideLoungeAndroid.Infrastructure
{
    public static class Network
    {
        public static Context Context { get; set; }

        public static async void SendOrders(TextView uploadProgress)
        {
            ((Activity)Context).RunOnUiThread(() => { uploadProgress.Text = "Preparing Orders For Upload..."; });
            await Task.Run(() => { });

            List<Order> orders = Database.GetUnuploadedOrders(Context, uploadProgress);

            if (orders == null)
            {
                ((Activity)Context).RunOnUiThread(() => { uploadProgress.Text = "No Orders To Upload."; });
                await Task.Run(() => { });

                return;
            }

            ((Activity)Context).RunOnUiThread(() => { uploadProgress.Text = "Serializing Orders..."; });
            await Task.Run(() => { });

            string json = new JsonSerializer().Serialize(orders);

            ((Activity)Context).RunOnUiThread(() => { uploadProgress.Text = "Uploading Orders..."; });
            await Task.Run(() => { });

            HttpClient client = new HttpClient();

            StringContent content = new StringContent(json, UTF32Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;
            try
            {
                response = await client.PostAsync("http://192.168.1.66/api/LakesideLounge/PostOrders", content);
            }
            catch
            {
                ((Activity)Context).RunOnUiThread(() => { uploadProgress.Text = "Failed to Upload Orders."; });
                await Task.Run(() => { });
                return;
            }

            HttpStatusCode statusCode = response.StatusCode;

            if (statusCode == HttpStatusCode.OK)
            {
                ((Activity)Context).RunOnUiThread(() => { uploadProgress.Text = "Marking Orders as Uploaded..."; });
                await Task.Run(() => { });

                foreach (Order order in orders)
                    Database.OrderUploaded(order.Id, true);
            }

            string responseText = await response.Content.ReadAsStringAsync();

            ((Activity)Context).RunOnUiThread(() => { uploadProgress.Text = "Orders Uploaded."; });
            await Task.Run(() => { });
        }

        public static async void GetUpdates(TextView downloadProgress)
        {
            ((Activity)Context).RunOnUiThread(() => { downloadProgress.Text = "Downloading Updates..."; });

            HttpClient client = new HttpClient();
            HttpResponseMessage response = null;

            try
            {
                response = await client.GetAsync("http://192.168.1.66/api/LakesideLounge/GetUpdates");
            }
            catch (Exception ex)
            {
                ((Activity)Context).RunOnUiThread(() => { downloadProgress.Text = "Failed to Download Updates: " + ex.Message; });
                return;
            }

            string returnedContent = await response.Content.ReadAsStringAsync();

            ((Activity)Context).RunOnUiThread(() => { downloadProgress.Text = "Deserializing Updates..."; });
            
            List<Update> updates = new JsonDeserializer<List<Update>>().Deserialize(returnedContent);

            if (updates == null || updates.Count == 0)
            {
                ((Activity)Context).RunOnUiThread(() => { downloadProgress.Text = "No Updates to Process."; });
                return;
            }

            ((Activity)Context).RunOnUiThread(() => { downloadProgress.Text = "Processing Updates..."; });

            ProcessUpdates(updates);

            ((Activity)Context).RunOnUiThread(() => { downloadProgress.Text = "Updates Downloaded and Processed."; });
        }

        private static void ProcessUpdates(List<Update> updates)
        {
            foreach(Update update in updates)
            {
                string updateText = update.UpdateText;

                if (updateText.StartsWith("ADD_VARIATION,"))
                    AddVariation(updateText);
                else if (updateText.StartsWith("UPDATE_VARIATION,"))
                    UpdateVariation(updateText);
                else if (updateText.StartsWith("SWAP_VARIATION_POSITIONS,"))
                    SwapVariationPositions(updateText);
                else if (updateText.StartsWith("REMOVE_VARIATION,"))
                    RemoveVariation(updateText);
                else if (updateText.StartsWith("REINSTATE_VARIATION,"))
                    ReinstateVariation(updateText);
                else if (updateText.StartsWith("DELETE_VARIATION,"))
                { }
                if (updateText.StartsWith("ADD_COMPONENT,"))
                    AddComponent(updateText);
                else if (updateText.StartsWith("UPDATE_COMPONENT,"))
                    UpdateComponent(updateText);
                else if (updateText.StartsWith("REMOVE_COMPONENT,"))
                    RemoveComponent(updateText);
                else if (updateText.StartsWith("DELETE_COMPONENT,"))
                { }
                else if (updateText.StartsWith("ADD_VARIATION_COMPONENT,"))
                    AddVariationComponent(updateText);
                else if (updateText.StartsWith("UPDATE_VARIATION_COMPONENT,"))
                    UpdateVariationComponent(updateText);
                else if (updateText.StartsWith("SWAP_VARIATION_COMPONENT_POSITIONS,"))
                    SwapVariationComponentPositions(updateText);
                else if (updateText.StartsWith("UPDATE_VARIATION_COMPONENT_DEFAULT,"))
                    UpdateVariationComponentDefault(updateText);
                else if (updateText.StartsWith("REMOVE_VARIATION_COMPONENT,"))
                    RemoveVariationComponent(updateText);
                else if (updateText.StartsWith("ADD_COMPONENT_COMPONENT,"))
                    AddComponentComponent(updateText);
                else if (updateText.StartsWith("UPDATE_COMPONENT_COMPONENT,"))
                    UpdateComponentComponent(updateText);
                else if (updateText.StartsWith("SWAP_COMPONENT_COMPONENT_POSITIONS,"))
                    SwapComponentComponentPositions(updateText);
                else if (updateText.StartsWith("UPDATE_COMPONENT_COMPONENT_DEFAULT,"))
                    UpdateComponentComponentDefault(updateText);
                else if (updateText.StartsWith("REMOVE_COMPONENT_COMPONENT,"))
                    RemoveComponentComponent(updateText);
                else if (updateText.StartsWith("UPDATE_PRICE,"))
                    AddPrice(updateText);

                Database.SaveUpdate(update);
            }
        }

        private static void AddPrice(string update)
        {
            int firstComma = update.IndexOf(",");
            int secondComma = update.IndexOf(",", firstComma + 1);
            int id = Int32.Parse(update.Substring(firstComma + 1, secondComma - firstComma - 1));

            int thirdComma = update.IndexOf(",", secondComma + 1);
            int parentType = Int32.Parse(update.Substring(secondComma + 1, thirdComma - secondComma - 1));

            int fourthComma = update.IndexOf(",", thirdComma + 1);
            DateTime date = DateTime.Parse(update.Substring(thirdComma + 1, fourthComma - thirdComma - 1));

            decimal price = decimal.Parse(update.Substring(fourthComma + 1, update.Length - fourthComma - 1));

            Database.AddPrice(id, parentType, date, price);
        }

        private static void AddVariation(string update)
        {
            int firstComma = update.IndexOf(",");
            int secondComma = update.IndexOf(",", firstComma + 1);
            int id = Int32.Parse(update.Substring(firstComma + 1, secondComma - firstComma - 1));

            int thirdComma = update.IndexOf(",", secondComma + 1);
            int parentId = Int32.Parse(update.Substring(secondComma + 1, thirdComma - secondComma - 1));

            int fourthComma = update.IndexOf(",", thirdComma + 1);
            string name = update.Substring(thirdComma + 1, fourthComma - thirdComma - 1);

            int fifthComma = update.IndexOf(",", fourthComma + 1);
            string displayName = update.Substring(fourthComma + 1, fifthComma - fourthComma - 1);

            int sixthComma = update.IndexOf(",", fifthComma + 1);
            decimal price = decimal.Parse(update.Substring(fifthComma + 1, sixthComma - fifthComma - 1));

            int seventhComma = update.IndexOf(",", sixthComma + 1);
            float points = float.Parse(update.Substring(sixthComma + 1, seventhComma - sixthComma - 1));

            int eighthComma = update.IndexOf(",", seventhComma + 1);
            decimal pointPrice = decimal.Parse(update.Substring(seventhComma + 1, eighthComma - seventhComma - 1));

            int position = int.Parse(update.Substring(eighthComma + 1, update.Length - eighthComma - 1));

            Database.AddVariation(id, parentId, name, displayName, price, points, pointPrice, position);
        }

        private static void UpdateVariation(string update)
        {
            int firstComma = update.IndexOf(",");
            int secondComma = update.IndexOf(",", firstComma + 1);
            int id = Int32.Parse(update.Substring(firstComma + 1, secondComma - firstComma - 1));

            int thirdComma = update.IndexOf(",", secondComma + 1);
            int parentId = Int32.Parse(update.Substring(secondComma + 1, thirdComma - secondComma - 1));

            int fourthComma = update.IndexOf(",", thirdComma + 1);
            string name = update.Substring(thirdComma + 1, fourthComma - thirdComma - 1);

            int fifthComma = update.IndexOf(",", fourthComma + 1);
            string displayName = update.Substring(fourthComma + 1, fifthComma - fourthComma - 1);

            int sixthComma = update.IndexOf(",", fifthComma + 1);
            float points = float.Parse(update.Substring(fifthComma + 1, sixthComma - fifthComma - 1));

            decimal pointPrice = decimal.Parse(update.Substring(sixthComma + 1, update.Length - sixthComma - 1));

            Database.UpdateVariation(id, parentId, name, displayName, points, pointPrice);
        }
        
        private static void RemoveVariation(string update)
        {
            int firstComma = update.IndexOf(",");
            int variationId = Int32.Parse(update.Substring(firstComma + 1, update.Length - firstComma - 1));

            Database.AddRemoveVariation(variationId, true);
        }

        private static void ReinstateVariation(string update)
        {
            int firstComma = update.IndexOf(",");
            int variationId = Int32.Parse(update.Substring(firstComma + 1, update.Length - firstComma - 1));

            Database.AddRemoveVariation(variationId, false);
        }

        private static void AddVariationComponent(string update)
        {
            int firstComma = update.IndexOf(",");
            int secondComma = update.IndexOf(",", firstComma + 1);
            int variationId = Int32.Parse(update.Substring(firstComma + 1, secondComma - firstComma - 1));

            int thirdComma = update.IndexOf(",", secondComma + 1);
            int componentId = Int32.Parse(update.Substring(secondComma + 1, thirdComma - secondComma - 1));

            int position = Int32.Parse(update.Substring(thirdComma + 1, update.Length - thirdComma - 1));

            Database.AddVariationComponent(variationId, componentId, position);
        }

        private static void UpdateVariationComponentDefault(string update)
        {
            int firstComma = update.IndexOf(",");
            int secondComma = update.IndexOf(",", firstComma + 1);
            int variationId = Int32.Parse(update.Substring(firstComma + 1, secondComma - firstComma - 1));

            int thirdComma = update.IndexOf(",", secondComma + 1);
            int componentId = Int32.Parse(update.Substring(secondComma + 1, thirdComma - secondComma - 1));

            bool isDefault = bool.Parse(update.Substring(thirdComma + 1, update.Length - thirdComma - 1));

            Database.UpdateVariationComponentDefault(variationId, componentId, isDefault);
        }

        private static void UpdateComponentComponentDefault(string update)
        {
            int firstComma = update.IndexOf(",");
            int secondComma = update.IndexOf(",", firstComma + 1);
            int parentId = Int32.Parse(update.Substring(firstComma + 1, secondComma - firstComma - 1));

            int thirdComma = update.IndexOf(",", secondComma + 1);
            int componentId = Int32.Parse(update.Substring(secondComma + 1, thirdComma - secondComma - 1));

            bool isDefault = bool.Parse(update.Substring(thirdComma + 1, update.Length - thirdComma - 1));

            Database.UpdateComponentComponentDefault(parentId, componentId, isDefault);
        }

        private static void RemoveVariationComponent(string update)
        {
            int firstComma = update.IndexOf(",");
            int secondComma = update.IndexOf(",", firstComma + 1);
            int variationId = Int32.Parse(update.Substring(firstComma + 1, secondComma - firstComma - 1));

            int thirdComma = update.IndexOf(",", secondComma + 1);
            int componentId = Int32.Parse(update.Substring(secondComma + 1, thirdComma - secondComma - 1));

            int position = Int32.Parse(update.Substring(thirdComma + 1, update.Length - thirdComma - 1));

            Database.RemoveVariationComponent(variationId, componentId, position);
        }

        private static void UpdateVariationComponent(string update)
        {
            int firstComma = update.IndexOf(",");
            int secondComma = update.IndexOf(",", firstComma + 1);
            int parentId = Int32.Parse(update.Substring(firstComma + 1, secondComma - firstComma - 1));

            int thirdComma = update.IndexOf(",", secondComma + 1);
            int itemId = Int32.Parse(update.Substring(secondComma + 1, thirdComma - secondComma - 1));

            int fourthComma = update.IndexOf(",", thirdComma + 1);
            int portions = int.Parse(update.Substring(thirdComma + 1, fourthComma - thirdComma - 1));

            int fifthComma = update.IndexOf(",", fourthComma + 1);
            int group = int.Parse(update.Substring(fourthComma + 1, fifthComma - fourthComma - 1));

            float points = float.Parse(update.Substring(fifthComma + 1, update.Length - fifthComma - 1));
            
            Database.UpdateVariationComponent(parentId, itemId, portions, group, points);
        }

        private static void UpdateComponentComponent(string update)
        {
            int firstComma = update.IndexOf(",");
            int secondComma = update.IndexOf(",", firstComma + 1);
            int parentId = Int32.Parse(update.Substring(firstComma + 1, secondComma - firstComma - 1));

            int thirdComma = update.IndexOf(",", secondComma + 1);
            int itemId = Int32.Parse(update.Substring(secondComma + 1, thirdComma - secondComma - 1));

            int group = int.Parse(update.Substring(thirdComma + 1, update.Length - thirdComma - 1));

            Database.UpdateComponentComponent(parentId, itemId, group);
        }

        private static void AddComponentComponent(string update)
        {
            int firstComma = update.IndexOf(",");
            int secondComma = update.IndexOf(",", firstComma + 1);
            int parentId = Int32.Parse(update.Substring(firstComma + 1, secondComma - firstComma - 1));

            int thirdComma = update.IndexOf(",", secondComma + 1);
            int componentId = Int32.Parse(update.Substring(secondComma + 1, thirdComma - secondComma - 1));

            int position = Int32.Parse(update.Substring(thirdComma + 1, update.Length - thirdComma - 1));

            Database.AddComponentComponent(parentId, componentId, position);
        }

        private static void RemoveComponentComponent(string update)
        {
            int firstComma = update.IndexOf(",");
            int secondComma = update.IndexOf(",", firstComma + 1);
            int parentId = Int32.Parse(update.Substring(firstComma + 1, secondComma - firstComma - 1));

            int thirdComma = update.IndexOf(",", secondComma + 1);
            int componentId = Int32.Parse(update.Substring(secondComma + 1, thirdComma - secondComma - 1));

            int position = Int32.Parse(update.Substring(thirdComma + 1, update.Length - thirdComma - 1));

            Database.RemoveComponentComponent(parentId, componentId, position);
        }

        private static void AddComponent(string update)
        {
            int firstComma = update.IndexOf(",");
            int secondComma = update.IndexOf(",", firstComma + 1);
            int id = Int32.Parse(update.Substring(firstComma + 1, secondComma - firstComma - 1));

            int thirdComma = update.IndexOf(",", secondComma + 1);
            string name = update.Substring(secondComma + 1, thirdComma - secondComma - 1);
            
            string displayName = update.Substring(thirdComma + 1, update.Length - thirdComma - 1);

            Database.AddComponent(id, name, displayName);
        }

        private static void UpdateComponent(string update)
        {
            int firstComma = update.IndexOf(",");
            int secondComma = update.IndexOf(",", firstComma + 1);
            int id = Int32.Parse(update.Substring(firstComma + 1, secondComma - firstComma - 1));

            int thirdComma = update.IndexOf(",", secondComma + 1);
            string name = update.Substring(secondComma + 1, thirdComma - secondComma - 1);

            string displayName = update.Substring(thirdComma + 1, update.Length - thirdComma - 1);

            Database.UpdateComponent(id, name, displayName);
        }

        private static void RemoveComponent(string update)
        {
        }

        private static void SwapVariationPositions(string update)
        {
            int firstComma = update.IndexOf(",");
            int secondComma = update.IndexOf(",", firstComma + 1);
            int model1Id = Int32.Parse(update.Substring(firstComma + 1, secondComma - firstComma - 1));

            int thirdComma = update.IndexOf(",", secondComma + 1);
            int model1Position = Int32.Parse(update.Substring(secondComma + 1, thirdComma - secondComma - 1));

            int fourthComma = update.IndexOf(",", thirdComma + 1);
            int model2Id = Int32.Parse(update.Substring(thirdComma + 1, fourthComma - thirdComma - 1));

            int model2Position = Int32.Parse(update.Substring(fourthComma + 1, update.Length - fourthComma - 1));

            Database.SwapVariationPositions(model1Id, model1Position, model2Id, model2Position);
        }

        private static void SwapVariationComponentPositions(string update)
        {
            int firstComma = update.IndexOf(",");
            int secondComma = update.IndexOf(",", firstComma + 1);
            int parentId = Int32.Parse(update.Substring(firstComma + 1, secondComma - firstComma - 1));

            int thirdComma = update.IndexOf(",", secondComma + 1);
            int model1Id = Int32.Parse(update.Substring(secondComma + 1, thirdComma - secondComma - 1));

            int fourthComma = update.IndexOf(",", thirdComma + 1);
            int model1Position = Int32.Parse(update.Substring(thirdComma + 1, fourthComma - thirdComma - 1));

            int fifthComma = update.IndexOf(",", fourthComma + 1);
            int model2Id = Int32.Parse(update.Substring(fourthComma + 1, fifthComma - fourthComma - 1));

            int model2Position = Int32.Parse(update.Substring(fifthComma + 1, update.Length - fifthComma - 1));

            Database.SwapVariationComponentPositions(parentId, model1Id, model1Position, model2Id, model2Position);
        }

        private static void SwapComponentComponentPositions(string update)
        {
            int firstComma = update.IndexOf(",");
            int secondComma = update.IndexOf(",", firstComma + 1);
            int parentId = Int32.Parse(update.Substring(firstComma + 1, secondComma - firstComma - 1));

            int thirdComma = update.IndexOf(",", secondComma + 1);
            int model1Id = Int32.Parse(update.Substring(secondComma + 1, thirdComma - secondComma - 1));

            int fourthComma = update.IndexOf(",", thirdComma + 1);
            int model1Position = Int32.Parse(update.Substring(thirdComma + 1, fourthComma - thirdComma - 1));

            int fifthComma = update.IndexOf(",", fourthComma + 1);
            int model2Id = Int32.Parse(update.Substring(fourthComma + 1, fifthComma - fourthComma - 1));

            int model2Position = Int32.Parse(update.Substring(fifthComma + 1, update.Length - fifthComma - 1));

            Database.SwapComponentComponentPositions(parentId, model1Id, model1Position, model2Id, model2Position);
        }
    }
}