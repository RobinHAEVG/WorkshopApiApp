using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;

namespace WorkshopApi
{
    public class WorkshopApiV1
    {
        private HttpClient httpClient;
        private const string API_PREFIX = "/api/v1/";

        public WorkshopApiV1(string baseUrl, string apiKey)
        {
            this.httpClient = SetupHttpClient(baseUrl, apiKey);
        }

        private HttpClient SetupHttpClient(string url, string key)
        {
            HttpClient c = new();
            c.Timeout = new TimeSpan(0, 0, 0, 10);
            c.BaseAddress = new Uri(url.TrimEnd('/') + API_PREFIX);
            c.DefaultRequestHeaders.Add("X-Api-Key", key);
            return c;
        }

        public List<Product> GetAllProducts()
        {
            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Get, "product/getall");
            HttpResponseMessage resp = this.httpClient.Send(req);
            if (resp.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Expected status code 200, got {resp.StatusCode}");
            }

            string body = resp.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            return JsonSerializer.Deserialize<List<Product>>(body);

        }

        public Product GetProduct(int productId)
        {
            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Get, $"product/{productId}/get");
            HttpResponseMessage resp = this.httpClient.Send(req);
            if (resp.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Expected status code 200, got {resp.StatusCode}");
            }

            string body = resp.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            return JsonSerializer.Deserialize<Product>(body);
        }

        public void AddProduct(Product p)
        {
            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, $"product/add");
            req.Content = new StringContent(JsonSerializer.Serialize(p));
            HttpResponseMessage resp = this.httpClient.Send(req);
            if (resp.StatusCode != HttpStatusCode.OK) // eig. 202 Created
            {
                throw new Exception($"Expected status code 200, got {resp.StatusCode}");
            }

            string body = resp.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            Product temp = JsonSerializer.Deserialize<Product>(body);
            p.Id = temp.Id;
        }

        public void EditProduct(Product p)
        {
            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Put, $"product/edit");
            req.Content = new StringContent(JsonSerializer.Serialize(p));
            HttpResponseMessage resp = this.httpClient.Send(req);
            if (resp.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Expected status code 200, got {resp.StatusCode}");
            }
        }

        public void RemoveProduct(int id)
        {
            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Delete, $"product/{id}/remove");
            HttpResponseMessage resp = this.httpClient.Send(req);
            if (resp.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Expected status code 200, got {resp.StatusCode}");
            }
        }
    }
}
