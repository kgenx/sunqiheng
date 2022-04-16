namespace Virgil.DropBox.Client.FileSystem
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reactive.Linq;

    public class FolderWatcher
    {
        private readonly Folder folder;
        private readonly FileSystemWatcher fileSystemWatcher;

        public FolderWatcher(Folder folder)
        {
            this.folder = folder;
            this.fileSystemWatcher = new FileSystemWatcher(folder.FolderPath)
            {
                IncludeSubdirectories = true,
                InternalBufferSize = 1024 * 64,
                NotifyFilter =
                    NotifyFilters.CreationTime |
                    NotifyFilters.DirectoryName |
                    NotifyFilters.FileName |
                    NotifyFilters.LastWrite |
                    NotifyFilters.Size |
                    NotifyFilters.Attributes
            };
        }
        
        public void Start()
        {
            var created = Observable.FromEventPattern<FileSystemEventArgs>(this.fileSystemWatcher, "Created");
            var changed = Observable.FromEventPattern<FileSystemEventArgs>(this.fileSystemWatcher, "Changed");

            var merged = created.Merge(changed)
                .Buffer(TimeSpan.FromSeconds(2))
                .Where(it => it.Count > 0)
                .Select(eventPatterns =>
                {
                    return eventPatterns
                    .GroupBy(x