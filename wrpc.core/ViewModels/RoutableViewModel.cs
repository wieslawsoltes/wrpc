using WasabiRpc.Models.Services;

namespace WasabiRpc.ViewModels;

public abstract partial class RoutableViewModel : ViewModelBase, IRoutable
{
    protected RoutableViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService, INavigationService detailsNavigationService)
    {
        RpcService = rpcService;
        NavigationService = navigationService;
        DetailsNavigationService = detailsNavigationService;
    }

    public IRpcServiceViewModel RpcService { get;  }

    public INavigationService NavigationService { get; }

    public INavigationService DetailsNavigationService { get; }
}
