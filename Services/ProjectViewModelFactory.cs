using CommunityToolkit.Maui.Storage;
using ScrivenerExplorer.Extensions;
using ScrivenerExplorer.Interfaces;
using ScrivenerExplorer.ViewModels;
using System.Xml.Linq;
using System.Xml.XPath;

namespace ScrivenerExplorer.Services
{
    public class ProjectViewModelFactory : IProjectViewModelFactory
    {
        public ProjectFile CreateViewModel(FolderPickerResult result)
        {
            if (result.Folder.Name.EndsWith("scriv", StringComparison.OrdinalIgnoreCase))
            {
                var filePath = Path.Combine(result.Folder.Path, result.Folder.Name) + "x";
                var projectXml = XElement.Load(filePath);

                var projectFile = new ProjectFile
                {
                    Title = result.Folder.Name.Replace(".scriv", string.Empty)
                };

                var labelsXml = projectXml.XPathSelectElements("LabelSettings/Labels/Label");
                foreach (var labelXml in labelsXml)
                {
                    var label = new FolderLabel
                    {
                        Id = labelXml.Attribute("ID")?.Value,
                        Title = labelXml.Value,
                        Color = labelXml.Attribute("Color")?.Value.ToColor()
                    };
                    projectFile.Labels.Add(label);
                }

                var foldersXml = projectXml.XPathSelectElements("Binder/BinderItem");
                foreach (var folderXml in foldersXml)
                {
                    var folder = new Folder
                    {
                        Id = folderXml.Attribute("ID")?.Value,
                        Title = folderXml.Element("Title")?.Value,
                        Items = new List<FolderItem>()
                    };
                    var binderItemsXml = folderXml.Descendants("BinderItem");
                    foreach (var binderItemXml in binderItemsXml)
                    {
                        var folderItem = new FolderItem
                        {
                            Title = binderItemXml.Element("Title")?.Value,
                            LabelColor = GetColorFromLabel(binderItemXml.XPathSelectElement("MetaData/LabelID")?.Value, projectFile.Labels),
                            Filename = binderItemXml.Attribute("ID")?.Value,
                            RootPath = result.Folder.Path
                        };
                        folder.Items.Add(folderItem);
                    }
                    projectFile.Folders.Add(folder);
                }

                return projectFile;
            }

            return null;
        }

        private Color GetColorFromLabel(string labelId, List<FolderLabel> labels)
        {
            return labels.FirstOrDefault(x => x.Id == labelId)?.Color;
        }
    }
}
