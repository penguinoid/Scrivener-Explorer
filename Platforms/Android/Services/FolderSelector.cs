using Android.Content;
using ScrivenerExplorer.Interfaces;

namespace ScrivenerExplorer.Platforms.Android.Services
{
    public class FolderSelector : IFolderSelector
    {
        public Task SelectAsync()
        {
            var currentActivity = Platform.CurrentActivity;
            if (currentActivity is null)
            {
                throw new System.InvalidOperationException($"Could not get current MAUI activity.");
            }

            using var intent = new Intent(Intent.ActionOpenDocumentTree);
            intent.PutExtra("android.content.extra.SHOW_ADVANCED", true);
            intent.PutExtra("android.content.extra.FANCY", true);
            currentActivity.StartActivityForResult(intent, MainActivity.ReadFolderResultCode);

            return Task.CompletedTask;
        }
    }
}
