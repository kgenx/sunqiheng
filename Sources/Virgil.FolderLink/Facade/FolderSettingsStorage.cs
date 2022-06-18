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
        private readonly IStorageProvider storageProvid