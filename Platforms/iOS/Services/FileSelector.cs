using Android.Content;
using Android.Provider;
using ScrivenerExplorer.Interfaces;
using System.Threading.Tasks;
using Application = Android.App.Application;
using Environment = Android.OS.Environment;
using Uri = Android.Net.Uri;

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
