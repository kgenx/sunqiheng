namespace Virgil.LocalStorage
{
    using System;
    using System.IO;
    using System.IO.IsolatedStorage;
    using System.Text;
    using Infrastructure;

    public class IsolatedStorageProvider : IStorageProvider
    {
        private readonly IEncryptor encryptor;
        private const string FolderName = "VirgilSecurity";
        private const string FileName = "keystore";

        private readonly string FilePath = $"{FolderName}/{FileName}";
        

        public IsolatedStorageProvider(IEncryptor encryptor)
        {
