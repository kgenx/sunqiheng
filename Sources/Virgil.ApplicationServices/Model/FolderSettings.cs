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
   