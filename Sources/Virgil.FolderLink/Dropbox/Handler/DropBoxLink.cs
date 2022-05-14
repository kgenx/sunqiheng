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
        private readonly Can