namespace Scrivener.Models
{
    public class FolderItem
    {
        public string Title { get; set; }
        public string Filename { get; set; }
        public string Label { get; set; }
        public string Synopsis { get; set; }
        public bool IsSectionVisible { get; set; }
        public bool IsSynopsisVisible { get; set; }
        public bool IsNotesVisible { get; set; }
        public string SectionFilePath => GetFilePath(RootPath, $"{Filename}.rtf");
        public string SynopsisFilePath => GetFilePath(RootPath, $"{Filename}_synopsis.txt");
        public string NotesFilePath => GetFilePath(RootPath, $"{Filename}_notes.rtf");
        public string RootPath { get; set; }

        private string GetFilePath(string path, string fileName)
        {
            return Path.Combine(Path.GetFullPath(path), "Files", "Docs", fileName);
        }
    }
}