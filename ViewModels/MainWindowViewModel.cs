using CommunityToolkit.Mvvm.Input;
using HttpPoster.Services;

namespace HttpPoster.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly DataService _dataService;

    public RequestViewModel Request { get; }
    public SidePanelViewModel SidePanel { get; }

    public MainWindowViewModel()
    {
        _dataService = new DataService();
        _dataService.Load();

        var httpService = new HttpService();
        Request = new RequestViewModel(httpService, _dataService);
        SidePanel = new SidePanelViewModel(_dataService);

        SidePanel.FavoriteSelected += r => Request.LoadFrom(r);
        SidePanel.HistorySelected += e => Request.LoadFromHistory(e);
        Request.HistoryEntryAdded += SidePanel.PrependHistoryEntry;

        SidePanel.Reload();
    }

    [RelayCommand]
    private void SaveFavorite()
    {
        SidePanel.ShowSaveFavorite();
    }

    [RelayCommand]
    private void ConfirmSaveFavorite()
    {
        var request = Request.ToSavedRequest(string.Empty);
        SidePanel.ConfirmSaveFavorite(request);
    }
}
