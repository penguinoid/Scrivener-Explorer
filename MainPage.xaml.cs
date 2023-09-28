using Scrivener.Models;
using Scrivener.ViewModels;
using Scrivener;
using System.Xml.Linq;
using System.Xml.XPath;

namespace ScrivenerExplorer
{
    public partial class MainPage : ContentPage
    {
        public ProjectViewModel ProjectViewModel { get; set; }

        public MainPage()
        {
            InitializeComponent();
            ProjectViewModel = new ProjectViewModel();
            BindingContext = ProjectViewModel;
        }

        private async void OnScrivPickerClicked(object sender, EventArgs e)
        {
            FileResult result = await FilePicker.PickAsync();
            if (result != null)
            {
                if (result.FileName.EndsWith("scrivx", StringComparison.OrdinalIgnoreCase))
                {
                    await using var stream = await result.OpenReadAsync();
                    var projectXml = XElement.Load(stream);

                    var projectFile = new ProjectFile
                    {
                        Title = result.FileName
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
                                RootPath = result.FullPath.Replace(result.FileName, string.Empty)
                            };
                            folder.Items.Add(folderItem);
                        }
                        projectFile.Folders.Add(folder);
                    }

                    ProjectViewModel.ProjectFile = projectFile;
                }
            }

        }

        private void Folder_OnTapped(object sender, EventArgs e)
        {
            var viewCell = sender as ViewCell;
            var folder = viewCell.BindingContext as Folder;
            Navigation.PushAsync(new FolderPage(folder));
        }
    }
}