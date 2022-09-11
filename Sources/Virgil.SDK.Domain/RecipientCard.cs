﻿namespace Virgil.SDK.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Models;
    using Virgil.Crypto;


    public class RecipientCard
    {
        protected CardModel VirgilCardDto;

        protected RecipientCard()
        {
        }

        protected RecipientCard(RecipientCard recipientCard)
        {
            this.VirgilCardDto = recipientCard.VirgilCardDto;
            this.Id = recipientCard.Id;
            this.Identity = recipientCard.Identity;
            this.PublicKey = recipientCard.PublicKey;
            this.Hash = recipientCard.Hash;
            this.CreatedAt = recipientCard.CreatedAt;
        }

        public RecipientCard(CardModel virgilCardDto)
        {
            this.VirgilCardDto = virgilCardDto;
            this.Id = virgilCardDto.Id;
            this.Identity = new Identity(virgilCardDto.Identity);
            this.PublicKey = new PublishedPublicKey(virgilCardDto.PublicKey);
            this.Hash = virgilCardDto.Hash;
            this.CreatedAt = virgilCardDto.CreatedAt;
        }

        public Dictionary<string, string> CustomData
        {
            get
            {
                var customData = this.VirgilCardDto?.CustomData;
                if (customData == null)
                {
                    return new Dictionary<string, string>();
                }
                else
                {
                    return new Dictionary<string, string>(customData);
                }
            }
        }

        public Guid Id { get; protected set; }

        public Identity Identity { get; protected set; }

        public PublishedPublicKey PublicKey { get; protected set; }

        public string Hash { get; protected set; }

        public DateTime CreatedAt { get; protected set; }

        public byte[] Encrypt(byte[] data)
        {
            using (var cipher = new VirgilCipher())
            {
                cipher.AddKeyRecipient(this.GetRecepientId(), this.PublicKey.Data);
                return cipher.Encrypt(data, true);
            }
        }

        public string Encrypt(string data)
        {
            return Convert.ToBase64String(this.Encrypt(data.GetBytes(Encoding.UTF8)));
        }

        public byte[] GetRecepientId()
        {
   