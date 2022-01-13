namespace Virgil.Sync.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Windows.Input;
    using FolderLink.Facade;
    using Infrastructure.Messaging;
    using Infrastructure.Messaging.Application;
    using Infrastructure.Mvvm;
    using Newtonsoft.Json;
    using Ookii.Dialogs.Wpf;
    using SDK.Domain;
    using SDK.Domain.Exceptions;

    public class KeyManagementViewModel : ViewModel
    {
        private readonly IEventAggregator aggregator;
        private string selectedPath;
        private string password;
        private VirgilCardDto selectedCard;
        private bool isMultipleKeys;
        private bool isShowKeyNotFoundMessage;

        public KeyManagementViewModel(IEventAggregator aggregator)
        {
            this.Password = "";
            this.aggregator = aggregator;

            this.ReturnToSignInCommand = new RelayCommand(() =>
            {
                this.aggregator.Publish(new NavigateTo(typeof(SignInVi