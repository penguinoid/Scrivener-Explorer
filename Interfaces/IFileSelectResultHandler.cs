namespace ScrivenerExplorer.Interfaces
{
    public interface IFileSelectResultHandler
    {
        public void SetHandler(EventHandler<string?> onFileSelectedDelegate);
    }
}
