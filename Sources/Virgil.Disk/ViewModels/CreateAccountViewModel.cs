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
      