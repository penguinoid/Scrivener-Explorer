using ScrivenerExplorer.Interfaces;
using ScrivenerExplorer.Models;

namespace ScrivenerExplorer
{
    public class FileSelectResultHandler : IFileSelectResultHandler
    {
        public void SetHandler(EventHandler<FileSelectorResult> onFileSelectedDelegate)
        {
            throw new FeatureNotSupportedException();
        }
    }
}
