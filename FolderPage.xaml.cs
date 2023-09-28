using Scrivener.Models;

namespace Scrivener;

public partial class FolderPage : ContentPage
{
    public FolderPage(Folder folder)
    {
        InitializeComponent();
        BindingContext = folder;
    }

    private void FolderItem_OnTapped(object sender, EventArgs e)
    {
        var viewCell = sender as ViewCell;
        var folderItem = viewCell.BindingContext as FolderItem;
        Navigation.PushAsync(new FolderItemPage(folderItem));
    }
}