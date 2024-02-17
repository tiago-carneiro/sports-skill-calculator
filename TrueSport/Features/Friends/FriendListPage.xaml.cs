namespace TrueSport;

public partial class FriendListPage
{
    public FriendListPage(IEnumerable<FriendModel> friends)
    {
        InitializeComponent();
        collectionView.ItemsSource = friends;
    }

    void CollectionView_SelectionChanged(System.Object sender, Microsoft.Maui.Controls.SelectionChangedEventArgs e)
        => Close((sender as CollectionView).SelectedItem);
}
