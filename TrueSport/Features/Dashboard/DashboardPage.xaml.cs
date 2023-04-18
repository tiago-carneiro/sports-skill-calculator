namespace TrueSport;

public partial class DashboardPage : ContentPage
{
    bool _needUpdate;
    public DashboardPage(DashboardViewModel viewModel)
    {
        InitializeComponent();
        this.SetViewModel(viewModel);
    }

    protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
    {
        base.OnNavigatedFrom(args);
        _needUpdate = Shell.Current.CurrentPage is GamePage;
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        if (_needUpdate)
        {
            await (BindingContext as BaseViewModel).InitializeAsync();
            _needUpdate = false;
        }
    }
}