﻿using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using PortableStorage.Droid;
using ScrivenerExplorer.Models;

namespace ScrivenerExplorer
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        public event EventHandler<FolderSelectorResult> OnFolderSelected;

        public const int ReadFolderResultCode = 1;

        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Platform.Init(this, savedInstanceState);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            switch (requestCode)
            {
                case ReadFolderResultCode:
                    if (data?.Data is not null)
                    {
                        var storageUri = SafStorageHelper.ResolveFromActivityResult(this, data);

                        var result = new FolderSelectorResult()
                        {
                            StorageRoot = SafStorgeProvider.CreateStorage(this, storageUri),
                            HasData = true
                        };

                        OnFolderSelected?.Invoke(this, result);
                    }
                    break;
            }
        }
    }
}