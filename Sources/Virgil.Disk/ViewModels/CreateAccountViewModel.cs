namespace Virgil.Sync.ViewModels
{
    using System;
    using System.Windows.Input;
    using Infrastructure.Messaging;
    using Infrastructure.Messaging.Application;
    using Infrastructure.Mvvm;
    using Operations;
    using SDK.Exceptions;
    using Sync.Messages;

    public interface ICreateNewAccountModel
    {
        
    }

    public interface IRegenerateKeypairModel
    {
        string Login { get; set; }
    }

    public class CreateAccountViewModel : ViewModel, ICreateNewAccountModel, IRegenerateKeypairModel
    {
        private readonly IEventAggregator aggregator;
        private readonly State state;
        private string confirmPassword;
        private string login;
        private string password;
        private bool isUploadPrivateKey;
        private bool isPasswordUsed;

        public enum State
        {
            CreateNewAccount,
            RegenerateKeyPair
        }

        public CreateAccountViewModel(IEventAggregator aggregator, State state)
        {
            this.Title = "";
            this.ConfirmButtonTitle = "";
            this.ReturnToPreviousPageTitle = "";

            switch (state)
            {
                case State.CreateNewAccount:
                    this.Title = "Create an account";
                    this.ConfirmButtonTitle = "CREATE MY ACCOUNT";
                    this.ReturnToPreviousPageTitle = "HAVE AN ACCOUNT";
                    break;
                case State.RegenerateKeyPair:
                    this.Title = "Regenerate keypair";
                    this.ConfirmButtonTitle = "REGENERATE KEYPAIR";
                    this.ReturnToPreviousPageTitle = "RETURN TO SIGN IN";
                    break;
            }

            this.aggregator = aggregator;
            this.state = state;

            this.NavigateToSignInCommand = new RelayCommand(() =>
            {
                this.aggregator.Publish(new NavigateTo(typeof (SignInViewModel)));
                this.CleanupState();
            });

            this.CreateAccountCommand = new RelayCommand(async () =>
            {
                this.ClearErrors();

                if (string.IsNullOrWhiteSpace(this.Login))
                {
                    this.AddErrorFor(nameof(this.Login), "Login should be a valid email");
                }

                if (this.IsPasswordUsed)
                {
                    if (string.IsNullOrEmpty(this.Password))
                    {
                        this.AddErrorFor(nameof(this.Password), "You should provide password");
                    }

                    if (!string.IsNullOrEmpty(this.Password))
                    {
                        if (this.Password != this.ConfirmPassword)
                        {
                            this.AddErrorFor(nameof(this.Password), "Passwords should match");
                            this.AddErrorFor(nameof(this.ConfirmPassword), "Passwords should match");
                        }
                    }
                }

                if (this.HasErrors)
                    return;

                try
                {
                    this.IsBusy = true;
                    IConfirmationRequiredOperation operation;
                    switch (this.state)
                    {
                        case State.CreateNewAccount:
                            operation = new CreateAccountOperation(this.aggregator, this.IsPasswordUsed, this.IsUploadPrivateKey);
                            break;
                        case State.RegenerateKeyPair:
                            operation = new RegenerateKeyPairOperation(this.aggregator, this.IsPasswordUsed, this.IsUploadPrivateKey);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(state), state, null);
                    }

                    await operation.Initiate(this.Login, this.Password);
                    this.aggregator.Publish(new ConfirmOperation(operation));
                }
                catch (VirgilPublicServicesException exception) when (exception.ErrorCode == 30202)
                {
                    this.AddErrorFor(nameof(this.Login), exception.Message);
                }
                catch (IdentityServiceException exception) when (exception.ErrorCode == 40200)
                {
                    this.AddErrorFor(nameof(this.Login), exception.Message);
                }
                catch (Exception exception)
                {
                    this.RaiseErrorMessage(exception.Message);
                }
                finally
                {
                    this.IsBu