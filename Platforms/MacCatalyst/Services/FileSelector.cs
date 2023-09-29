using ScrivenerExplorer.Interfaces;

namespace ScrivenerExplorer
{
    public class FileSelector : IFileSelector
    {
        public Task SelectAsync()
        {
            throw new FeatureNotSupportedException();
        }
    }
}
