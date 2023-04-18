using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace TrueSport;

public partial class DashboardViewModel : BaseViewModel
{
    readonly IDashboardService _dashboardService;

    [ObservableProperty]
    int _skillRating;

    public DashboardViewModel(IDashboardService dashboardService)
        => _dashboardService = dashboardService;

    public async override Task InitializeAsync()
        => SkillRating = await _dashboardService.GetSkillRatingAsync();
    
    [RelayCommand]
    async Task CalculateSkill()
        => await Shell.Current.GoToAsync(nameof(GamePage));
}