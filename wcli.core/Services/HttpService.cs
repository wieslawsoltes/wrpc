using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WasabiCli.Services;

public class HttpService : IHttpService
{
    private static readonly HttpClient s_httpClient;

    static HttpService()
    {
        s_httpClient = new HttpClient();
    }

    public async Task<string?> GetResponseDataAsync(string requestUri, string requestBodyJson, CancellationToken token)
    {
        var content = new StringContent(requestBodyJson, Encoding.UTF8, "application/json");
#if DEBUG
            Console.WriteLine($"RequestBody:{Environment.NewLine}{requestBodyJson}");
#endif
        var response = await s_httpClient.PostAsync(requestUri, content, token);
        var responseBody = await response.Content.ReadAsStringAsync(token);
#if DEBUG
            Console.WriteLine($"Status code: {response.StatusCode}");
            Console.WriteLine($"Response body:{Environment.NewLine}{responseBody}");
#endif
        return response.StatusCode switch
        {
            HttpStatusCode.OK => responseBody,
            _ => null
        };
    }
}
