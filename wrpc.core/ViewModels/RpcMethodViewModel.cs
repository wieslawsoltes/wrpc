using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace WasabiRpc.ViewModels;

public partial class RpcMethodViewModel : ViewModelBase
{
    [ObservableProperty] private string _name;
    [ObservableProperty] private IRelayCommand _command;

    public RpcMethodViewModel(string name, IRelayCommand command)
    {
        _name = name;
        _command = command;
    }
}
