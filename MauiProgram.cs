using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Storage;
using Microsoft.Extensions.Logging;
using ScrivenerExplorer.Interfaces;
using ScrivenerExplorer.Services;

namespace ScrivenerExplorer
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<IFileSelector, FileSelector>();
            builder.Services.AddSingleton<IFileSelectResultHandler, FileSelectResultHandler>();
            builder.Services.AddSingleton<IProjectViewModelFactory, ProjectViewModelFactory>();
            builder.Services.AddSingleton<IFolderPicker>(FolderPicker.Default);

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}