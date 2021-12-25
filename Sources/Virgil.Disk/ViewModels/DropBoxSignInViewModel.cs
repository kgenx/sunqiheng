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
 