using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Epias.Transparency.Api;

public class HttpClientManager : IHttpClientManager
{
    private readonly IHttpClientFactory httpClientFactory;

    public HttpClientManager(IHttpClientFactory httpClientFactory)
    {
        this.httpClientFactory = httpClientFactory;
    }

    public HttpClient CreateHttpClient(string name)
    {
        return httpClientFactory.CreateClient(name);
    }

    public async Task<string> GetResponseWithRetries(string requestUrl, string name = "")
    {
        int tryCount = 0;

        while (true)
        {
            try
            {
                return await CreateHttpClient(name).GetStringAsync(requestUrl);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Http client error: " + ex.Message + " RequestUrl: " + requestUrl);

                if (ex.Message.Contains("403") || ex.Message.Contains("404") || ex.Message.Contains("401"))
                {
                    throw;
                }
                else
                {
                    tryCount++;
                    if (tryCount < 10)
                    {
                        if (ex.Message.Contains("429") || ex.Message.Contains("The request was canceled due to the configured HttpClient.Timeout"))
                        {
                            await Task.Delay(TimeSpan.FromSeconds(5 * tryCount));
                        }
                        else
                        {
                            await Task.Delay(tryCount * 2000);
                        }
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }
    }

    public async Task<string> PostResponseWithRetries(string requestUrl, string json, string name = "", bool checkSuccessStatusCode = true)
    {
        var httpClient = CreateHttpClient(name);
        return await PostResponseWithRetries(httpClient, requestUrl, new StringContent(json, Encoding.UTF8, "application/json"), checkSuccessStatusCode: checkSuccessStatusCode);
    }



    public async Task<string> PostResponseWithRetries(string requestUrl, HttpContent httpContent = null, string name = "", bool checkSuccessStatusCode = true)
    {
        var httpClient = CreateHttpClient(name);
        return await PostResponseWithRetries(httpClient, requestUrl, httpContent, checkSuccessStatusCode: checkSuccessStatusCode);
    }

    public async Task<string> PostResponseWithRetries(HttpClient httpClient, string requestUrl, string json, bool checkSuccessStatusCode = true)
    {
        return await PostResponseWithRetries(httpClient, requestUrl, new StringContent(json, Encoding.UTF8, "application/json"), checkSuccessStatusCode: checkSuccessStatusCode);
    }

    public async Task<string> PostResponseWithRetries(HttpClient httpClient, string requestUrl, HttpContent httpContent = null, bool checkSuccessStatusCode = true)
    {
        int tryCount = 0;

        if (httpContent == null)
        {
            httpContent = new StringContent(string.Empty);
        }

        while (true)
        {
            try
            {
                var postResult = await httpClient.PostAsync(requestUrl, httpContent);

                if (checkSuccessStatusCode)
                {
                    postResult.EnsureSuccessStatusCode();
                }

                return await postResult.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Http client error: " + ex.Message + " RequestUrl: " + requestUrl);

                if (ex.Message.Contains("403") || ex.Message.Contains("404") || ex.Message.Contains("401"))
                {
                    throw;
                }
                else
                {
                    tryCount++;

                    if (tryCount < 10)
                    {
                        if (ex.Message.Contains("429") || ex.Message.Contains("The request was canceled due to the configured HttpClient.Timeout"))
                        {
                            await Task.Delay(TimeSpan.FromSeconds(5 * tryCount));
                        }
                        else
                        {
                            await Task.Delay(tryCount * 2000);
                        }
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }
    }
    public static string GetStatusCode(HttpStatusCode httpStatusCode)
    {
        return ((int)httpStatusCode).ToString();
    }
}

