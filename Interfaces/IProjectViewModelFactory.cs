using CommunityToolkit.Maui.Storage;
using ScrivenerExplorer.Models;
using ScrivenerExplorer.ViewModels;

namespace ScrivenerExplorer.Interfaces
{
    public interface IProjectViewModelFactory
    {
        ProjectFile CreateViewModel(FolderSelectorResult result);
    }
}
