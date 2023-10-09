using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Storage;
using CommunityToolkit.Mvvm.Input;
using Scrivener;
using Scrivener.ViewModels;
using ScrivenerExplorer.Interfaces;
using ScrivenerExplorer.Models;
using ScrivenerExplorer.ViewModels;

namespace ScrivenerExplorer
{
    public partial class MainPage : ContentPage
    {
        private readonly IFileSelector _fileSelector;
        private readonly IFileSelectResultHandler _fileSelectResultHandler;
        private readonly IProjectViewModelFactory _projectViewModelFactory;
        private readonly IFolderPicker _folderPicker;

        public ProjectViewModel ProjectViewModel { get; set; }

        public MainPage(IFileSelector fileSelector, 
            IFileSelectResultHandler fileSelectResultHandler, 
            IProjectViewModelFactory projectViewModelFactory,
            IFolderPicker folderPicker)
        {
            _fileSelector = fileSelector;
            _fileSelectResultHandler = fileSelectResultHandler;
            _projectViewModelFactory = projectViewModelFactory;
            _folderPicker = folderPicker;

            InitializeComponent();
            //InitFileSelectResultHandler();

            ProjectViewModel = new ProjectViewModel
            {
                ProjectFile = new ProjectFile()
            };

            BindingContext = ProjectViewModel;
        }

        //private void InitFileSelectResultHandler()
        //{
        //    _fileSelectResultHandler.SetHandler(async (sender, result) =>
        //    {
        //        if (!result.HasFile)
        //        {
        //            return;
        //        }

        //        SetModel(result);
        //    });
        //}

        //public async Task PickFolder(CancellationToken cancellationToken)
        //{
        //    var result = await _folderPicker.PickAsync(cancellationToken);
        //    if (result.IsSuccessful)
        //    {
        //        await Toast.Make($"The folder was picked: Name - {result.Folder.Name}, Path - {result.Folder.Path}", ToastDuration.Long).Show(cancellationToken);
        //    }
        //    else
        //    {
        //        await Toast.Make($"The folder was not picked with error: {result.Exception.Message}").Show(cancellationToken);
        //    }
        //}

        public async void OnFolderPickerClicked(object sender, EventArgs args)
        {
            var cancellationToken = new CancellationToken(false);
            var result = await _folderPicker.PickAsync(cancellationToken);
            result.EnsureSuccess();
            SetModel(result);
            //await Toast.Make($"Folder picked: Name - {result.Folder.Name}, Path - {result.Folder.Path}", ToastDuration.Long).Show(cancellationToken);
        }

        //[RelayCommand]
        //public async Task PickFolder(CancellationToken cancellationToken)
        //{
        //    var folderPickerResult = await _folderPicker.PickAsync(cancellationToken);
        //    if (folderPickerResult.IsSuccessful)
        //    {
        //        await Toast.Make($"Folder picked: Name - {folderPickerResult.Folder.Name}, Path - {folderPickerResult.Folder.Path}", ToastDuration.Long).Show(cancellationToken);
        //    }
        //    else
        //    {
        //        await Toast.Make($"Folder is not picked, {folderPickerResult.Exception.Message}").Show(cancellationToken);
        //    }
        //}

        private void SetModel(FolderPickerResult result)
        {
            ProjectViewModel.ProjectFile = _projectViewModelFactory.CreateViewModel(result);
            ProjectViewModel.ProjectFile.IsInit = true;
        }

        private async void OnScrivPickerClicked(object sender, EventArgs e)
        {
            await _fileSelector.SelectAsync();
        }

        private void Folder_OnTapped(object sender, EventArgs e)
        {
            var viewCell = sender as ViewCell;
            var folder = viewCell.BindingContext as Folder;
            Navigation.PushAsync(new FolderPage(folder));
        }

        private void LabelButton_OnClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var projectFile = button.BindingContext as ProjectViewModel;
            Navigation.PushAsync(new LabelPage(projectFile.ProjectFile.Labels));
        }
    }
}