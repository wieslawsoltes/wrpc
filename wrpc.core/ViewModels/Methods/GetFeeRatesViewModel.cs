using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using WasabiRpc.ViewModels.Factories;
using WasabiRpc.Models.App;
using WasabiRpc.Models.BatchMode;
using WasabiRpc.Models.Info;
using WasabiRpc.Models.Results;
using WasabiRpc.Models.Services;

namespace WasabiRpc.ViewModels.Methods;

public partial class GetFeeRatesViewModel : RoutableMethodViewModel
{
    public GetFeeRatesViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService, IBatchManager batchManager)
        : base(rpcService, navigationService, batchManager)
    {
    }

    [RelayCommand]
    private async Task GetFeeRates()
    {
        await RunCommand();
    }

    public override async Task<IRoutable?> Execute(Job job)
    {
        var result = await RpcService.Send<RpcGetFeeRatesResult>(job.RpcMethod, job.RpcServerUri, NavigationService);
        if (result is RpcGetFeeRatesResult { Result: not null } rpcGetFeeRatesResult)
        {
            return new GetFeeRatesInfo { FeeRates = rpcGetFeeRatesResult.Result }.ToViewModel(RpcService, NavigationService);
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
        var requestBody = new RpcMethod
        {
            Method = "getfeerates"
        };

        var rpcServerUri = $"{RpcService.ServerPrefix}";

        return new Job("getfeerates", requestBody, rpcServerUri);
    }
}
