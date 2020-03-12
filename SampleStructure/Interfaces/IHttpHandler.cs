using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IHttpHandler
    {
        Task<IEnumerable<T>> GetAsyncWithIEnumerable<T>(Uri url);
        Task<T> GetAsync<T>(Uri url);
        Task<HttpResponseMessage> PostAsync(Uri url, IEnumerable<KeyValuePair<string, string>> data);
        Task<T> PostAsync<T>(string url, object data);
        Task<T> PutAsync<T>(string url, object data = null);
        Task<T> DeleteAsync<T>(string url);
    }
}
