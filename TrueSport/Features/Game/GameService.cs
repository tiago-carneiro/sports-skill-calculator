namespace TrueSport;

public enum GameWinner
{
    MyTeam,
    Opposite
}

public interface IGameService
{
    Task CalculateSkillRatingAsync(GameWinner winner);
}

public class GameService : IGameService
{
    public async Task CalculateSkillRatingAsync(GameWinner winner)
    {
        var query = ConstantsHelper.ApiUrl
                     .AppendPathSegment("api/v1.0/random");

        var collection = StoreService.Instance.GetCollection<UserModel>();
        var user = collection.Query().First();

        switch (winner)
        {
            case GameWinner.MyTeam:
                query = query.SetQueryParam("min", user.Skill).SetQueryParam("max", 10);
                break;
            case GameWinner.Opposite:
                query = query.SetQueryParam("min", 0).SetQueryParam("max", user.Skill);
                break;
        }

        var result = await query.GetJsonAsync<int[]>();
        user.Skill = result.First();
        collection.Upsert(user);
    }
}