﻿namespace ScrivenerExplorer.ViewModels
{
    public class Folder
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public List<FolderItem> Items { get; set; }
    }
}
