namespace WasabiRpc.Models.Services;

public interface IRoutable
{
    IRpcServiceViewModel RpcService { get;  }

    INavigationService NavigationService { get; }
}
