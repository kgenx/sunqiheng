namespace Virgil.DropBox.Client.FileSystem
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Events;

    public class Folder
    {
        private readonly IFileEventListener eventListener;

        public string FolderName { get; }
        
        public List<LocalFile> Files { get; } = new List<LocalFile>();

        public string FolderPath { get; }

        public Folder(string folderPath, string folderName, IFileEventListener eventListener)
        {
            this.FolderName = folderName;
            this.eventListener = eventListener;
            this.FolderPath = folderPath;
        }

        public void Scan()
        {
            var paths = Directory.EnumerateFiles(this.FolderPath, 