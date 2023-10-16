using ScrivenerExplorer.Interfaces;
using PortableStorage;

namespace ScrivenerExplorer.Repositories
{
    public class StorageRepository : IStorageRepository
    {
        private static Dictionary<string, StorageEntry> _storageEntries;

        public StorageRepository()
        {
            Init();
        }

        public void Initialize()
        {
            Init();
        }

        public StorageEntry GetStorageEntry(string key)
        {
            if (_storageEntries.TryGetValue(key, out var entry))
            {
                return entry;
            }

            return null;
        }

        public bool FileExists(string key)
        {
            return _storageEntries.ContainsKey(key);
        }

        public void AddStorageEntry(string key, StorageEntry entry)
        {
            if (_storageEntries.ContainsKey(key))
            {
                return;
            }

            _storageEntries.Add(key, entry);
        }

        private void Init()
        {
            _storageEntries = new Dictionary<string, StorageEntry>();
        }
    }
}
