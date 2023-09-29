using ScrivenerExplorer.Interfaces;
using Windows.Storage.Pickers;
using ScrivenerExplorer.Models;

namespace ScrivenerExplorer
{
    public class FileSelector : IFileSelector
    {
        public event EventHandler<FileSelectorResult> OnFileSelected;

        public async Task SelectAsync()
        {
            var filePicker = new FileOpenPicker();
            filePicker.FileTypeFilter.Add("*");

            var appCurrent = Microsoft.Maui.Controls.Application.Current;
            if (appCurrent is null)
            {
                throw new InvalidOperationException($"Could not get current MAUI app instance.");
            }

            var mauiWinUIWindow = appCurrent.Windows[0].Handler.PlatformView as MauiWinUIWindow;
            if (mauiWinUIWindow is null)
            {
                throw new InvalidOperationException($"Could not get windows platform view.");
            }

            // Associate the HWND with the file picker
            WinRT.Interop.InitializeWithWindow.Initialize(filePicker, mauiWinUIWindow.WindowHandle);

            var result = await filePicker.PickSingleFileAsync();

            var fileSelectorResult = new FileSelectorResult
            {
                HasFile = result.IsAvailable,
                FileName = result.Name,
                FilePath = result.Path

            };

            if (result is not null)
            {
                OnFileSelected?.Invoke(this, fileSelectorResult);
            }
        }
    }
}
