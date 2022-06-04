namespace Virgil.FolderLink.Dropbox.Operations
{
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using Core;
    using Core.Operations;
    using FolderLink.Core.Events;

    public class DownloadFileFromServer : Operation
    {
        private readonly ICloudStorage cloudStorage;
        private readonly LocalFolderRoot root;
        private readonly ServerPath serverPath;
        
        public DownloadFileFromServer(DropBoxEvent @event, IC