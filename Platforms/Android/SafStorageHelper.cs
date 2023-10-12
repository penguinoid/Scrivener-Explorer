using Android.App;
using Android.Content;
using Android.Provider;

namespace ScrivenerExplorer
{
    public static class SafStorageHelper
    {
        /// <summary>
        /// return null if the request does not belong to requestId
        /// </summary>
        public static Uri ResolveFromActivityResult(Activity activity, Intent data)
        {
            var androidUri = data.Data;
            var takeFlags = data.Flags & (ActivityFlags.GrantReadUriPermission | ActivityFlags.GrantWriteUriPermission);
            activity.ContentResolver.TakePersistableUriPermission(androidUri, takeFlags);
            var storageUri = DocumentsContract.BuildDocumentUriUsingTree(androidUri, DocumentsContract.GetTreeDocumentId(androidUri));
            return new Uri(storageUri.ToString());
        }
    }
}
