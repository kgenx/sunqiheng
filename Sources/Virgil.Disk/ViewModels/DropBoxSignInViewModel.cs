namespace Virgil.Sync.ViewModels
{
    using System;
    using System.Windows.Input;
    using Dropbox.Api;
    using FolderLink.Dropbox.Messages;
    using Infrastructure.Messaging;
    using Infrastructure.Messaging.Application;
    using Infrastructure.Mvvm;

    public class HandleNavigation
    {
        public HandleNavigation(Uri uri)
        {
            this.Uri = uri;
        }

        public Uri Uri { get; set; }
        public bool Cancel { get; set; }
    }

    public class DropBoxSignInViewModel : ViewModel
    {
        private readonly IEventAggregator eventAggregator;
        
        private const string RedirectUri = "https://virgilsecurity.com/";
        private string oauth2State;
        private string authorizeUri;

        public DropBoxSignInViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.NavigateBack = new RelayCommand(() =>
            {
                eventAggregator.Publish(new NavigateTo(typeof(FolderSettingsViewModel)));
        