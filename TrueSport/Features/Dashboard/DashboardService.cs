namespace TrueSport;

public interface IDashboardService
{
    Task<int> GetSkillRatingAsync();
}

public class DashboardService : IDashboardService
{
    public Task<int> GetSkillRatingAsync()
    {
        var skill = StoreService.Instance.GetCollection<UserModel>().Query().First().Skill;
        return Task.FromResult(skill);
    }
}