namespace Virgil.Sync.ViewModels.Operations
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Infrastructure.Messaging;
    using Infrastructure.Messaging.Application;
    using SDK.Domain;
    using SDK.Exceptions;

    public class CreateAccountOperation : IConfirmationRequiredOperation
    {
        private readonly IEventAggregator eventAggregator;
        private readonly bool usePassword;
        private readonly bool uploadPrivateKey;

        private string email;
        private string password;

        private IdentityTokenRequest request;

        public CreateAccountOperation(IEventAggregator eventAggregator, bool usePassword, bool uploadPrivateKey)
        {
            this.eventAggregator = eventAggregator;
            this.usePassword = usePassword;
            this.uploadPrivateKey =