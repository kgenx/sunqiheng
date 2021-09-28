
﻿namespace Virgil.Sync
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Windows;
    using System.Windows.Threading;
    using FolderLink.Core;
    using FolderLink.Dropbox.Messages;
    using FolderLink.Facade;
    using Infrastructure;
    using Infrastructure.Messaging;
    using Ninject;
    using SDK;
    using View;
    using ViewModels;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public Bootstrapper Bootstrapper { get; private set; }