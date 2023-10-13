using WasabiCli.Models;
using WasabiCli.Models.App;
using WasabiCli.Models.RpcJson;

namespace WasabiCli.ViewModels;

public abstract partial class BatchMethodViewModel : ViewModelBase
{
    protected abstract void OnRpcSuccess(Rpc rpcResult);

    protected abstract void OnRpcError(RpcErrorResult rpcErrorResult);

    protected abstract void OnError(Error error);

    public abstract Job CreateJob();
}
