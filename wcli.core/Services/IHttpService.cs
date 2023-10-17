using System.Threading;
using System.Threading.Tasks;

namespace WasabiCli.Services;

public interface IHttpService
{
    Task<string?> GetResponseDataAsync(string requestUri, string requestBodyJson, CancellationToken token);
}
