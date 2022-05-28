namespace Virgil.FolderLink.Dropbox.Handler
{
    using System.Collections.Generic;
    using System.Linq;
    using Core;
    using Core.Events;
    using Core.Operations;
    using Local;
    using Local.Operations;
    using Operations;
    using Server;

    public class OperationsFactory
    {
        private readonly ICloudStorage cloudStorage;
        private readonly LocalFolderRoot localFolderRoot;
        private readonly LocalFolder localFolder;

        public OperationsFactory(ICloudStorage cloudStorage, LocalFolder localFolder)
        {
            this.cloudStorage = cloudStorage;
            this.localFolderRoot = localFolder.Root;
            this.localFolder = localFolder;
        }

        public Operation CreateOperation(DropBoxFileAddedEvent @event)
        {
            return new DownloadFileFromServer(@event, this.cloudStorage, this.