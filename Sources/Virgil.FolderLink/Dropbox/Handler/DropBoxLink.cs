namespace Virgil.FolderLink.Dropbox.Handler
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Core;
    using Core.Operations;
    using Core.Events;
    using Infrastructure.Messaging;
    using Local;
    using Messages;
    using O