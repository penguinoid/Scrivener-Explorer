# Scrivener Explorer
This is a file explorer for Scrivener that reads its project file and provides navigation to its text files.
## Background
Since an Android version of Scrivener is ever likely to be a thing, I wrote this for myself to be able to at least access my projects while out and about. It's useful to be able to read my notes even if it doesn't allow direct editing.
## What this does
It reads a .scrix project file and allows the user to browse through the folder heirarchy and read chapters, synopses and notes. Reading chapters and notes requires an external app capable of reading RTF files, eg Word. The files are opened read-only but copies can be saved.
To this end it consists of the following pages:
- The main page which aloows you to select the .scrivx file to open.
- A list of folders in your project
## What it doesn't do
It doesn't let you edit anything. This is mostly by design. I don't want to edit my .scrix file and mess something up. Even more than that, I don't want you to mess up YOUR .scrix file! There are also a lot of restrictions as to how Android can acceess files outside an application and these have forced my hand during development. I had originally planned on allowing the actual text files (.rtf for chapters and notes, .txt for synopses) to be edited, but for various reasons they will not open externally with write permissions. This is now a somewhat moot point. As you will see from the setup notes, I don't intend to use this directly with my Scrivener project any more, and I don't advise you to either.
