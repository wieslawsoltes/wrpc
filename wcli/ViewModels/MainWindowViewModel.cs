using System;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiCli.Models.RpcJson;
using WasabiCli.Models.WalletWasabi;
using WasabiCli.Services;

namespace WasabiCli.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty] private string? _rpcServerPrefix;
    [ObservableProperty] private string? _walletName;
    [ObservableProperty] private string? _walletPassword;
    [ObservableProperty] private object? _currentDialog;
    [ObservableProperty] private ObservableCollection<RpcMethodViewModel>? _rpcMethods;

    public MainWindowViewModel()
    {
        RpcServerPrefix = "http://127.0.0.1:37128";
        WalletName = "Wallet 1";
        WalletPassword = "";
        RpcMethods = new ObservableCollection<RpcMethodViewModel>()
        {
            new ("GetStatus", GetStatusCommand),
            new ("CreateWallet", CreateWalletCommand),
            new ("ListCoins", ListCoinsCommand),
            new ("ListUnspentCoins", ListUnspentCoinsCommand),
            new ("GetWalletInfo", GetWalletInfoCommand),
            new ("GetNewAddress", GetNewAddressCommand),
            new ("Send", SendCommand),
            new ("Build", BuildCommand),
            new ("Broadcast", BroadcastCommand),
            new ("GetHistory", GetHistoryCommand),
            new ("ListKeys", ListKeysCommand),
            new ("StartCoinJoin", StartCoinJoinCommand),
            new ("StopCoinJoin", StopCoinJoinCommand),
            new ("Stop", StopCommand)
        };
    }

    private async Task<Rpc?> SendRpcMethod<T>(
        RpcMethod requestBody, 
        string rpcServerUri, 
        JsonTypeInfo<T> jsonTypeInfo) where T: Rpc
    {
        var requestBodyJson = JsonSerializer.Serialize(requestBody, RpcJsonContext.Default.RpcMethod);
        var cts = new CancellationTokenSource();
        var rpcService = new RpcService();
        var responseBodyJson = await rpcService.GetResponseDataAsync(rpcServerUri, requestBodyJson, true, cts.Token);
        if (responseBodyJson is not null)
        {
            try
            {
                var okResult = JsonSerializer.Deserialize(responseBodyJson, jsonTypeInfo);
                if (okResult is not null)
                {
                    return okResult;
                }
            }
            catch (Exception)
            {
                // ignored
            }

            try
            {
                return JsonSerializer.Deserialize(responseBodyJson, RpcJsonContext.Default.RpcErrorResult);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        return default;
    }

    [RelayCommand]
    private void SetCurrentDialog(object? currentDialog)
    {
        CurrentDialog = currentDialog;
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
        var rpcResult = await SendRpcMethod(requestBody, rpcServerUri, RpcJsonContext.Default.RpcGetStatusResult);
        if (rpcResult is RpcGetStatusResult { Result: not null } rpcGetStatusResult)
        {
            // TODO:
            CurrentDialog = rpcGetStatusResult.Result;
        }
        else if (rpcResult is RpcErrorResult { Error: not null } rpcErrorResult)
        {
            // TODO:
            CurrentDialog = rpcErrorResult.Error;
        }
    }

    [RelayCommand]
    private async Task CreateWallet()
    {
        // {"jsonrpc":"2.0","id":"1","method":"createwallet","params":["WalletName", "UserPassword"]}'
        var requestBody = new RpcMethod()
        {
            Method = "createwallet",
            Params = new []
            {
                WalletName,
                WalletPassword
            }
        };
        var rpcServerUri = $"{RpcServerPrefix}";
        var rpcResult = await SendRpcMethod(requestBody, rpcServerUri, RpcJsonContext.Default.RpcCreateWalletResult);
        if (rpcResult is RpcCreateWalletResult { Result: not null } rpcCreateWalletResult)
        {
            // TODO:
            CurrentDialog = new CreateWalletInfo { Mnemonic = rpcCreateWalletResult.Result };
        }
        else if (rpcResult is RpcErrorResult { Error: not null } rpcErrorResult)
        {
            // TODO:
            CurrentDialog = rpcErrorResult.Error;
        }
    }

    [RelayCommand]
    private async Task ListCoins()
    {
        throw new NotImplementedException();
    }

    [RelayCommand]
    private async Task ListUnspentCoins()
    {
        throw new NotImplementedException();
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
        var rpcResult = await SendRpcMethod(requestBody, rpcServerUri, RpcJsonContext.Default.RpcGetWalletInfoResult);
        if (rpcResult is RpcGetWalletInfoResult { Result: not null } rpcGetWalletInfoResult)
        {
            // TODO:
            CurrentDialog = rpcGetWalletInfoResult.Result;
        }
        else if (rpcResult is RpcErrorResult { Error: not null } rpcErrorResult)
        {
            // TODO:
            CurrentDialog = rpcErrorResult.Error;
        }
    }

    [RelayCommand]
    private async Task GetNewAddress()
    {
        throw new NotImplementedException();
    }

    [RelayCommand]
    private async Task Send()
    {
        throw new NotImplementedException();
    }

    [RelayCommand]
    private async Task Build()
    {
        throw new NotImplementedException();
    }

    [RelayCommand]
    private async Task Broadcast()
    {
        throw new NotImplementedException();
    }

    [RelayCommand]
    private async Task GetHistory()
    {
        throw new NotImplementedException();
    }

    [RelayCommand]
    private async Task ListKeys()
    {
        throw new NotImplementedException();
    }

    [RelayCommand]
    private async Task StartCoinJoin()
    {
        throw new NotImplementedException();
    }

    [RelayCommand]
    private async Task StopCoinJoin()
    {
        throw new NotImplementedException();
    }

    [RelayCommand]
    private async Task Stop()
    {
        throw new NotImplementedException();
    }
}
