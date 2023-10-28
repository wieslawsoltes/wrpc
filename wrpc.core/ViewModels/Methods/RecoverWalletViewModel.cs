using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WasabiRpc.ViewModels.Factories;
using WasabiRpc.Models.App;
using WasabiRpc.Models.BatchMode;
using WasabiRpc.Models.Results;
using WasabiRpc.Models.Services;

namespace WasabiRpc.ViewModels.Methods;

public partial class RecoverWalletViewModel : RoutableMethodViewModel
{
    [NotifyCanExecuteChangedFor(nameof(RecoverWalletCommand))]
    [ObservableProperty] 
    private string? _walletName;

    [NotifyCanExecuteChangedFor(nameof(RecoverWalletCommand))]
    [ObservableProperty] 
    private string? _walletMnemonic;

    [ObservableProperty] private string? _walletPassword;

    public RecoverWalletViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService, IBatchManager batchManager)
        : base(rpcService, navigationService, batchManager)
    {
        WalletName = "Wallet";
        WalletMnemonic = "";
        WalletPassword = "";
    }

    private bool CanRecoverWallet()
    {
        return WalletName is not null 
               && WalletName.Length > 0
               && WalletMnemonic is not null 
               && WalletMnemonic.Length > 0;
    }

    [RelayCommand(CanExecute = nameof(CanRecoverWallet))]
    private async Task RecoverWallet()
    {
        await RunCommand();
    }

    public override async Task<IRoutable?> Execute(Job job)
    {
        var result = await RpcService.Send<RpcRecoverWalletResult>(job.RpcMethod, job.RpcServerUri);
        return ToJobResult(result);
    }

    public override IRoutable? ToJobResult(object? result)
    {
        if (result is RpcRecoverWalletResult)
        {
            return new Success { Message = $"Recovered wallet {WalletName}" }.ToViewModel(RpcService, NavigationService);
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
        var requestBody = new RecoverWalletRpcMethod
        {
            Method = "recoverwallet",
            Params = new []
            {
                WalletName,
                WalletMnemonic,
                WalletPassword
            }
        };

        var rpcServerUri = $"{RpcService.ServerPrefix}";

        return new Job("recoverwallet", requestBody, rpcServerUri);
    }
}
