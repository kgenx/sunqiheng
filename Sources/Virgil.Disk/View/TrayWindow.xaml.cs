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

      