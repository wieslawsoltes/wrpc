﻿using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using WasabiRpc.ViewModels.Factories;
using WasabiRpc.Models.App;
using WasabiRpc.Models.BatchMode;
using WasabiRpc.Models.Rpc;
using WasabiRpc.Models.Rpc.Methods;
using WasabiRpc.Models.Rpc.Results;
using WasabiRpc.Models.Services;

namespace WasabiRpc.ViewModels.Methods;

public partial class GetStatusViewModel : RoutableMethodViewModel
{
    public GetStatusViewModel(
        IRpcServiceViewModel rpcService, 
        INavigationService navigationService,
        INavigationService detailsNavigationService, 
        IBatchManager batchManager)
        : base(rpcService, navigationService, detailsNavigationService, batchManager)
    {
    }

    [RelayCommand]
    private async Task GetStatus()
    {
        await RunCommand();
    }

    public override async Task<IRoutable?> Execute(Job job)
    {
        var result = await RpcService.Send<RpcGetStatusResult>(job.RpcMethod, job.RpcServerUri);
        return ToJobResult(result);
    }

    public override IRoutable? ToJobResult(object? result)
    {
        if (result is RpcGetStatusResult { Result: not null } rpcGetStatusResult)
        {
            return rpcGetStatusResult.Result?.ToViewModel(RpcService, NavigationService, DetailsNavigationService, GetStatusCommand);
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
        var requestBody = new GetStatusRpcMethod
        {
            Method = "getstatus"
        };

        var rpcServerUri = $"{RpcService.ServerPrefix}";

        return new Job("getstatus", requestBody, rpcServerUri);
    }
}
