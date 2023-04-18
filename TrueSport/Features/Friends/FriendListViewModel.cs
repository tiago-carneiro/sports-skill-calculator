using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace TrueSport
{
    public partial class FriendListViewModel : BaseViewModel
    {
        readonly IFriendService _friendService;

        [ObservableProperty]
        IEnumerable<FriendModel> _friends;

        public FriendListViewModel(IFriendService friendService)
            => _friendService = friendService;

        public override async Task InitializeAsync()
            => Friends = await _friendService.GetAsync();
    }
}