using Scrivener.Models;

namespace Scrivener;

public partial class FolderItemPage : ContentPage
{
	public FolderItem FolderItem { get; set; }

	public FolderItemPage(FolderItem folderItem)
    {
        InitializeComponent();
        FolderItem = folderItem;
        
        if (File.Exists(folderItem.SectionFilePath))
        {
            FolderItem.IsSectionVisible = true;
        }

        var synopsisResult = LoadFile(folderItem.SynopsisFilePath).Result;
        if (synopsisResult.IsSuccess)
        {
            FolderItem.IsSynopsisVisible = true;
            FolderItem.Synopsis = synopsisResult.Text;
        }
        
        if (File.Exists(folderItem.NotesFilePath))
        {
            FolderItem.IsNotesVisible = true;
        }

        BindingContext = FolderItem;
    }

    private async Task<TextResult> LoadFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            await using var fileStream = File.OpenRead(filePath);
            using var reader = new StreamReader(fileStream);
            var text = await reader.ReadToEndAsync().ConfigureAwait(continueOnCapturedContext: false);
            return new TextResult
            {
                IsSuccess = true,
                Text = text
            };
        }

        return new TextResult();
    }

    private string GetFilePath(string path, string fileName)
    {
        return Path.Combine(Path.GetFullPath(path), "Files", "Docs", fileName);
    }

    private async void SectionButton_OnClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var folderItem = button.BindingContext as FolderItem;
        await Launcher.Default.OpenAsync(new OpenFileRequest(folderItem.Title, new ReadOnlyFile(folderItem.SectionFilePath)));
    }

    private async void SynopsisButton_OnClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var folderItem = button.BindingContext as FolderItem;
        await Launcher.Default.OpenAsync(new OpenFileRequest(folderItem.Title, new ReadOnlyFile(folderItem.SynopsisFilePath)));
    }

    private async void NotesButton_OnClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var folderItem = button.BindingContext as FolderItem;
        await Launcher.Default.OpenAsync(new OpenFileRequest(folderItem.Title, new ReadOnlyFile(folderItem.NotesFilePath)));
    }
}