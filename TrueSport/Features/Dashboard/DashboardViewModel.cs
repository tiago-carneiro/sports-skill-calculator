namespace TrueSport;

public partial class DashboardViewModel : BaseViewModel
{
    readonly IDashboardService _dashboardService;

    [ObservableProperty]
    int _skillRating;

    public DashboardViewModel(IDashboardService dashboardService)
        => _dashboardService = dashboardService;

    public override async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        SkillRating = await _dashboardService.GetSkillRatingAsync();
    }
    
    [RelayCommand]
    async Task CalculateSkill()
        => await Shell.Current.GoToAsync(nameof(GamePage));
}