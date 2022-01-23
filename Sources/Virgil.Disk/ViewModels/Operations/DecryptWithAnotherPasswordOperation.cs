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
        private rea