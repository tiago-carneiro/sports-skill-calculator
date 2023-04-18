namespace TrueSport;

public partial class GamePage : ContentPage
{
	public GamePage(GameViewModel viewModel)
	{
		InitializeComponent();
		this.SetViewModel(viewModel);
	}
}