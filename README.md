# Scrivener Explorer
This is a file explorer for Scrivener that reads its project file and provides navigation to its text files.
## Background
Since an Android version of Scrivener is never likely to be a thing, I wrote this for myself to be able to at least access my projects while out and about. It's useful to be able to read my notes even if it doesn't allow direct editing.
## What it does
It reads a .scrix project file and allows the user to browse through the folder heirarchy and read chapters, synopses and notes.
To this end it consists of the following pages:
- The main page which allows you to select the folder containing the .scrivx file to open.
- A list of folders in your project. This are typically Manuscript, Characters etc but it will read whatever they've been called.
- A list of chapters / sections in that folder. This is a flattened view of the tree structure for simplicity's sake. They are colour coded by label if present.
- A list of labels and their colours for reference.
- The synopsis of a selected chapter, along with any notes and its text.
## What it doesn't do
It doesn't let you edit anything. This is by design. I don't want to edit my .scrix file and mess something up, and I don't want to run into synchronisation issues with the text.
## Notes on use
When opening a folder in the app, you will be prompted to select the project's folder. This will be a folder with a name ending in .scriv. It will contain a .scrivx file - this is the project file. It will also contain some folders, such as Files and Settings. You will be prompted to give permission to access te selected folder. Click 'yes'. This is required as the app needs to be able to read the contents of the folder.
## Cloud storage
Unfortunately neither OneDrive nor Google Drive have implemented Androids Storage Access Framework for folder browsing, which is frustrating. The upshot of this is that this app requires your project folder to be physically on your device. You could achieve this by manually copying your updated project as an when is convenient, but fortunately there is a simpler, more automated way. You can use a third-party synchronisation app which can sync a folder in cloud storage with a folder on your device. I've used FolderSync, which has a free and paid version. The free version is good enough for what is needed here although it does contain ads, and the paid version isn't very expensive considering the huge number of providers its supports.

Whatever you use for syncing, it's important that you set the folders up for one-way syncing only. This ensures that should anything on the device be deleted, the deletion isn't synced back to your source in the cloud.

