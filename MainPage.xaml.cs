using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Storage;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls.PlatformConfiguration;
using PortableStorage;
using Scrivener;
using Scrivener.ViewModels;
using ScrivenerExplorer.Interfaces;
using ScrivenerExplorer.Models;
using ScrivenerExplorer.ViewModels;
using static Android.Media.Audiofx.DynamicsProcessing;

namespace ScrivenerExplorer
{
    public partial class MainPage : ContentPage
    {
        private readonly IFileSelector _fileSelector;
        private readonly IFileSelectResultHandler _fileSelectResultHandler;
        private readonly IFolderSelector _folderSelector;
        private readonly IFolderSelectResultHandler _folderSelectResultHandler;
        private readonly IProjectViewModelFactory _projectViewModelFactory;
        private readonly IStorageRepository _storageRepository;
        private readonly IFolderPicker _folderPicker;

        public ProjectViewModel ProjectViewModel { get; set; }

        public MainPage(IFileSelector fileSelector, 
            IFileSelectResultHandler fileSelectResultHandler,
            IFolderSelector folderSelector,
            IFolderSelectResultHandler folderSelectResultHandler,
            IProjectViewModelFactory projectViewModelFactory,
            IStorageRepository storageRepository,
            IFolderPicker folderPicker)
        {
            _fileSelector = fileSelector;
            _fileSelectResultHandler = fileSelectResultHandler;
            _projectViewModelFactory = projectViewModelFactory;
            _storageRepository = storageRepository;
            _folderSelector = folderSelector;
            _folderSelectResultHandler = folderSelectResultHandler;
            _folderPicker = folderPicker;

            InitializeComponent();
            InitFolderSelectResultHandler();

            ProjectViewModel = new ProjectViewModel
            {
                ProjectFile = new ProjectFile()
            };

            BindingContext = ProjectViewModel;
        }

        private void InitFolderSelectResultHandler()
        {
            _folderSelectResultHandler.SetHandler(async (sender, result) =>
            {
                if (!result.HasData)
                {
                    return;
                }

                SetModel(result);
            });
        }

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

        //public async void OnFolderPickerClicked(object sender, EventArgs args)
        //{
        //    var cancellationToken = new CancellationToken(false);
        //    var result = await _folderPicker.PickAsync(cancellationToken);
        //    result.EnsureSuccess();
        //    SetModel(result);
        //    //await Toast.Make($"Folder picked: Name - {result.Folder.Name}, Path - {result.Folder.Path}", ToastDuration.Long).Show(cancellationToken);
        //}

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

        private void SetModel(FolderSelectorResult result)
        {
            PopulateStorageRepository(result.StorageRoot);
            ProjectViewModel.ProjectFile = _projectViewModelFactory.CreateViewModel(result);
            ProjectViewModel.ProjectFile.IsInit = true;
        }

        private async void OnScrivPickerClicked(object sender, EventArgs e)
        {
            await _folderSelector.SelectAsync();
        }

        private void Folder_OnTapped(object sender, EventArgs e)
        {
            var viewCell = sender as ViewCell;
            var folder = viewCell.BindingContext as Folder;
            Navigation.PushAsync(new FolderPage(folder, _storageRepository));
        }

        private void LabelButton_OnClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var projectFile = button.BindingContext as ProjectViewModel;
            Navigation.PushAsync(new LabelPage(projectFile.ProjectFile.Labels));
        }

        //private void SafStoragePickerClicked(object sender, EventArgs e)
        //{
        //    SafStorageHelper.BrowserFolder(Platform.CurrentActivity, 100);
        //}

        private void PopulateStorageRepository(StorageRoot storageRoot)
        {
            var folders = storageRoot.Entries;
            foreach (var folder in folders)
            {
                if (folder.Name == "Files")
                {
                    var files = folder.OpenStorage();
                    foreach (var file in files.Entries)
                    {
                        if (file.Name == "Docs")
                        {
                            var docs = file.OpenStorage();
                            foreach (var doc in docs.Entries)
                            {
                                _storageRepository.AddStorageEntry(doc.Name, doc);
                            }
                        }
                    }
                }
            }
        }
    }
}