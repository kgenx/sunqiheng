namespace Virgil.Sync.ViewModels
{
    using System.Windows.Input;
    using FolderLink.Dropbox.Messages;
    using Infrastructure.Messaging;
    using Infrastructure.Messaging.Application;
    using Infrastructure.Mvvm;

    public class ErrorMessageViewModel : ViewModel
    {
        private readonly IEventAggregator eventAggregator;
        private string errorLarge;
        private string errorDetails;

        public ErrorMessageViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;

            this.OkCommand = new RelayCommand(() =>
            {
                this.eventAggregator