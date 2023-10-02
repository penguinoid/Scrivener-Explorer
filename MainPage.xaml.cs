using Scrivener;
using Scrivener.Models;
using Scrivener.ViewModels;
using ScrivenerExplorer.Interfaces;
using ScrivenerExplorer.Models;
using ScrivenerExplorer.ViewModels;
using System.Xml.Linq;
using System.Xml.XPath;

namespace ScrivenerExplorer
{
    public partial class MainPage : ContentPage
    {
        private readonly IFileSelector _fileSelector;
        private readonly IFileSelectResultHandler _fileSelectResultHandler;

        public ProjectViewModel ProjectViewModel { get; set; }

        public MainPage(IFileSelector fileSelector, IFileSelectResultHandler fileSelectResultHandler)
        {
            _fileSelector = fileSelector;
            _fileSelectResultHandler = fileSelectResultHandler;

            InitializeComponent();
            InitFileSelectResultHandler();

            ProjectViewModel = new ProjectViewModel();
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
            if (result.HasFile)
            {
                if (result.FileName.EndsWith("scrivx", StringComparison.OrdinalIgnoreCase))
                {
                    //await using var stream = await result.OpenReadAsync();
                    var projectXml = XElement.Load(result.FilePath);

                    var projectFile = new ProjectFile
                    {
                        Title = result.Title
                    };

                    var foldersXml = projectXml.XPathSelectElements("Binder/BinderItem");
                    foreach (var folderXml in foldersXml)
                    {
                        var folder = new Folder
                        {
                            Id = folderXml.Attribute("ID")?.Value,
                            Title = folderXml.Element("Title")?.Value,
                            Items = new List<FolderItem>()
                        };
                        var binderItemsXml = folderXml.XPathSelectElements("Children/BinderItem");
                        foreach (var binderItemXml in binderItemsXml)
                        {
                            var folderItem = new FolderItem
                            {
                                Title = binderItemXml.Element("Title")?.Value,
                                Label = binderItemXml.XPathSelectElement("MetaData/LabelID")?.Value,
                                Filename = binderItemXml.Attribute("ID")?.Value,
                                RootPath = result.FilePath.Replace(result.FileName, string.Empty)
                            };
                            folder.Items.Add(folderItem);
                        }
                        projectFile.Folders.Add(folder);
                    }

                    ProjectViewModel.ProjectFile = projectFile;
                }
            }
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
    }
}