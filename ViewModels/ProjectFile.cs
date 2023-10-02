namespace ScrivenerExplorer.ViewModels
{
    public class ProjectFile
    {
        public string Title { get; set; }
        public List<Folder> Folders { get; set; }

        public ProjectFile()
        {
            Folders = new List<Folder>();
        }
    }
}
