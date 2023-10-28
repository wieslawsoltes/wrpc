using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiRpc.ViewModels.Factories;
using WasabiRpc.Models.App;
using WasabiRpc.Models.BatchMode;
using WasabiRpc.Models.Results;
using WasabiRpc.Models.Services;

namespace WasabiRpc.ViewModels.Methods;

public partial class GetNewAddressViewModel : RoutableMethodViewModel
{
    [ObservableProperty] private string? _walletName;

    [NotifyCanExecuteChangedFor(nameof(GetNewAddressCommand))]
    [ObservableProperty] 
    private string? _label;

    public GetNewAddressViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService, IBatchManager batchManager, string? walletName)
        : base(rpcService, navigationService, batchManager)
    {
        WalletName = walletName;
        Label = "Label";
    }

    private bool CanGetNewAddress()
    {
        return Label is not null 
               && Label.Length > 0;
    }

    [RelayCommand(CanExecute = nameof(CanGetNewAddress))]
    private async Task GetNewAddress()
    {
        await RunCommand();
    }

    public override async Task<IRoutable?> Execute(Job job)
    {
        var result = await RpcService.Send<RpcGetNewAddressResult>(job.RpcMethod, job.RpcServerUri);
        return ToJobResult(result);
    }

    public override IRoutable? ToJobResult(object? result)
    {
        if (result is RpcGetNewAddressResult { Result: not null } rpcGetNewAddressResult)
        {
            return rpcGetNewAddressResult.Result?.ToViewModel(RpcService, NavigationService);
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
        var requestBody = new GetNewAddressRpcMethod
        {
            Method = "getnewaddress",
            Params = new []
            {
                Label
            }
        };

        var rpcServerUri = $"{RpcService.ServerPrefix}/{WalletName}";

        return new Job("getnewaddress", requestBody, rpcServerUri);
    }
}
