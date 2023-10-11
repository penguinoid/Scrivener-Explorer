using PortableStorage;

namespace ScrivenerExplorer.Interfaces
{
    public interface IStorageRepository
    {
        StorageEntry GetStorageEntry(string key);
        bool FileExists(string key);
        void AddStorageEntry(string key, StorageEntry entry);
    }
}
