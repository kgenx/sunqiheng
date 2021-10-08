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
            this.encryptor = encryptor;
        }

        public void Save(string data, string path = null)
        {
            using (var storage = GetIsolatedStorage())
            using (var stream = new IsolatedStorageFileStream(path ?? this.FilePath, FileMode.Create, storage))
            {
                var bytes = Encoding.UTF8.GetBytes(data);
                var encrypt = this.encryptor.Encrypt(bytes);
                stream.Write(encrypt, 0, encrypt.Length);
            }
        }

        public string Load(string path = null)
        {
            this.CreateFileIfNotExists();

            using (var storage = GetI