using ScrivenerExplorer.Interfaces;
using ScrivenerExplorer.Models;

namespace ScrivenerExplorer.Platforms.Android.Services
{
    public class FolderSelectResultHandler : IFolderSelectResultHandler
    {
        public void SetHandler(EventHandler<FolderSelectorResult> onFolderSelectedDelegate)
        {
            if (Platform.CurrentActivity is not MainActivity currentActivity)
            {
                throw new Exception($"Current activity is not of type {typeof(MainActivity).Name}");
            }

            currentActivity.OnFolderSelected += onFolderSelectedDelegate;
        }
    }
}
