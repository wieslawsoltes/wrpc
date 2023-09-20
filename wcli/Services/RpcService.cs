using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WasabiCli.Services;

public class RpcService
{
    private static readonly HttpClient s_httpClient;

    static RpcService()
    {
        s_httpClient = new();
    }

    public async Task<string?> GetResponseDataAsync(string requestUri, string requestBodyJson, bool debug, CancellationToken token)
    {
        var content = new StringContent(requestBodyJson, Encoding.UTF8, "application/json");
        if (debug)
        {
            Console.WriteLine($"RequestBody:{Environment.NewLine}{requestBodyJson}");
        }

        var response = await s_httpClient.PostAsync(requestUri, content, token);
        var responseBody = await response.Content.ReadAsStringAsync(token);
        if (debug)
        {
            Console.WriteLine($"Status code: {response.StatusCode}");
            Console.WriteLine($"Response body:{Environment.NewLine}{responseBody}");
        }

        return response.StatusCode switch
        {
            HttpStatusCode.OK => responseBody,
            _ => null
        };
    }
}
