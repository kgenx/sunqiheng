namespace Virgil.SDK.Domain
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

      