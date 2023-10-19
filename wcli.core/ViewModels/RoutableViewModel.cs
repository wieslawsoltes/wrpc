using WasabiCli.Models.Services;

namespace WasabiCli.ViewModels;

public abstract partial class RoutableViewModel : ViewModelBase, IRoutable
{
    protected RoutableViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService)
    {
        RpcService = rpcService;
        NavigationService = navigationService;
    }

    public IRpcServiceViewModel RpcService { get;  }

    public INavigationService NavigationService { get; }
}
