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
    using SDK.Domai