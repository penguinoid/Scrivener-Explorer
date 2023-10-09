using CommunityToolkit.Maui.Storage;
using ScrivenerExplorer.ViewModels;

namespace ScrivenerExplorer.Interfaces
{
    public interface IProjectViewModelFactory
    {
        ProjectFile CreateViewModel(FolderPickerResult result);
    }
}
