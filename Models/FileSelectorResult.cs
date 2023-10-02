namespace ScrivenerExplorer.Models
{
    public class FileSelectorResult
    {
        public bool HasFile { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string Title => FileName.Split('.')[0];
    }
}
