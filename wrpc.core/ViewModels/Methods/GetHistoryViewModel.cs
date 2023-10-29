using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiRpc.ViewModels.Factories;
using WasabiRpc.Models.App;
using WasabiRpc.Models.BatchMode;
using WasabiRpc.Models.Info;
using WasabiRpc.Models.Rpc;
using WasabiRpc.Models.Rpc.Methods;
using WasabiRpc.Models.Rpc.Results;
using WasabiRpc.Models.Services;

namespace WasabiRpc.ViewModels.Methods;

public partial class GetHistoryViewModel : RoutableMethodViewModel
{
    [ObservableProperty] private string? _walletName;

    public GetHistoryViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService, IBatchManager batchManager, string? walletName)
        : base(rpcService, navigationService, batchManager)
    {
        WalletName = walletName;
    }

    [RelayCommand]
    private async Task GetHistory()
    {
        await RunCommand();
    }

    public override async Task<IRoutable?> Execute(Job job)
    {
        var result = await RpcService.Send<RpcGetHistoryResult>(job.RpcMethod, job.RpcServerUri);
        return ToJobResult(result);
    }

    public override IRoutable? ToJobResult(object? result)
    {
        if (result is RpcGetHistoryResult { Result: not null } rpcGetHistoryResult)
        {
            return new GetHistoryInfo { Transactions = rpcGetHistoryResult.Result }.ToViewModelAdapter(RpcService, NavigationService, BatchManager, WalletName, GetHistoryCommand);
        }

        if (result is RpcErrorResult { Error: not null } rpcErrorResult)
        {
            return rpcErrorResult.Error?.ToViewModel(RpcService, NavigationService);
        }

        if (result is Error error)
        {
            return error.ToViewModel(RpcService, NavigationService);
        }

        return null;
    }

    public override Job CreateJob()
    {
        var requestBody = new GetHistoryRpcMethod
        {
            Method = "gethistory"
        };

        var rpcServerUri = $"{RpcService.ServerPrefix}/{WalletName}";

        return new Job("gethistory", requestBody, rpcServerUri);
    }
}
