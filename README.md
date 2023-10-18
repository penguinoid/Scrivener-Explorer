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

