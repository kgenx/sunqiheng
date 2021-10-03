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
            this.IoC.Bind<IEventAggregator>().To<EventAggregator>().InSingletonScope();
            this.IoC.Bind<FolderLinkFacade>().ToSelf().InSingletonScope();

            this.IoC.Bind<IStorageProvider>().To<IsolatedStorageProvider>().InSingletonScope();
            this.IoC.Bind<IEncryptor>().To<WindowsPerUserEncryptor>().InSingletonScope();

            this.IoC.Bind<ConfirmationCodeViewModel>().ToSelf().InSingletonScope();

            this.IoC.Bind<IRegenerateKeypairModel>()
                .To<CreateAccountViewModel>()
                .InSingletonScope()
                .WithParameter(new ConstructorArgument("state", CreateAccountViewModel.State.Regenerate