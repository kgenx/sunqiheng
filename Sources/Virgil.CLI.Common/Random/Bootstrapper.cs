namespace Virgil.CLI.Common.Random
{
    using Autofac;
    using FolderLink.Facade;
    using Infrastructure;
    using Infrastructure.Messaging;

    public class Bootstrapper
    {
        public virtual void Initialize()
        {
            var builder = new ContainerBuilder();

            // Register individual components

            builder.RegisterType<ApplicationState>().InstancePerLifetimeScope();
            builder.RegisterType<FolderSettingsStorage>().InstancePerLifetimeScope();
            builder.RegisterType<EventAggregator>().As<IEventAggregator>().InstancePerLifetimeScope(