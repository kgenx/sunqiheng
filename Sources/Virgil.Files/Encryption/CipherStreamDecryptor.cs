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
        private readonly VirgilHash hashEncryp