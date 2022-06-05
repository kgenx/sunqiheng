namespace Virgil.FolderLink.Dropbox.Server
{
    using System;
    using Core;
    using global::Dropbox.Api.Files;

    public class ServerFile
    {
        public ServerPath Path { get; set; }
        public long Bytes { get; set; }
        public DateTime ServerModified { get; set; }
        public DateTime ClientModified { get; set; }
        
        public ServerFile(FileMetadata entry)
        {
            this.Path = ServerPath.FromServerPath(entry.PathLower);
            this.Bytes = (long) entry.Size;
            this.ServerModified = entry.ServerModified;
            this.ClientModified = entry.ClientModified;
        }

        public override