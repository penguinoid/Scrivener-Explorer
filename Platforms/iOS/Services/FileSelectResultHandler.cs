using ScrivenerExplorer.Interfaces;
using System;

namespace ScrivenerExplorer
{
    public class FileSelectResultHandler : IFileSelectResultHandler
    {
        public void SetHandler(EventHandler<string?> onFileSelectedDelegate)
        {
            throw new FeatureNotSupportedException();
        }
    }
}
