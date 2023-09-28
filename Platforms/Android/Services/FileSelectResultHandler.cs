using ScrivenerExplorer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrivenerExplorer.Platforms.Android.Services
{
    public class FileSelectResultHandler : IFileSelectResultHandler
    {
        public void SetHandler(EventHandler<string> onFileSelectedDelegate)
        {
            if (Platform.CurrentActivity is not MainActivity currentActivity)
            {
                throw new Exception($"Current activity is not of type {typeof(MainActivity).Name}");
            }

            currentActivity.OnFileSelected += onFileSelectedDelegate;
        }
    }
}
