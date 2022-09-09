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
    