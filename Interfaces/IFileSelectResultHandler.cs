using ScrivenerExplorer.Models;

namespace ScrivenerExplorer.Interfaces
{
    public interface IFileSelectResultHandler
    {
        public void SetHandler(EventHandler<FileSelectorResult> onFileSelectedDelegate);
    }
}
