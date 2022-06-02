using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Epias.Transparency.Api;

public interface IHttpClientManager
{
    public HttpClient CreateHttpClient(string name);
    public Task<string> GetResponseWithRetries(string requestUrl, string name = "");

    public Task<string> PostResponseWithRetries(string requestUrl, string json, string name = "",
        bool checkSuccessStatusCode = true);

    public Task<string> PostResponseWithRetries(string requestUrl, HttpContent httpContent = null, string name = "",
        bool checkSuccessStatusCode = true);

    public Task<string> PostResponseWithRetries(HttpClient httpClient, string requestUrl, string json,
        bool checkSuccessStatusCode = true);

    public Task<string> PostResponseWithRetries(HttpClient httpClient, string requestUrl,
        HttpContent httpContent = null, bool checkSuccessStatusCode = true);
}

