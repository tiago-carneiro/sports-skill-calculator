namespace TrueSport;

public partial class FriendListViewModel : BaseViewModel
{
    readonly IFriendService _friendService;

    [ObservableProperty]
    IEnumerable<FriendModel> _friends;

    public FriendListViewModel(IFriendService friendService)
        => _friendService = friendService;

    public override async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Friends = await _friendService.GetAsync();
    }
}