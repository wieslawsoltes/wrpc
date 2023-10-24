using WasabiRpc.Models.App;
using WasabiRpc.Models.BatchMode;
using WasabiRpc.Models.Services;
using WasabiRpc.ViewModels.App;

namespace WasabiRpc.ViewModels.Factories;

public static class AppFactory
{
    public static ErrorViewModel ToViewModel(this Error error, IRpcServiceViewModel rpcService, INavigationService navigationService)
    {
        return new ErrorViewModel(rpcService, navigationService)
        {
            Message = error.Message,
        };
    }

    public static SuccessViewModel ToViewModel(this Success success, IRpcServiceViewModel rpcService, INavigationService navigationService)
    {
        return new SuccessViewModel(rpcService, navigationService)
        {
            Message = success.Message,
        };
    }
}
