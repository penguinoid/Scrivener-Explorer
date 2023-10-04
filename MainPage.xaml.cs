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

        public ProjectViewModel ProjectViewModel { get; set; }

        public MainPage(IFileSelector fileSelector, 
            IFileSelectResultHandler fileSelectResultHandler, 
            IProjectViewModelFactory projectViewModelFactory)
        {
            _fileSelector = fileSelector;
            _fileSelectResultHandler = fileSelectResultHandler;
            _projectViewModelFactory = projectViewModelFactory;

            InitializeComponent();
            InitFileSelectResultHandler();

            ProjectViewModel = new ProjectViewModel
            {
                ProjectFile = new ProjectFile()
            };

            BindingContext = ProjectViewModel;
        }

        private void InitFileSelectResultHandler()
        {
            _fileSelectResultHandler.SetHandler(async (sender, result) =>
            {
                if (!result.HasFile)
                {
                    return;
                }

                SetModel(result);
            });
        }

        private void SetModel(FileSelectorResult result)
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