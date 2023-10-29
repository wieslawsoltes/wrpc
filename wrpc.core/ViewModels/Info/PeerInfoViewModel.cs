using System;
using CommunityToolkit.Mvvm.ComponentModel;
using WasabiRpc.Models.Services;

namespace WasabiRpc.ViewModels.Info;

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

    public PeerInfoViewModel(
        IRpcServiceViewModel rpcService, 
        INavigationService navigationService,
        INavigationService detailsNavigationService)
        : base(rpcService, navigationService, detailsNavigationService)
    {
    }
}
