namespace Virgil.DropBox.Client.Encryption
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Virgil.Crypto;
    using Virgil.SDK.Keys;
    using Virgil.SDK.Keys.Model;
    using Virgil.SDK.PrivateKeys;
    using Virgil.SDK.PrivateKeys.Http;
    using Virgil.SDK.PrivateKeys.Model;

    public class EncryptionCredentials
    {
        public byte[] PublicKey { get; set; }
        public byte[] PrivateKey { get; set; }
        public byte[] RecepientId { get; set; }

        public EncryptionCredentials()
        {
        }

        public EncryptionCredentials(byte[] publicKey, byte[] privateKey, byte[] recepientId)
        {
            this.PublicKey = publicKey;
            this.PrivateKey = privateKey;
            this.RecepientId = recepientId;
        }
    }

    public static class Keystore
    {
        private const string AppToken = "45fd8a505f50243fa8400594ba0b2b29";
        private const string EmailId = "samasdad@mailinator.com";
        private const string ContainerPassword = "2SZGfXN7WJmsmpQs";

        public async static Task CreateKeyPair()
        {
            var keysService = new KeysClient(AppToken);
            var privateKeysService = new KeyringClient(AppToken);

            //var key  = await keysService.PublicKeys.Search(EmailId);
            
            byte[] publicKey;
            byte[] privateKey;

            // Step 1. Generate Public/Private key pair.

            using (var keyPair = new VirgilKeyPair())
            {
                publicK