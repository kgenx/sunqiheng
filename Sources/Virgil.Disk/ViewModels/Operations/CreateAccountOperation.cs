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
            this.uploadPrivateKey = uploadPrivateKey;
        }

        public async Task Initiate(string email, string password)
        {
            this.email = email.Trim().ToLowerInvariant();
            this.password = password;

            var search = await Cards.Search(this.email);
            if (search.Count != 0)
            {
                throw new VirgilException("The card with this e-mail was already created");
            }

            this.request = await Identity.Verify(this.email);
        }

        public async Task Confirm(string code)
        {
            var token = await this.request.Confirm(code);

