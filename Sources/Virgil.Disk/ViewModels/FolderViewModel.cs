namespace Virgil.Sync.ViewModels
{
    using System.Collections.Generic;
    using System.IO;
    using System.Windows.Input;
    using FolderLink.Facade;
    using Infrastructure.Mvvm;

    public class FolderViewModel : ViewModel
    {
        public delegate void FolderViewModelAction(FolderViewModel model);

        private readonly Folder folder;

        public FolderViewModel(Folder folder)
        {
            this.folder = folder;

            this.OnFolderChangedCommand = new RelayCommand<string>(path =>
            {
                if (!string.IsNullOrWhiteSpace(path))
                {
        