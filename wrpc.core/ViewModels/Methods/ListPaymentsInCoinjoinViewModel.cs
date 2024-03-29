using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiRpc.Models.App;
using WasabiRpc.Models.BatchMode;
using WasabiRpc.Models.Info;
using WasabiRpc.Models.Rpc;
using WasabiRpc.Models.Rpc.Methods;
using WasabiRpc.Models.Rpc.Results;
using WasabiRpc.Models.Services;
using WasabiRpc.ViewModels.Factories;

namespace WasabiRpc.ViewModels.Methods;

public partial class ListPaymentsInCoinjoinViewModel : RoutableMethodViewModel
{
    [ObservableProperty] private string? _walletName;

    public ListPaymentsInCoinjoinViewModel(
        IRpcServiceViewModel rpcService, 
        INavigationService navigationService,
        INavigationService detailsNavigationService, 
        IBatchManager batchManager, string? walletName)
        : base(rpcService, navigationService, detailsNavigationService, batchManager)
    {
        WalletName = walletName;
    }

    [RelayCommand]
    private async Task ListPaymentsInCoinjoin()
    {
        await RunCommand();
    }

    public override async Task<IRoutable?> Execute(Job job)
    {
        var result = await RpcService.Send<RpcListPaymentsInCoinjoinResult>(job.RpcMethod, job.RpcServerUri);
        return ToJobResult(result);
    }

    public override IRoutable? ToJobResult(object? result)
    {
        if (result is RpcListPaymentsInCoinjoinResult { Result: not null } rpcListPaymentsInCoinjoinResult)
        {
            return new ListPaymentsInCoinjoinInfo { Payments = rpcListPaymentsInCoinjoinResult.Result }.ToViewModel(RpcService, NavigationService, DetailsNavigationService, ListPaymentsInCoinjoinCommand);
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
        var requestBody = new ListPaymentsInCoinjoinRpcMethod
        {
            Method = "listpaymentsincoinjoin"
        };

        var rpcServerUri = $"{RpcService.ServerPrefix}/{WalletName}";

        return new Job("listpaymentsincoinjoin", requestBody, rpcServerUri);
    }
}
