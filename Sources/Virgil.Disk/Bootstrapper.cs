namespace Virgil.Sync
{
    using FolderLink.Facade;
    using Infrastructure;
    using Infrastructure.Messaging;
    using LocalStorage;
    using Ninject;
    using Ninject.Parameters;
    using ViewModels;

    public class Bootstrapper
    {
        public StandardKernel IoC;

        public void Initialize()
        {
            this.IoC = new StandardKernel();

            this.IoC.Bind<ApplicationState>().ToSelf().InSingletonScope();
            this.IoC.Bind<FolderSettingsStorage>().ToSelf().InSingletonScope();
            this.IoC.Bind<IEventAggr