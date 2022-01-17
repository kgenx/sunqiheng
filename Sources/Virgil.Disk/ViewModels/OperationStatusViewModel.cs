namespace Virgil.Sync.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Linq;
    using FolderLink.Core.Operations;
    using FolderLink.Dropbox.Messages;
    using Infrastructure.Messaging;

    public class OperationStatusViewModel : ViewModel, IHandle<DropBoxLinkChanged>
    {
        private ObservableCollection<Operation> operations;
        private ObservableCollection<OperationViewModel> operationViewModels;

        public ObservableCollection<OperationViewModel> Operations
        {
            get { return this.operationViewModels; }
            private set
            {
                if (Equals(value, this.operationViewModels)) return;
                this.operationViewModels = value;
                this.RaisePropertyChanged();
            }
        }

        public OperationStatusViewModel(I