using ScrivenerExplorer.Models;

namespace ScrivenerExplorer.Interfaces
{
    public interface IFolderSelectResultHandler
    {
        void SetHandler(EventHandler<FolderSelectorResult> onFolderSelectedDelegate);
    }
}
