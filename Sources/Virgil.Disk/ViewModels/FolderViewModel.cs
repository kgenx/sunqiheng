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
                    if (!path.EndsWith(Path.DirectorySeparatorChar.ToString()))
                    {
                        path = path + Path.DirectorySeparatorChar;
                    }
                }
                this.FolderPath = path;
                if (!this.HasErrors)
                {
                    this.OnSuccessfullyChanged?.Invoke(this);
                }
            });
        }

        public string Alias
        {
            get { return this.folder.Alias; }
            set
            {
                if (value == this.folder.Alias) return;
                this.folder.Alias = value;
        