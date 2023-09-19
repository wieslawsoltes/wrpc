using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace wcli.Services;

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

        // Deserialize the response
        var responseBody = await response.Content.ReadAsStringAsync(token);
        if (debug)
        {
            Console.WriteLine($"Status code: {response.StatusCode}");
            Console.WriteLine($"Response body:{Environment.NewLine}{responseBody}");
        }

        switch (response.StatusCode)
        {
            case HttpStatusCode.Unauthorized:
            case HttpStatusCode.TooManyRequests:
            case HttpStatusCode.InternalServerError:
            case HttpStatusCode.NotFound:
            case HttpStatusCode.BadRequest:
            {
                return responseBody;
            }
            case HttpStatusCode.OK:
            {
                return responseBody;
            }
            default:
            {
                return "null";
            }
        }
    }
}
