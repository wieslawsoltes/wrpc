﻿using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiRpc.ViewModels.Factories;
using WasabiRpc.Models.App;
using WasabiRpc.Models.BatchMode;
using WasabiRpc.Models.Rpc;
using WasabiRpc.Models.Rpc.Methods;
using WasabiRpc.Models.Rpc.Results;
using WasabiRpc.Models.Services;

namespace WasabiRpc.ViewModels.Methods;

public partial class LoadWalletViewModel : RoutableMethodViewModel
{
    [ObservableProperty] private string? _walletName;

    public LoadWalletViewModel(
        IRpcServiceViewModel rpcService, 
        INavigationService navigationService,
        INavigationService detailsNavigationService, 
        IBatchManager batchManager, 
        string? walletName)
        : base(rpcService, navigationService, detailsNavigationService, batchManager)
    {
        WalletName = walletName;
    }

    [RelayCommand]
    private async Task LoadWallet()
    {
        await RunCommand();
    }

    public override async Task<IRoutable?> Execute(Job job)
    {
        var result = await RpcService.Send<RpcLoadWalletResult>(job.RpcMethod, job.RpcServerUri);
        return ToJobResult(result);
    }

    public override IRoutable? ToJobResult(object? result)
    {
        if (result is RpcLoadWalletResult)
        {
            return new Success { Message = $"Loaded wallet {WalletName}" }.ToViewModel(RpcService, NavigationService, DetailsNavigationService);
        }

        if (result is RpcErrorResult { Error: not null } rpcErrorResult)
        {
            return rpcErrorResult.Error?.ToViewModel(RpcService, NavigationService, DetailsNavigationService);
        }

        if (result is Error error)
        {
            return error.ToViewModel(RpcService, NavigationService, DetailsNavigationService);
        }

        return null;
    }

    public override Job CreateJob()
    {
        var requestBody = new LoadWalletRpcMethod
        {
            Method = "loadwallet",
            Params = new []
            {
                WalletName,
            }
        };

        var rpcServerUri = $"{RpcService.ServerPrefix}";
// requestBody -> json !!!
        return new Job("loadwallet", requestBody, rpcServerUri);
    }
}
