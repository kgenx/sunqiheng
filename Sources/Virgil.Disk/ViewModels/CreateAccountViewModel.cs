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
        private bool isUploadPrivateKey