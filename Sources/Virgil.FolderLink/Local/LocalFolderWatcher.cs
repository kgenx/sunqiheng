namespace Virgil.FolderLink.Local
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    
    using Core;
    using Dropbox;
    using SDK.Http;

    public class LocalFolderWatcher : IDisposable
    {
        private readonly LocalFolder folder;
        private readonly FileSystemWatcher fileSystemWatcher;
        private bool disposed = false;
        private bool stopped = false;

        public LocalFolderWatcher(LocalFolder folder)
        {
            this.folder = folder;
            this.fileSystemWatcher = new FileSystemWatcher(folder.Root.Value)
            {
                IncludeSubdirectories = true,
                InternalBufferSize = 1024 * 64,
                NotifyFilter = NotifyFilters.FileName |
                                NotifyFilters.DirectoryName |
                                NotifyFilters.Attributes |
                                NotifyFilters.Size |
                                NotifyFilters.LastWrite |
                                NotifyFilters.LastAccess |
                                NotifyFilters.CreationTime



            };
        }

        public struct TimestampedEvent
        {
            public RawFileSystemEvent Event { get; }
            public DateTime DateTime { get; }
            public LocalPath LocalPath { get; set; }

            public TimestampedEvent(FileSystemEventArgs args, LocalFolderRoot root)
            {
                this.Event = new RawFileSystemEvent(args);
                this.DateTime = DateTime.UtcNow;
                this.LocalPath = new LocalPath(args.FullPath, root);
            }
        }
        
        public void Start()
        {
            var deleted = Observable.FromEventPattern<FileSystemEventArgs>(this.fileSystemWatcher, "Deleted")
                .Select(it => new TimestampedEvent(it.EventArgs, this.folder.Root));

            var created = Observable.FromEventPattern<FileSystemEventArgs>(this.fileSystemWatcher, "Created")
                .Select(it => new TimestampedEvent(it.EventArgs, this.folder.Root));

            var changed = Observable.FromEventPattern<FileSystemEventArgs>(this.fileSystemWatcher, "Changed")
                .Select(it => new TimestampedEvent(it.EventArgs, this.folder.Root));

            var renames = Observable.FromEventPattern<RenamedEventArgs>(this.fileSystemWatcher, "Renamed")
                .Select(it => new TimestampedEvent(it.EventArgs, this.folder.Root));

                //{
                //    it => 

                //    var fullPath = it.EventArgs.FullPath;
                //    var oldFullPath = it.EventArgs.OldFullPath;

                //    var isDirectory = IsDirectory(fullPath);


                //    return new[]
                //    {
                //        new FileSystemEventArgs(
                //            WatcherChangeTypes.Deleted,
                //            Path.GetDirectoryName(oldFullPath),
                //            Path.GetFileName(oldFullPath)),
                //        new FileSystemEventArgs(
                //            WatcherChangeTypes.Created,
                //            Path.GetDirectoryName(fullPath),
                //            Path.GetFileName(fullPath)),
                //        it.EventArgs
                //    };
                    
                //})
                //.SelectMany(it => it)
                //.Select(it => new TimestampedEvent(it, this.folder.Root));


            var poll = created.Merge(changed).Merge(renames).Merge(deleted)
                .Where(it => FileNameRules.FileNameValid(it.Event.FullPath))
                .Collect(
                    () => new List<TimestampedEvent>(),
                    (list, x) =>
                    {
                        list.Add(x);
                        return list;
                    },
                    list => new List<TimestampedEvent>()
             