using System.Globalization;
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

public partial class PayInCoinjoinViewModel : RoutableMethodViewModel
{
    [NotifyCanExecuteChangedFor(nameof(PayInCoinjoinCommand))]
    [ObservableProperty] 
    private string? _walletName;

    [NotifyCanExecuteChangedFor(nameof(PayInCoinjoinCommand))]
    [ObservableProperty] 
    private string? _address;

    [NotifyCanExecuteChangedFor(nameof(PayInCoinjoinCommand))]
    [ObservableProperty] 
    private decimal _amount;

    public PayInCoinjoinViewModel(
        IRpcServiceViewModel rpcService, 
        INavigationService navigationService,
        INavigationService detailsNavigationService, 
        IBatchManager batchManager, string? walletName)
        : base(rpcService, navigationService, detailsNavigationService, batchManager)
    {
        WalletName = walletName;
        Address = "";
        Amount = 0;
    }

    private bool CanPayInCoinjoin()
    {
        return WalletName is not null 
               && WalletName.Length > 0
               && Address is not null
               && Address.Length > 0
               && Amount > 0;
    }

    [RelayCommand(CanExecute = nameof(CanPayInCoinjoin))]
    private async Task PayInCoinjoin()
    {
        await RunCommand();
    }

    public override async Task<IRoutable?> Execute(Job job)
    {
        var result = await RpcService.Send<RpcPayInCoinjoinResult>(job.RpcMethod, job.RpcServerUri);
        return ToJobResult(result);
    }

    public override IRoutable? ToJobResult(object? result)
    {
        if (result is RpcPayInCoinjoinResult { Result: not null } rpcPayInCoinjoinResult)
        {
            return new PayInCoinjoinInfo { PaymentId = rpcPayInCoinjoinResult.Result }.ToViewModel(RpcService, NavigationService, DetailsNavigationService);
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
        var requestBody = new PayInCoinjoinRpcMethod
        {
            Method = "payincoinjoin",
            Params = new []
            {
                Address,
                Amount.ToString(CultureInfo.InvariantCulture)
            }
        };

        var rpcServerUri = $"{RpcService.ServerPrefix}/{WalletName}";

        return new Job("payincoinjoin", requestBody, rpcServerUri);
    }
}
