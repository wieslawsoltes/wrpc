using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using WasabiCli.Models.Services;

namespace WasabiCli.ViewModels.Info;

public partial class StatusInfoViewModel : RoutableViewModel
{
    [ObservableProperty] 
    private string? _torStatus;

    [ObservableProperty] 
    private string? _onionService;

    [ObservableProperty] 
    private string? _backendStatus;

    [ObservableProperty] 
    private string? _bestBlockchainHeight;

    [ObservableProperty] 
    private string? _bestBlockchainHash;

    [ObservableProperty] 
    private int _filtersCount;

    [ObservableProperty] 
    private int _filtersLeft;

    [ObservableProperty] 
    private string? _network;

    [ObservableProperty] 
    private decimal _exchangeRate;

    [ObservableProperty] 
    private List<PeerInfoViewModel>? _peers;

    public StatusInfoViewModel(IRpcServiceViewModel rpcService, INavigationService navigationService)
        : base(rpcService, navigationService)
    {
    }

    // TODO:
}
