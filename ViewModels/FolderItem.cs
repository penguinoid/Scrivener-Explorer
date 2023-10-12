namespace ScrivenerExplorer.ViewModels
{
    public class FolderItem
    {
        public string Title { get; set; }
        public string Filename { get; set; }
        public Color LabelColor { get; set; }
        public string Synopsis { get; set; }
        public string Notes { get; set; }
        public string Section { get; set; }
        public bool IsSectionVisible { get; set; }
        public bool IsSynopsisVisible { get; set; }
        public bool IsNotesVisible { get; set; }
        public string SectionFileName => $"{Filename}.rtf";
        public string SynopsisFileName => $"{Filename}_synopsis.txt";
        public string NotesFileName => $"{Filename}_notes.rtf";
    }
}