using System.Threading;
using System.Threading.Tasks;

namespace WasabiCli.Models.Services;

public interface IHttpService
{
    Task<string?> GetResponseDataAsync(string requestUri, string requestBodyJson, CancellationToken token);
}
