using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using wcli.Models;
using wcli.Services;

namespace wcli.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty] private string? _rpcServerPrefix;
    [ObservableProperty] private string? _walletName;

    public MainWindowViewModel()
    {
        RpcServerPrefix = "http://127.0.0.1:37128";
        WalletName = "Wallet 1";
    }

    private async Task<T?> SendRpcMethod<T>(RpcMethod requestBody, string rpcServerUri, JsonTypeInfo<T> jsonTypeInfo)
    {
        var requestBodyJson = JsonSerializer.Serialize(requestBody, RpcJsonContext.Default.RpcMethod);
        var cts = new CancellationTokenSource();
        var rpcService = new RpcService();
        var responseBodyJson = await rpcService.GetResponseDataAsync(rpcServerUri, requestBodyJson, true, cts.Token);
        return responseBodyJson is not null ? JsonSerializer.Deserialize(responseBodyJson, jsonTypeInfo) : default;
    }

    [RelayCommand]
    private async Task GetStatus()
    {
        // {"jsonrpc":"2.0","id":"1","method":"getstatus"}
        var requestBody = new RpcMethod()
        {
            Method = "getstatus"
        };
        var rpcServerUri = $"{RpcServerPrefix}";
        var rpcResult = await SendRpcMethod<RpcGetStatusResult>(requestBody, rpcServerUri, RpcJsonContext.Default.RpcGetStatusResult);
        if (rpcResult?.Result != null)
        {
            // TODO:
        }
    }

    [RelayCommand]
    private async Task GetWalletInfo()
    {
        // {"jsonrpc":"2.0","id":"1","method":"getwalletinfo"}
        var requestBody = new RpcMethod()
        {
            Method = "getwalletinfo"
        };
        var rpcServerUri = $"{RpcServerPrefix}/{WalletName}";
        var rpcResult = await SendRpcMethod<RpcGetWalletInfoResult>(requestBody, rpcServerUri, RpcJsonContext.Default.RpcGetWalletInfoResult);
        if (rpcResult?.Result != null)
        {
            // TODO:
        }
    }
}
