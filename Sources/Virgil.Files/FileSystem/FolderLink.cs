namespace Virgil.DropBox.Client.FileSystem
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Threading.Tasks;
    using Encryption;
    using Events;
    using Operations;

    public class FolderLink : IFileEventListener
    {
        private readonly EncryptionCredentials credentials;
        private readonly Folder encryptedFolder;
        private readonly Folder decryptedFolder;
        private readonly FolderWatcher encryptedFolderWatcher;
        private readonly FolderWatcher decryptedFolderWatcher;

        private readonly ConcurrentQueue<Operation> operations = new ConcurrentQueue<Operation>();
        
       