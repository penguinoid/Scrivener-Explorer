using ScrivenerExplorer.ViewModels;

namespace ScrivenerExplorer;

public partial class LabelPage : ContentPage
{
	public LabelPage(List<FolderLabel> labels)
	{
		InitializeComponent();
        BindingContext = labels;
    }
}