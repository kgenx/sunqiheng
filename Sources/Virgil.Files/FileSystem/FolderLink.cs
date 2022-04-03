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
        
        public FolderLink(string encryptedFolder, string decryptedFolder, EncryptionCredentials credentials)
        {
            this.credentials = credentials;
            this.encryptedFolder = new Folder(encryptedFolder, "Encrypted", this);
            this.decryptedFolder = new Folder(decryptedFolder, "Decrypted", this);

            this.encryptedFolderWatcher = new FolderWatcher(this.encryptedFolder);
            this.decryptedFolderWatcher = new FolderWatcher(this.decryptedFolder);
        }

        public void Launch()
        {
            this.encryptedFolder.Scan();
            this.decryptedFolder.Scan();

            var encr