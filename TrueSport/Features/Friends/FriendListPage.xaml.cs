namespace TrueSport;

public partial class FriendListPage
{
    public FriendListPage(FriendListViewModel viewModel)
    {
        InitializeComponent();
        this.SetViewModel(viewModel);
    }

    void CollectionView_SelectionChanged(System.Object sender, Microsoft.Maui.Controls.SelectionChangedEventArgs e)
        => Close((sender as CollectionView).SelectedItem);
}
