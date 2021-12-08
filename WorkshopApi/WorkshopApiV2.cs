using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace WorkshopApi
{
    public class WorkshopApiV2
    {
        private const string API_PREFIX = "/api/v2/";
        private HttpClient httpClient;
        private string apiKey;
        private AccessToken accessToken;

        public WorkshopApiV2(string baseUrl, string apiKey)
        {
            
            this.apiKey = apiKey;

            this.httpClient = SetupHttpClient(baseUrl, apiKey);
        }

        private HttpClient SetupHttpClient(string url, string key)
        {
            HttpClient c = new HttpClient();
            c.Timeout = new TimeSpan(0, 0, 0, 5);
            c.BaseAddress = new Uri(url.TrimEnd('/') + API_PREFIX);
            return c;
        }

        public void Authenticate()
        {
            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Get, "authenticate");
            req.Headers.Add("X-Api-Token", this.apiKey);
            HttpResponseMessage resp = this.httpClient.Send(req);
            if (resp.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Expected status code 200 OK, got {resp.StatusCode}");
            }

            string body = resp.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            this.accessToken = JsonSerializer.Deserialize<AccessToken>(body);
        }

        private void AddAuthHeader(HttpRequestMessage req)
        {
            if (this.accessToken.ValidUntil > DateTime.Now.AddMinutes(-10))
            {
                this.Authenticate();
            }
            string token = $"nobody:{this.accessToken.Token}".ToBase64();
            req.Headers.Authorization = new AuthenticationHeaderValue("Basic", token);
        }

        public List<Product> GetAllProducts()
        {
            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Get, "product/getall");
            AddAuthHeader(req);
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
            AddAuthHeader(req);
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
            AddAuthHeader(req);
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
            AddAuthHeader(req);
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
            AddAuthHeader(req);
            HttpResponseMessage resp = this.httpClient.Send(req);
            if (resp.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Expected status code 200, got {resp.StatusCode}");
            }
        }

        public List<Review> GetAllReviews(int productId)
        {
            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Get, $"product/{productId}/review/getall");
            AddAuthHeader(req);
            HttpResponseMessage resp = this.httpClient.Send(req);
            if (resp.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Expected status code 200, got {resp.StatusCode}");
            }

            string body = resp.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            return JsonSerializer.Deserialize<List<Review>>(body);
        }

        public Review GetReview(int productId, int reviewId)
        {
            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Get, $"product/{productId}/review/{reviewId}/get");
            AddAuthHeader(req);
            HttpResponseMessage resp = this.httpClient.Send(req);
            if (resp.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Expected status code 200, got {resp.StatusCode}");
            }

            string body = resp.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            return JsonSerializer.Deserialize<Review>(body);
        }

        public void AddReview(int productId, Review r)
        {
            r.ProductId = productId;
            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, $"product/{productId}/review/add");
            AddAuthHeader(req);
            req.Content = new StringContent(JsonSerializer.Serialize(r));
            HttpResponseMessage resp = this.httpClient.Send(req);
            if (resp.StatusCode != HttpStatusCode.OK) // eigentlich 202 Created
            {
                throw new Exception($"Expected status code 200, got {resp.StatusCode}");
            }
        }

        public void EditReview(int productId, Review r)
        {
            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Get, $"product/{productId}/review/{r.Id}/edit");
            AddAuthHeader(req);
            req.Content = new StringContent(JsonSerializer.Serialize(r));
            HttpResponseMessage resp = this.httpClient.Send(req);
            if (resp.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Expected status code 200, got {resp.StatusCode}");
            }
        }
    }
}
