using ScrivenerExplorer;
using ScrivenerExplorer.Interfaces;
using ScrivenerExplorer.ViewModels;

namespace Scrivener;

public partial class FolderPage : ContentPage
{
    private readonly IStorageRepository _storageRepository;

    public FolderPage(Folder folder, IStorageRepository storageRepository)
    {
        _storageRepository = storageRepository;
        InitializeComponent();
        BindingContext = folder;
    }

    private void FolderItem_OnTapped(object sender, EventArgs e)
    {
        var viewCell = sender as ViewCell;
        var folderItem = viewCell.BindingContext as FolderItem;
        Navigation.PushAsync(new FolderItemPage(folderItem, _storageRepository));
    }
}