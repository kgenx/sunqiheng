namespace Virgil.Disk.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Dropbox.Api;
    using Infrastructure;
    using Newtonsoft.Json;

    public class DropboxCredentials
    {
        public DropboxCredentials()
        {
        }

        public DropboxCredentials(OAuth2Response oauth)
        {
            this.AccessToken = oauth.AccessToken;
            this.UserId = oauth.Uid;
        }

        [JsonProperty]
        public string AccessToken { get; set; }

        [JsonProperty]
        public string UserId { get; set; }

        public bool IsEmpty()
        {
            return string.IsNullOrEmpty(this.AccessToken) && string.IsNullOrWhiteSpace(this.UserId);
        }
    }

    public class PerUserFolderSettings : Dictionary<string, FolderSettings>
    {
    }

    public class FolderSettings
    {
        public FolderSettings()
        {
            this.TargetFolders = new List<Folder>();
            this.SourceFolder = new Folder();
            this.DropboxCredentials = new DropboxCredentials();
        }

        [JsonProperty]
        public List<Folder> TargetFolders { get; set; } 

        [JsonProperty]
        public Folder SourceFolder { get; set; }

        [JsonProperty]
        public DropboxCredentials DropboxCredentials { get; set;  }

        public ValidationErrors Validate()
        {
