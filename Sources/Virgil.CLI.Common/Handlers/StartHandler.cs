using System.Threading.Tasks;

namespace Virgil.CLI.Common.Handlers
{
    using System;
    using System.Linq;
    using Autofac;
    using FolderLink.Core;
    using FolderLink.Dropbox.Messages;
    using FolderLink.Facade;
    using Infrastructure.Messaging;
    using Options;
    using Random;
    using SDK;
    using SDK.Domain;

    public class StartHandler : CommandHandler<StartOptions>
    {
        private readonly Bootstrapper bootstrapper;

		public StartHandler (Bootstrapper bootstrapper)
		{		
			this.boo