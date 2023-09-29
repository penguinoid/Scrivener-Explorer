﻿using ScrivenerExplorer.Interfaces;
using ScrivenerExplorer.Models;

namespace ScrivenerExplorer
{
    public class FileSelectResultHandler : IFileSelectResultHandler
    {
        public void SetHandler(EventHandler<FileSelectorResult> onFileSelectedDelegate)
        {
            if (Platform.CurrentActivity is not MainActivity currentActivity)
            {
                throw new Exception($"Current activity is not of type {typeof(MainActivity).Name}");
            }

            currentActivity.OnFileSelected += onFileSelectedDelegate;
        }
    }
}
