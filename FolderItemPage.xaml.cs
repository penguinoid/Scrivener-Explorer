using ScrivenerExplorer.Interfaces;
using ScrivenerExplorer.ViewModels;

namespace Scrivener;

public partial class FolderItemPage : ContentPage
{
    public FolderItem FolderItem { get; set; }

	public FolderItemPage(FolderItem folderItem, IStorageRepository storageRepository)
    {
        InitializeComponent();
        FolderItem = folderItem;

        var synopsis = storageRepository.GetStorageEntry(folderItem.SynopsisFileName);
        if (synopsis != null)
        {
            FolderItem.IsSynopsisVisible = true;
            FolderItem.Synopsis = synopsis.ReadAllText();
            if (!string.IsNullOrWhiteSpace(FolderItem.Synopsis))
            {
                FolderItem.IsSynopsisVisible = true;
            }
        }
        
        if (storageRepository.FileExists(folderItem.NotesFileName))
        {
            var notes = storageRepository.GetStorageEntry(folderItem.NotesFileName);
            FolderItem.Notes = Dangl.TextConverter.Rtf.RtfToText.ConvertRtfToText(notes.ReadAllText());
            if (!string.IsNullOrWhiteSpace(FolderItem.Notes))
            {
                FolderItem.IsNotesVisible = true;
            }
        }

        if (storageRepository.FileExists(folderItem.SectionFileName))
        {
            var section = storageRepository.GetStorageEntry(folderItem.SectionFileName);
            FolderItem.Section = Dangl.TextConverter.Rtf.RtfToText.ConvertRtfToText(section.ReadAllText());
            if (!string.IsNullOrWhiteSpace(FolderItem.Section))
            {
                FolderItem.IsSectionVisible = true;
            }
        }

        BindingContext = FolderItem;
    }
}