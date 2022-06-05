namespace Virgil.FolderLink.Encryption
{
    using System;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using Crypto;
    using Crypto.Foundation;

    public class CipherStreamEncryptor : IDisposable
    {
        private readonly Stream sourceStream;
        private readonly VirgilChunkCipher virgilCipher;
        private byte[] buffer;
        private bool hasMore = true;
        private int chunkSize;
        private bool disposed;

        public CipherStreamEncryptor(Stream sourceStream)
        {
            this.sourceStream = sourceStream;
            this.virgilCipher = new VirgilChunkCipher();
        }

        public void AddEncryptedValue(string key, string value, byte[] recipientId, byte[] publicKey)
        {
            using (var cipher = new VirgilCipher())
            {
                cipher.AddKeyRecipient(recipientId, publicKey);
         