namespace Virgil.Sync.View
{
    using System.Windows.Controls;
    using System.Windows.Navigation;
    using FolderLink.Dropbox.Messages;
    using Infrastructure;
    using Infrastructure.Messaging;
    using ViewModels;

    /// <summary>
    /// Interaction logic for DropBoxSignIn.xaml
    /// </summary>
    public partial class DropBoxSignInView : UserControl, IHandle<DropboxSignOut>
    {
        public DropBoxSignInView()
        {
            this.Initial