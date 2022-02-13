
﻿namespace Virgil.Sync.ViewModels
{
    using System;
    using System.Windows.Input;
    using Infrastructure.Messaging;
    using Infrastructure.Messaging.Application;
    using Infrastructure.Mvvm;
    using Operations;
    using SDK.Exceptions;
    using Sync.Messages;

    public class SignInViewModel : ViewModel, IHandle<DisplaySignInError>
    {
        private readonly IEventAggregator aggregator;
        private string login;


        public SignInViewModel(IEventAggregator aggregator)
        {
            this.aggregator = aggregator;

            aggregator.Subscribe(this);
            
            this.ProblemsSigningInCommand = new RelayCommand(() =>
            {
                aggregator.Publish(new ProblemsSigningIn(this.Login));
            });

            this.NavigateToImportCommand = new RelayCommand(() =>
            {
                aggregator.Publish(new NavigateTo(typeof(KeyManagementViewModel)));
            });

            this.SignInCommand = new RelayCommand(async () =>
            {
                this.ClearErrors();

                if (string.IsNullOrWhiteSpace(this.Login))
                {
                    this.AddErrorFor(nameof(this.Login), "Login should be a valid email");
                }

              
                if (this.HasErrors)
                {
                    return;
                }

                try
                {
                    this.IsBusy = true;
                    var operation = new LoadAccountOperation(this.aggregator);
                    await operation.Initiate(this.Login, "");
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
                    this.IsBusy = false;
                }
            });

            this.NavigateToCreateAccountCommand = new RelayCommand(() =>
            {
                aggregator.Publish(new NavigateTo(typeof (ICreateNewAccountModel)));
            });
        }

        public override void CleanupState()
        {
            this.Login = "";
            this.ClearErrors();
        }

        public string Login
        {
            get { return this.login; }
            set
            {
                if (value == this.login) return;
                this.login = value;
                this.RaisePropertyChanged();
            }
        }
        public ICommand SignInCommand { get; }

        public ICommand NavigateToCreateAccountCommand { get; }

        public ICommand ProblemsSigningInCommand { get; }

        public ICommand NavigateToImportCommand { get; }

        public void Handle(DisplaySignInError message)
        {
        }
    }
}