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
			this.bootstrapper = bootstrapper;
		}
		
        public override int Handle(StartOptions command)
        {
            var virgilHub = ServiceHub.Create(ServiceHubConfig.UseAccessToken(ApiConfig.VirgilToken));
            ServiceLocator.Setup(virgilHub);

            var eventAggregator = this.bootstrapper.Container.Resolve<IEventAggregator>();
            eventAggregator.Subscribe(new Listener());

            var appState = this.bootstrapper.Container.Resolve<ApplicationState>();
            appState.Restore();

            if (!appState.HasAccount)
            {
                Console.WriteLine("  