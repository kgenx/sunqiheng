
﻿namespace Virgil.SDK.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Exceptions;
    using Identities;
    using Models;
    using Newtonsoft.Json;
    using SDK.Exceptions;
    using Virgil.Crypto;
    

    public class PersonalCard : RecipientCard
    {
        internal PersonalCard(CardModel cardDto, PrivateKey privateKey) : base(cardDto)
        {
            this.PrivateKey = privateKey;
            this.IsPrivateKeyEncrypted = VirgilKeyPair.IsPrivateKeyEncrypted(privateKey);
        }

        public PersonalCard(RecipientCard recipientCard, PrivateKey privateKey) : base(recipientCard)
        {
            this.PrivateKey = privateKey;
            this.IsPrivateKeyEncrypted = VirgilKeyPair.IsPrivateKeyEncrypted(privateKey);
        }

        internal PersonalCard(CardModel virgilCardDto, PublicKeyModel publicKey, PrivateKey privateKey)
        {
            this.VirgilCardDto = virgilCardDto;
            this.Id = virgilCardDto.Id;
            this.Identity = new Identity(virgilCardDto.Identity);
            this.PublicKey = new PublishedPublicKey(publicKey);
            this.Hash = virgilCardDto.Hash;
            this.CreatedAt = virgilCardDto.CreatedAt;
            this.PrivateKey = privateKey;
            this.IsPrivateKeyEncrypted = VirgilKeyPair.IsPrivateKeyEncrypted(privateKey);
        }

        public PrivateKey PrivateKey { get; }

        public bool IsPrivateKeyEncrypted { get; }

        public bool CheckPrivateKeyPassword(string password)
        {
            return VirgilKeyPair.CheckPrivateKeyPassword(this.PrivateKey.Data, password.GetBytes());
        }
        
        public byte[] Decrypt(byte[] cipherData, string privateKeyPassword = null)
        {
            using (var cipher = new VirgilCipher())
            {
                var contentInfoSize = VirgilCipherBase.DefineContentInfoSize(cipherData);
                if (contentInfoSize == 0)
                {