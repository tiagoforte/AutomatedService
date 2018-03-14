using Newtonsoft.Json;
using SV.Batch.Commands;
using SV.Batch.ValueObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SV.Batch.Util
{
    public static class RestClient
    {
        public static async Task<T> GetAsyncGeneric<T>(this HttpClient client, string url) where T : class
        {
            var response = await client.GetAsync(url);
            var content = string.Empty;
            try
            {
                await response.EnsureSuccessStatusCodeAsync();
                content = await response.Content.ReadAsStringAsync();
            }
            catch (SimpleHttpResponseException ex)
            {
                content = Util.Serialize.SerializeObject<HttpResponseMessage>(response);
            }
            return JsonConvert.DeserializeObject<T>(content);
        }

        public static async Task<T> PostAsyncGeneric<T>(this HttpClient client, string url, HttpContent httpContent) where T : class
        {
            var response = await client.PostAsync(url, httpContent);
            var content = string.Empty;
            try
            {
                await response.EnsureSuccessStatusCodeAsync();
                content = await response.Content.ReadAsStringAsync();
            }
            catch (SimpleHttpResponseException ex)
            {
                content = Util.Serialize.SerializeObject<HttpResponseMessage>(response);
            }
            return JsonConvert.DeserializeObject<T>(content);
        }

        public static async Task<T> PutAsyncGeneric<T>(this HttpClient client, string url, HttpContent httpContent) where T : class
        {
            var response = await client.PutAsync(url, httpContent);
            var content = string.Empty;
            try
            {
                await response.EnsureSuccessStatusCodeAsync();
                content = await response.Content.ReadAsStringAsync();
            }
            catch (SimpleHttpResponseException ex)
            {
                content = Util.Serialize.SerializeObject<HttpResponseMessage>(response);
            }
            return JsonConvert.DeserializeObject<T>(content);
        }

        public static HttpClient GetClient(string baseAddress, MediaType mediaType, User user)
        {
            HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromMinutes(10);

            if (!string.IsNullOrEmpty(baseAddress))
                client.BaseAddress = new Uri(baseAddress);

            if (user?.Token != null)
                client.DefaultRequestHeaders.Add("Authorization", String.Concat("bearer ", user.Token));

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType.Value));
            return client;
        }
    }

    public static class HttpResponseMessageExtensions
    {
        public static async Task EnsureSuccessStatusCodeAsync(this HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return;
            }
            var content = await response.Content.ReadAsStringAsync();
            if (response.Content != null)
                response.Content.Dispose();

            throw new SimpleHttpResponseException(response.StatusCode, content);
        }
    }

    public class SimpleHttpResponseException : Exception
    {
        public HttpStatusCode StatusCode { get; private set; }

        public SimpleHttpResponseException(HttpStatusCode statusCode, string content) : base(content)
        {
            StatusCode = statusCode;
        }
    }


}
