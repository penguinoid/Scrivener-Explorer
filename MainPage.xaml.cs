using PortableStorage;
using Scrivener;
using Scrivener.ViewModels;
using ScrivenerExplorer.Interfaces;
using ScrivenerExplorer.Models;
using ScrivenerExplorer.ViewModels;

namespace ScrivenerExplorer
{
    public partial class MainPage : ContentPage
    {
        private readonly IFolderSelector _folderSelector;
        private readonly IFolderSelectResultHandler _folderSelectResultHandler;
        private readonly IProjectViewModelFactory _projectViewModelFactory;
        private readonly IStorageRepository _storageRepository;

        public ProjectViewModel ProjectViewModel { get; set; }

        public MainPage(
            IFolderSelector folderSelector,
            IFolderSelectResultHandler folderSelectResultHandler,
            IProjectViewModelFactory projectViewModelFactory,
            IStorageRepository storageRepository)
        {
            _projectViewModelFactory = projectViewModelFactory;
            _storageRepository = storageRepository;
            _folderSelector = folderSelector;
            _folderSelectResultHandler = folderSelectResultHandler;

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