using System;
namespace TrueSport
{
    public interface IFriendService
    {
        Task<IEnumerable<FriendModel>> GetAsync();
    }

    public class FriendService : IFriendService
    {
        public Task<IEnumerable<FriendModel>> GetAsync()
        {
            var friends = StoreService.Instance.GetCollection<FriendModel>().FindAll().ToList();
            return Task.FromResult<IEnumerable<FriendModel>>(friends);
        }
    }
}