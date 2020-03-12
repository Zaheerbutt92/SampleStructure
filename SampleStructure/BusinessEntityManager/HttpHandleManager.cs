using Constant;
using Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntityManager
{
    public class HttpHandleManager : IHttpHandler
    {
        #region Public Methods

        public async Task<IEnumerable<T>> GetAsyncWithIEnumerable<T>(Uri url)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(HttpContentType.ContentTypeJson));
                var message = await client.GetAsync(url);
                var result = await message.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<T>>(result);
            }
        }

        public async Task<T> GetAsync<T>(Uri url)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(HttpContentType.ContentTypeJson));
                var message = await client.GetAsync(url);
                var result = await message.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(result);
            }
        }

        public async Task<HttpResponseMessage> PostAsync(Uri url, IEnumerable<KeyValuePair<string, string>> data)
        {
            using (var client = GetHttpClient())
            {
                HttpContent content = new FormUrlEncodedContent(data);
                var message = await client.PostAsync(url, content);
                var result = await message.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<HttpResponseMessage>(result);
            }
        }

        public async Task<T> PostAsync<T>(string url, object data)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(HttpContentType.ContentTypeJson));
                HttpResponseMessage message;
                if (data != null)
                {
                    var json = JsonConvert.SerializeObject(data);
                    var httpContent = new StringContent(json, Encoding.Unicode, HttpContentType.ContentTypeJson);
                    message = await client.PostAsync(url, httpContent);
                }
                else
                {
                    message = await client.PostAsync(url, null);
                }
                var result = await message.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(result);
            }
        }

        public async Task<T> PutAsync<T>(string url, object data = null)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(HttpContentType.ContentTypeJson));
                HttpResponseMessage message;
                if (data != null)
                {
                    var json = JsonConvert.SerializeObject(data);
                    var httpContent = new StringContent(json, Encoding.Unicode, HttpContentType.ContentTypeJson);
                    message = await client.PutAsync(url, httpContent);
                }
                else
                {
                    message = await client.PutAsync(url, null);
                }
                var result = await message.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(result);
            }
        }

        public async Task<T> DeleteAsync<T>(string url)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(HttpContentType.ContentTypeJson));
                var message = await client.DeleteAsync(url);
                var result = await message.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(result);
            }
        }

        #endregion

        #region Private Methods

        private HttpClient GetHttpClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(HttpContentType.ContentTypeJson));
            return client;
        }

        #endregion

    }
}
