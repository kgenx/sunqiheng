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
            this.Hash = virgilCardDto.Has