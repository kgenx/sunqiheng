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
        private readonly Str