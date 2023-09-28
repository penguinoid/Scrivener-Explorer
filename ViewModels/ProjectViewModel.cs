using Scrivener.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Scrivener.ViewModels;

public class ProjectViewModel : INotifyPropertyChanged
{
    private ProjectFile _projectFile;

    public ProjectFile ProjectFile
    {
        get
        {
            return _projectFile;
        }
        set
        {
            _projectFile = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}