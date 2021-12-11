namespace Virgil.Sync.View
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using System.Windows;
    using Hardcodet.Wpf.TaskbarNotification;
    using Infrastructure;
    using Infrastructure.Messaging;
    using Infrastructure.Messaging.Application;
    using Ookii.Dialogs.Wpf;
    using Application = System.Windows.Application;

    /// <summary>
    /// Interaction logic for TrayWindow.xaml
    /// </summary>
    public partial class TrayWindow : Window,
        IHandle<ErrorMessage>,
        IHandle<CardLoaded>,
        IHandle<Logout>,
        IHandle<FolderSettingsChanged>
    {
        public TrayWindow()
        {
            this.InitializeComponent();

            this.Hide();
            this.ShowInTaskbar = false;

            ServiceLocator.Resolve<IEventAggregator>().Subscribe(this);

            this.UpdateLogout();
            this.UpdateOpenFolder();
        }

        private void TaskbarIcon_OnTrayMouseDoubleClick(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).ShowUI();
        }

        private void TrayWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
        }

        private void OnOpenVirgilDiskClick(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).ShowDecrypte