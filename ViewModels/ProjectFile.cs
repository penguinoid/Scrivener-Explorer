using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ScrivenerExplorer.ViewModels
{
    public class ProjectFile : INotifyPropertyChanged
    {
        public string Title { get; set; }
        public List<Folder> Folders { get; set; }
        public List<FolderLabel> Labels { get; set; }

        private bool _isInit;
        public bool IsInit
        {
            get => _isInit;
            set
            {
                if (value == _isInit) return;
                _isInit = value;
                OnPropertyChanged();
            }
        }

        public ProjectFile()
        {
            Folders = new List<Folder>();
            Labels = new List<FolderLabel>();
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
}
