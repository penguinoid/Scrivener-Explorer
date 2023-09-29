using ScrivenerExplorer.Interfaces;
using ScrivenerExplorer.Models;

namespace ScrivenerExplorer
{
    public class FileSelectResultHandler : IFileSelectResultHandler
    {
        private readonly FileSelector _windowsFileSelector;
        public FileSelectResultHandler(FileSelector fileSelector)
        {
            _windowsFileSelector = fileSelector;
        }

        public void SetHandler(EventHandler<FileSelectorResult> onFileSelectedDelegate)
        {
            _windowsFileSelector.OnFileSelected += onFileSelectedDelegate;
        }
    }
}
