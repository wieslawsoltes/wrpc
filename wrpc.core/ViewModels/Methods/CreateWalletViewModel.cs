using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiRpc.ViewModels.Factories;
using WasabiRpc.Models.App;
using WasabiRpc.Models.BatchMode;
using WasabiRpc.Models.Info;
using WasabiRpc.Models.Results;
using WasabiRpc.Models.Services;

namespace WasabiRpc.ViewModels.Methods;

public partial class CreateWalletViewModel : RoutableMethodViewModel
{
    [NotifyCanExecuteChangedFor(nameof(CreateWalletCommand))]
    [ObservableProperty] 
    private string? _walletName;

    [ObservableProperty] private string? _walletPassword;

    public CreateWalletViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService, IBatchManager batchManager)
        : base(rpcService, navigationService, batchManager)
    {
        WalletName = "Wallet";
        WalletPassword = "";
    }

    private bool CanCreateWallet()
    {
        return WalletName is not null 
               && WalletName.Length > 0;
    }

    [RelayCommand(CanExecute = nameof(CanCreateWallet))]
    private async Task CreateWallet()
    {
        await RunCommand();
    }

    public override async Task<IRoutable?> Execute(Job job)
    {
        var result = await RpcService.Send<RpcCreateWalletResult>(job.RpcMethod, job.RpcServerUri);
        return ToJobResult(result);
    }

    public override IRoutable? ToJobResult(object? result)
    {
        if (result is RpcCreateWalletResult { Result: not null } rpcCreateWalletResult)
        {
            return new CreateWalletInfo { Mnemonic = rpcCreateWalletResult.Result }.ToViewModel(RpcService, NavigationService);
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
        var requestBody = new CreateWalletRpcMethod
        {
            Method = "createwallet",
            Params = new []
            {
                WalletName,
                WalletPassword
            }
        };

        var rpcServerUri = $"{RpcService.ServerPrefix}";

        return new Job("createwallet", requestBody, rpcServerUri);
    }
}
