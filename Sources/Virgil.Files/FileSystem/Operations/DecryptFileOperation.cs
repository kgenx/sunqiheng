namespace Virgil.DropBox.Client.FileSystem.Operations
{
    using System.IO;
    using System.Threading.Tasks;
    using Encryption;

    public class DecryptFileOperation : Operation
    {
        private readonly string source;
        private readonly string target;
        private readonly EncryptionCredentials credentials;

        public DecryptFileOperation(string source, string target, EncryptionCredentials credentials)
        {
            this.Title = "Encrypt " + source;

            this.source = source;
            this.target = target;

            this.credentials = credentials;
        }

        public override async Task Execute()
        {
            if (File.Exists(this.tar