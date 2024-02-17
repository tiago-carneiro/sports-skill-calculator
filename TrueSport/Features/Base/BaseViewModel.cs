namespace TrueSport;

public abstract partial class BaseViewModel : ObservableObject, IQueryAttributable
{
    [ObservableProperty]
    bool _isLoading;

    public virtual void ApplyQueryAttributes(IDictionary<string, object> query)
    {

    }

    protected async Task ShowToastAsync(string message)
        => await Toast.Make(message).Show();
}