namespace Virgil.FolderLink.Facade
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Infrastructure;
    using Infrastructure.Messaging;
    using Infrastructure.Messaging.Application;
    using Newtonsoft.Json;

    public class FolderSettingsStorage
    {
        private const string FilePath = "VirgilSecurity/folderSettings";
        private readonly IStorageProvider storageProvider;
        private readonly ApplicationState appState;
        private readonly IEventAggregator aggregator;
        private PerUserFolderSettings settings;
        private string recipientId;

        public FolderSettingsStorage(IStorageProvider storageProvider, ApplicationState appState, IEventAggregator aggregator)
        {
            this.storageProvider = storageProvider;
            this.appState = appState;
            this.aggregator = aggregator;

            this.LoadInternal();
        }

        private void LoadInternal()
        {
            try
            {
                this.settings = JsonConvert.DeserializeObject<PerUserFolderSe