namespace Virgil.FolderLink.Dropbox.Handler
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Core;
    using Core.Operations;
    using Core.Events;
    using Infrastructure.Messaging;
    using Local;
    using Messages;
    using Operations;
    using Server;

    public class DropBoxLink : IServerEventListener, ILocalEventListener, IDisposable
    {
        private readonly IEventAggregator aggregator;
        private readonly ICloudStorage cloudStorage;
        private readonly LocalFolder localFolder;
        private readonly ServerFolder serverFolder;

        private readonly OperationsFactory operationsFactory;
        private readonly LocalFolderWatcher localFolderWatcher;
        private readonly DropboxFolderWatcher serverFolderWatcher;

        private readonly ConcurrentQueue<Operation> operations = new ConcurrentQueue<Operation>();
        private readonly CancellationTokenSource cts = new CancellationTokenSource();

        public ObservableCollection<Operation> OperationsView = new ObservableCollection<Operation>();
        
        public DropBoxLink(DropBoxLinkParams dropBoxLinkParams, IEventAggregator aggregator)
        {
            this.aggregator = aggregator;
            var client = new DropboxClientFactory(dropBoxLinkParams.AccessToken).GetInstance();
            var localFolderRoot = new LocalFolderRoot(dropBoxLinkParams.LocalFolderPath);

            this.cloudStorage = new DropBoxCloudStorage(client, dropBoxLinkParams.Card, dropBoxLinkParams.PrivateKeyPassword);
            this.localFolder = new LocalFolder(localFolderRoot, "Source");
            this.localFolderWatcher = new LocalFolderWatcher(this.localFolder);
            this.serverFolder = new ServerFolder();
            this.serverFolderWatcher = new DropboxFolderWatcher(client, this.serverFolder);
            this.operationsFactory = new OperationsFactory(this.cloudStorage, this.localFolder);

            this.serverFolder.Subscribe(this);
            this.localFolder.Subscribe(this);
        }

        public async Task Launch()
        {
            this.localFolderWatcher.Start();
            await this.serverFolderWatcher.Start();


			var localFiles = this.localFold