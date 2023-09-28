using Android.App;
using Android.Content;
using Android.Net;
using Android.OS;
using Android.Provider;
using ScrivenerExplorer.Interfaces;
using Application = Android.App.Application;
using Environment = Android.OS.Environment;
using Uri = Android.Net.Uri;

namespace ScrivenerExplorer.Platforms.Android.Services
{
    public class FileSelector : IFileSelector
    {
        public Task SelectAsync()
        {
            var currentActivity = Platform.CurrentActivity;
            if (currentActivity is null)
            {
                throw new System.InvalidOperationException($"Could not get current MAUI activity.");
            }

            if (!Environment.IsExternalStorageManager)
            {
                var uri = Uri.Parse($"package:{Application.Context?.ApplicationInfo?.PackageName}");
                var permissionIntent = new Intent(Settings.ActionManageAppAllFilesAccessPermission, uri);
                currentActivity.StartActivity(permissionIntent);
            }

            var intent = new Intent(Intent.ActionOpenDocument);
            intent.AddCategory(Intent.CategoryOpenable);
            intent.SetType("*/*");

            intent.PutExtra(DocumentsContract.ExtraInitialUri, MediaStore.Downloads.ExternalContentUri);

            currentActivity.StartActivityForResult(intent, MainActivity.ReadFileResultCode);

            return Task.CompletedTask;
        }
    }
}
