namespace Virgil.DropBox.Client.Encryption
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Virgil.Crypto;
    using Virgil.Crypto.Foundation;

    public class CipherStreamDecryptor : IDisposable
    {
        private readonly Stream sourceStream;
        private readonly VirgilChunkCipher virgilCipher;
        private byte[] buffer;
        private bool hasMore = true;
        private int chunkSize;
        private bool disposed;

        private readonly VirgilHash hashOrigianl = VirgilHash.Sha512();
        private readonly VirgilHash hashEncrypted = VirgilHash.Sha512();
        private Hashes? hashes;

        public CipherStreamDecryptor(Stream sourceStream)
        {
            this.sourceStream = sourceStream;
            this.virgilCipher = new VirgilChunkCipher();
        }

        public async Task Init(byte[] recipientId, byte[] privateKey)
        {
            const int contentInfoHeaderSize = 16;
            var contentInfoHeader = new byte[contentInfoHeaderSize];
            await this.sourceStream.ReadAsync(contentInfoHeader, 0, contentInfoHeaderSize);
            int infoSize = (int)VirgilCipherBase.DefineContentInfoSize(contentInfoHeader);

            if (infoSize == 0 || infoSize < contentInfoHeaderSize)
            {
                throw new VirgilException("Encrypted file does not contain embedded content info.");
            }

            var contentInfo = new byte[infoSize];
            await this.sourceStream.ReadAsync(contentInfo, contentInfoHeaderSize, infoSize - contentInfoHeaderSize);
            Array.Copy(contentInfoHeader,contentInfo,contentInfoHeaderSize);
            
            this.virgilCipher.SetContentInfo(contentInfo);
            
            this.chunkSize = (int)this.virgilCipher.StartDecryptionWithKey(recipientId, privateKey);
            this.