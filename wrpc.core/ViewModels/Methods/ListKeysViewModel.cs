﻿using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiRpc.ViewModels.Factories;
using WasabiRpc.Models.App;
using WasabiRpc.Models.Info;
using WasabiRpc.Models.Results;
using WasabiRpc.Models.Services;

namespace WasabiRpc.ViewModels.Methods;

public partial class ListKeysViewModel : RoutableMethodViewModel
{
    [ObservableProperty] private string? _walletName;

    public ListKeysViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService, string walletName)
        : base(rpcService, navigationService)
    {
        WalletName = walletName;
    }

    [RelayCommand]
    private async Task ListKeys()
    {
        var job = CreateJob();

        if (RpcService.BatchMode)
        {
            OnBatch(job);
            return;
        }

        var result = await RpcService.Send<RpcListKeysResult>(job, NavigationService);
        if (result is RpcListKeysResult { Result: not null } rpcListKeysResult)
        {
            OnRpcSuccess(rpcListKeysResult);
        }
        else if (result is RpcErrorResult { Error: not null } rpcErrorResult)
        {
            OnRpcError(rpcErrorResult);
        }
        else if (result is Error error)
        {
            OnError(error);
        }
    }

    protected override void OnRpcSuccess(Rpc rpcResult)
    {
        if (rpcResult is RpcListKeysResult rpcListKeysResult)
        {
            NavigationService.NavigateTo(new ListKeysInfo { Keys = rpcListKeysResult.Result }.ToViewModel(RpcService, NavigationService));
        }
    }

    public override Job CreateJob()
    {
        var requestBody = new RpcMethod
        {
            Method = "listkeys"
        };

        var rpcServerUri = $"{RpcService.ServerPrefix}/{WalletName}";

        return new Job(requestBody, rpcServerUri);
    }
}