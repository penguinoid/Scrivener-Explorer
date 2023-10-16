using ScrivenerExplorer.Extensions;
using ScrivenerExplorer.Interfaces;
using ScrivenerExplorer.Models;
using ScrivenerExplorer.ViewModels;
using System.Xml.Linq;
using System.Xml.XPath;

namespace ScrivenerExplorer.Services
{
    public class ProjectViewModelFactory : IProjectViewModelFactory
    {
        public ProjectFile CreateViewModel(FolderSelectorResult result)
        {
            var projectEntry = result.StorageRoot.Entries.ToList().FirstOrDefault(x => x.Name.EndsWith(".scrivx"));
            if (projectEntry == null)
            {
                return null;
            }

            var projectText = projectEntry.ReadAllText();
            var projectXml = XElement.Parse(projectText);

            var projectFile = new ProjectFile
            {
                Title = projectEntry.Name.Replace(".scrivx", string.Empty)
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
                        Filename = binderItemXml.Attribute("ID")?.Value
                    };
                    folder.Items.Add(folderItem);
                }
                projectFile.Folders.Add(folder);
            }

            return projectFile;
        }

        private Color GetColorFromLabel(string labelId, List<FolderLabel> labels)
        {
            return labels.FirstOrDefault(x => x.Id == labelId)?.Color;
        }
    }
}
