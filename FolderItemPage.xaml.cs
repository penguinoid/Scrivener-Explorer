using System.Web;
using Android.OS;
using Scrivener.Models;
using ScrivenerExplorer.Interfaces;
using ScrivenerExplorer.ViewModels;
using static System.Collections.Specialized.BitVector32;

namespace Scrivener;

public partial class FolderItemPage : ContentPage
{
    private readonly IStorageRepository _storageRepository;
    public FolderItem FolderItem { get; set; }

	public FolderItemPage(FolderItem folderItem, IStorageRepository storageRepository)
    {
        _storageRepository = storageRepository;
        InitializeComponent();
        FolderItem = folderItem;

        var synopsis = _storageRepository.GetStorageEntry(folderItem.SynopsisFileName);
        if (synopsis != null)
        {
            FolderItem.IsSynopsisVisible = true;
            FolderItem.Synopsis = synopsis.ReadAllText();
        }
        
        if (_storageRepository.FileExists(folderItem.NotesFileName))
        {
            FolderItem.IsNotesVisible = true;
            var notes = _storageRepository.GetStorageEntry(folderItem.NotesFileName);
            FolderItem.Notes = Dangl.TextConverter.Rtf.RtfToText.ConvertRtfToText(notes.ReadAllText());
        }

        if (_storageRepository.FileExists(folderItem.SectionFileName))
        {
            FolderItem.IsSectionVisible = true;
            var section = _storageRepository.GetStorageEntry(folderItem.SectionFileName);
            FolderItem.Section = Dangl.TextConverter.Rtf.RtfToText.ConvertRtfToText(section.ReadAllText());
        }

        BindingContext = FolderItem;
    }

    //private async Task<TextResult> LoadFile(string filePath)
    //{
    //    if (File.Exists(filePath))
    //    {
    //        await using var fileStream = File.OpenRead(filePath);
    //        using var reader = new StreamReader(fileStream);
    //        var text = await reader.ReadToEndAsync().ConfigureAwait(continueOnCapturedContext: false);
    //        return new TextResult
    //        {
    //            IsSuccess = true,
    //            Text = text
    //        };
    //    }

    //    return new TextResult();
    //}

    //private string GetFilePath(string path, string fileName)
    //{
    //    return Path.Combine(Path.GetFullPath(path), "Files", "Docs", fileName);
    //}

    private async void SectionButton_OnClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var folderItem = button.BindingContext as FolderItem;
        var section = _storageRepository.GetStorageEntry(folderItem.SectionFileName);
        var localPath = new ReadOnlyFile(section.Uri.LocalPath);
        var localPathReq = new OpenFileRequest("test", localPath);
        var absolutePath = new ReadOnlyFile(section.Uri.AbsolutePath);
        var absolutePathReq = new OpenFileRequest("test", absolutePath);
        var originalString = new ReadOnlyFile(section.Uri.OriginalString);
        var originalStringReq = new OpenFileRequest("test", originalString);
        var absoluteUri = new ReadOnlyFile(section.Uri.AbsoluteUri);
        var absoluteUriReq = new OpenFileRequest("test", absoluteUri);
        var pathAndQuery = new ReadOnlyFile(section.Uri.PathAndQuery);
        var pathAndQueryReq = new OpenFileRequest("test", pathAndQuery);
        var path = new ReadOnlyFile(section.Path);
        var pathReq = new OpenFileRequest("test", path);
        var name = new ReadOnlyFile(section.Name);
        var nameReq = new OpenFileRequest("test", name);

        var text = Dangl.TextConverter.Rtf.RtfToText.ConvertRtfToText(section.ReadAllText());

        var sectionPath = HttpUtility.UrlDecode(section.Uri.AbsoluteUri);
        //System.Windows.f

        await Launcher.Default.OpenAsync(new OpenFileRequest(folderItem.Title, new ReadOnlyFile(section.Uri.ToString())));
    }

    private async void SynopsisButton_OnClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var folderItem = button.BindingContext as FolderItem;
        await Launcher.Default.OpenAsync(new OpenFileRequest(folderItem.Title, new ReadOnlyFile(folderItem.SynopsisFileName)));
    }

    private async void NotesButton_OnClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var folderItem = button.BindingContext as FolderItem;
        await Launcher.Default.OpenAsync(new OpenFileRequest(folderItem.Title, new ReadOnlyFile(folderItem.NotesFileName)));
    }
}