namespace TrueSport;

//TODO create as page with searchbox
public partial class FriendListPage
{
    public FriendListPage(FriendListViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    void CollectionView_SelectionChanged(System.Object sender, Microsoft.Maui.Controls.SelectionChangedEventArgs e)
        => Close((sender as CollectionView).SelectedItem);
}
