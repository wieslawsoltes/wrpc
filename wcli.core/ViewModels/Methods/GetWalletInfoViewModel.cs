﻿using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiCli.Models.App;
using WasabiCli.Models.Results;
using WasabiCli.Models.Services;
using WasabiCli.ViewModels.Factories;

namespace WasabiCli.ViewModels.Methods;

public partial class GetWalletInfoViewModel : RoutableMethodViewModel
{
    [ObservableProperty] private string? _walletName;

    public GetWalletInfoViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService, string walletName)
        : base(rpcService, navigationService)
    {
        WalletName = walletName;
    }

    [RelayCommand]
    private async Task GetWalletInfo()
    {
        var job = CreateJob();

        if (RpcService.BatchMode)
        {
            OnBatch(job);
            return;
        }

        var result = await RpcService.Send<RpcGetWalletInfoResult>(job, NavigationService);
        if (result is RpcGetWalletInfoResult { Result: not null } rpcGetWalletInfoResult)
        {
            OnRpcSuccess(rpcGetWalletInfoResult);
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
        if (rpcResult is RpcGetWalletInfoResult rpcGetWalletInfoResult)
        {
            NavigationService.NavigateTo(rpcGetWalletInfoResult.Result?.ToViewModel(RpcService, NavigationService));
        }
    }

    public override Job CreateJob()
    {
        var requestBody = new RpcMethod
        {
            Method = "getwalletinfo"
        };

        var rpcServerUri = $"{RpcService.ServerPrefix}/{WalletName}";

        return new Job(requestBody, rpcServerUri);
    }
}
