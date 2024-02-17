namespace TrueSport;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(GamePage), typeof(GamePage));
    }

    //when we call some page from menu or root the ApplyQueryAttributes method is never called
    protected override void OnNavigated(ShellNavigatedEventArgs args)
    {
        base.OnNavigated(args);
        if ((args.Source == ShellNavigationSource.ShellItemChanged ||
             args.Source == ShellNavigationSource.PopToRoot) &&
            CurrentPage?.BindingContext is BaseViewModel baseViewModel)
        {
            baseViewModel.ApplyQueryAttributes(new Dictionary<string, object>());
        }
    }
}