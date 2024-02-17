namespace TrueSport;

public partial class GameViewModel : BaseViewModel
{
    readonly IGameService _gameService;
    readonly IFriendService _friendService;

    [ObservableProperty]
    int _type, _winner;

    [ObservableProperty]
    FriendModel _partner;

    public ObservableCollection<FriendModel> Opponents { get; }

    public GameViewModel(IGameService gameService,
                        IFriendService friendService)
    {
        _gameService = gameService;
        _friendService = friendService;

        Opponents = new ObservableCollection<FriendModel>();
    }

    partial void OnTypeChanged(int value)
        => Partner = null;

    [RelayCommand]
    async Task SelectPartner()
    {
        var friend = await SelectFriendAsync();
        if (friend != null)
        {
            if (Opponents.Any(a => a.Id == friend.Id))
                await ShowToastAsync($"{friend.Name} - already in the opponents list");
            else
                Partner = friend;
        }
    }

    [RelayCommand]
    async Task SelectOpponent()
    {
        var friend = await SelectFriendAsync();
        if (friend != null)
        {
            switch (Type)
            {
                case 0:
                    Opponents.Clear();
                    Opponents.Add(friend);
                    break;
                case 1:
                    if (Opponents.Any(a => a.Id == friend.Id))
                    {
                        await ShowToastAsync($"{friend.Name} - already in the list");
                        return;
                    }

                    if(Partner.Id == friend.Id)
                    {
                        await ShowToastAsync($"{friend.Name} - is your partner");
                        return;
                    }

                    Opponents.Add(friend);
                    break;
            }
        }
    }

    [RelayCommand]
    Task RemoveOpponent(FriendModel opponent)
    {
        Opponents.Remove(opponent);
        return Task.CompletedTask;
    }

    [RelayCommand]
    async Task CalculateSkill()
    {
        if (!await IsFormValidAsync())
            return;

        var success = await _gameService.CalculateSkillRatingAsync((GameWinner)Winner).Handle(this);
        if (success)            
            await Shell.Current.GoToAsync("../");
    }

    async Task<FriendModel> SelectFriendAsync()
    {
        var friends = await _friendService.GetAsync();

        var result = await Shell.Current.CurrentPage.ShowPopupAsync(new FriendListPage(friends));

        if (result is FriendModel friend)
            return friend;

        return null;
    }

    async Task<bool> IsFormValidAsync()
    {
        switch (Type)
        {
            case 0:
                if (Opponents.Count == 0)
                {
                    await ShowToastAsync("You should select your opponent");
                    return false;
                }
                break;

            case 1:
                if (Partner == null)
                {
                    await ShowToastAsync("You should select your partner");
                    return false;
                }

                if (Opponents.Count != 2)
                {
                    await ShowToastAsync("You should select 2 opponents");
                    return false;
                }
                break;
        }

        return true;
    }
}