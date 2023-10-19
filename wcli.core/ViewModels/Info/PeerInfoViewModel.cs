using System;
using CommunityToolkit.Mvvm.ComponentModel;
using WasabiCli.Models.Services;

namespace WasabiCli.ViewModels.Info;

public partial class PeerInfoViewModel : RoutableViewModel
{
    [ObservableProperty] 
    private bool _isConnected;

    [ObservableProperty] 
    private DateTimeOffset _lastSeen;

    [ObservableProperty] 
    private string? _endpoint;

    [ObservableProperty] 
    private string? _userAgent;

    public PeerInfoViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService)
        : base(rpcService, navigationService)
    {
    }

    // TODO:
}
