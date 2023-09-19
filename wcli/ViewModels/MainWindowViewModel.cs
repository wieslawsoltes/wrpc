﻿using System.Text.Json;
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

    [RelayCommand]
    private async Task GetStatus()
    {
        var rpcServerUri = $"{RpcServerPrefix}";

        // {"jsonrpc":"2.0","id":"1","method":"getstatus"}
        var requestBody = new RpcMethod()
        {
            Method = "getstatus"
        };

        var requestBodyJson = JsonSerializer.Serialize(requestBody, RpcJsonContext.Default.RpcMethod);
        var cts = new CancellationTokenSource();
        var rpcService = new RpcService();

        var responseBodyJson = await rpcService.GetResponseDataAsync(rpcServerUri, requestBodyJson, true, cts.Token);
        if (responseBodyJson is not null)
        {
            var rpcResult = JsonSerializer.Deserialize(responseBodyJson, RpcJsonContext.Default.RpcGetStatusResult);
            if (rpcResult?.Result != null)
            {
                // ...
            }
        }
    }

    [RelayCommand]
    private async Task GetWalletInfo()
    {
        var rpcServerUri = $"{RpcServerPrefix}/{WalletName}";

        // {"jsonrpc":"2.0","id":"1","method":"getwalletinfo"}
        var requestBody = new RpcMethod()
        {
            Method = "getwalletinfo"
        };

        var requestBodyJson = JsonSerializer.Serialize(requestBody, RpcJsonContext.Default.RpcMethod);
        var cts = new CancellationTokenSource();
        var rpcService = new RpcService();

        var responseBodyJson = await rpcService.GetResponseDataAsync(rpcServerUri, requestBodyJson, true, cts.Token);
        if (responseBodyJson is not null)
        {
            var rpcResult = JsonSerializer.Deserialize(responseBodyJson, RpcJsonContext.Default.RpcGetWalletInfoResult);
            if (rpcResult?.Result != null)
            {
                // ...
            }
        }
    }
}
