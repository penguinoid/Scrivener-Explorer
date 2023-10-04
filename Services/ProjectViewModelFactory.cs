using ScrivenerExplorer.Interfaces;
using ScrivenerExplorer.Models;
using ScrivenerExplorer.ViewModels;
using System.Xml.Linq;
using System.Xml.XPath;
using ScrivenerExplorer.Extensions;

namespace ScrivenerExplorer.Services
{
    public class ProjectViewModelFactory : IProjectViewModelFactory
    {
        public ProjectFile CreateViewModel(FileSelectorResult result)
        {
            if (result.FileName.EndsWith("scrivx", StringComparison.OrdinalIgnoreCase))
            {
                var projectXml = XElement.Load(result.FilePath);

                var projectFile = new ProjectFile
                {
                    Title = result.Title
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
                    var binderItemsXml = folderXml.XPathSelectElements("Children/BinderItem");
                    foreach (var binderItemXml in binderItemsXml)
                    {
                        var folderItem = new FolderItem
                        {
                            Title = binderItemXml.Element("Title")?.Value,
                            LabelColor = GetColorFromLabel(binderItemXml.XPathSelectElement("MetaData/LabelID")?.Value, projectFile.Labels),
                            Filename = binderItemXml.Attribute("ID")?.Value,
                            RootPath = result.FilePath.Replace(result.FileName, string.Empty)
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
