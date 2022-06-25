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
                this.settings = JsonConvert.DeserializeObject<PerUserFolderSettings>(this.storageProvider.Load(FilePath))
                            ?? new PerUserFolderSettings();
            }
            catch (Exception)
            {
                this.settings = new PerUserFolderSettings();
            }
        }

        public FolderSettings FolderSettings
        {
            get
            {
                if (!this.appState.HasAccount)
                {
                    throw new InvalidOperationException("User not logged in");
                }

                this.recipientId = this.appState.CurrentCard.Identity.Value.ToLowerInvariant();

                if (!this.settings.ContainsKey(this.recipientId))
                {
                    this.settings[this.recipientId] = new FolderSettings();
                    this.Save();
                }

                return this.settings[this.recipientId];
            }

            private set
            {
                this.settings[this.recipientId] = value;
            }
        }

        public void SetLocalFoldersSettings(Folder source, IEnumerable<Folder> targets)
        {
            this.FolderSettings.SourceFolder = source;
            this.FolderSettings.TargetFolders = targets.ToList();
            this.Save();
        }

        public v