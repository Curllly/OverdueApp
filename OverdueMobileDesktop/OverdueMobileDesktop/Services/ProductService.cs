using OverdueMobileDesktop.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OverdueMobileDesktop.Services
{
    public class ProductService
    {
        public static string GetUrl()
        {
            switch (Device.RuntimePlatform)
            {
                case Device.UWP:
                    return "http://localhost:5068/Products";
                case Device.Android:
                    return "http://10.0.2.2:5068/Products";
                default:
                    return "";
            }
        }
        string Url = GetUrl();
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };
        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }
        public async Task<IEnumerable<Product>> Get()
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync(Url);
            return JsonSerializer.Deserialize<IEnumerable<Product>>(result, options);
        }
        public async Task<Product> Add(Product product)
        {
            HttpClient client = GetClient();
            var response = await client.PostAsync
                (
                Url, 
                new StringContent(JsonSerializer.Serialize(product), 
                    Encoding.UTF8, 
                    "application/json")
                );
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return null;

            return JsonSerializer.Deserialize<Product>(
                await response.Content.ReadAsStringAsync(), options);
        }
        public async Task<Product> Update(Product product)
        {
            HttpClient client = GetClient();
            var response = await client.PutAsync
                (
                Url,
                new StringContent(JsonSerializer.Serialize(product),
                    Encoding.UTF8,
                    "application/json")
                );
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return null;
            return JsonSerializer.Deserialize<Product>(
                await response.Content.ReadAsStringAsync(), options);
        }
        public async Task<Product> Delete(int id)
        {
            HttpClient client = GetClient();
            var response = await client.DeleteAsync(Url + id);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return null;

            return JsonSerializer.Deserialize<Product>(
                await response.Content.ReadAsStringAsync(), options);
        }
    }
}
