
namespace Virgil.FolderLink.Facade
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Dropbox.Messages;
    using FolderLink.Dropbox.Handler;
    using FolderLink.Local;
    using Infrastructure.Messaging;
    using Infrastructure.Messaging.Application;

    public class FolderLinkFacade : IHandle<Logout>, IHandle<DropboxSessionExpired>, IHandle<BeforeLogout>
    {
        private static readonly object m_lock = new object();
        private readonly ApplicationState applicationState;
        private readonly IEventAggregator eventAggregator;
        private readonly FolderSettingsStorage folderSettingsStorage;

        private readonly List<LocalFolderLink> links = new List<LocalFolderLink>();
        private DropBoxLink dropBoxLink;

        public FolderLinkFacade(
            ApplicationState applicationState,
            FolderSettingsStorage folderSettingsStorage,
            IEventAggregator eventAggregator)
        {
            this.applicationState = applicationState;
            this.folderSettingsStorage = folderSettingsStorage;
            this.eventAggregator = eventAggregator;
            eventAggregator.Subscribe(this);
        }

        public void Rebuild()
        {
            lock (m_lock)
            {
                this.CleanupAndStop();

                this.eventAggregator.Publish(new DropBoxLinkChanged());

                var folderSettings = this.folderSettingsStorage.FolderSettings;