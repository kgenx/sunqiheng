namespace Virgil.Sync.ViewModels.Operations
{
    using System.Text;
    using Crypto;
    using Infrastructure.Messaging;
    using Infrastructure.Messaging.Application;
    using SDK.Domain;
    using SDK.Domain.Exceptions;
    using SDK.Models;

    public class DecryptWithAnotherPasswordOperation
    {
        private readonly PrivateKeyModel privateKeyResponse;
        private readonly RecipientCard recipientCard;
        private readonly IEventAggregator aggregator;

        public DecryptWithAnotherPasswordOperation(
            string email,
            PrivateKeyModel privateKeyResponse,
            RecipientCard recipientCard,
            IEventAggregator aggregator)
        {
            this.Email = email;
            this.privateKeyResponse = privateKeyResponse;
            this.recipientCard = recipientCard;
            this.aggregator = aggregator;
        }

        public bool IsPasswordValid(string anotherPassword)
        {
            return VirgilKeyPair.CheckPrivateKeyPassword(this.privateKeyResponse.PrivateKey,
                Encoding