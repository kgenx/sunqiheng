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
                this.aggregator.Publish(new NavigateTo(typeof(SignInViewModel)));
            });

            this.ImportKeyCommand = new RelayCommand(this.ImportKey);

            this.SelectKeyCommand = new RelayCommand(arg => this.SelectedCard != null, this.SelectKey);
        }


        public ICommand SelectKeyCommand { get; }
        public ICommand ReturnToSignInCommand { get; }
        public ICommand ImportKeyCommand { get; }

        public string SelectedPath
        {
            get { return this.selectedPath; }
            set
            {
                if (value == this.selectedPath) return;
                this.selectedPath = value;
                this.RaisePropertyChanged();
            }
        }

        public string Password
        {
            get { return this.password; }
            set
            {
                if (value == this.password) return;
                this.password = value;
                this.RaisePropertyChanged();
            }
        }

        public bool IsShowKeyNotFoundMessage
        {
            get { return this.isShowKeyNotFoundMessage; }
            set
            {
                if (Equals(value, this.isShowKeyNotFoundMessage)) return;
                this.isShowKeyNotFoundMessage = value;
                this.RaisePropertyChanged();
            }
        }

        public ObservableCollection<VirgilCardDto> Cards { get; set; } = new ObservableCollection<VirgilCardDto>();

        public VirgilCardDto SelectedCard
        {
            get { return this.selectedCard; }
            set
            {
                if (Equals(value, this.selectedCard)) return;
                this.selectedCard = value;
                this.RaisePropertyChanged();
                ((RelayCommand) this.SelectKeyCommand).TriggerCanExecute();
            }
        }
        
        public bool IsMultipleKeys
        {
            get { return this.isMultipleKeys; }
            set
            {
                if (value == this.isMultipleKeys) return;
                this.isMultipleKeys = value;
                this.RaisePropertyChanged();
            }
        }

        public override void CleanupState()
        {
            this.SelectedCard = null;
            this.SelectedPath = "";
            this.Password = "";
            this.Cards.Clear();
            this.IsShowKeyNotFoundMessage = false;
        }

        private async void SelectKey(object arg)
        {
            try
            {
                this.ClearErrors();
                this.IsBusy = true;

                var fileDto = this.SelectedCard;
