namespace Virgil.Sync.ViewModels
{
    using System;
    using FolderLink.Core.Operations;

    public class OperationViewModel : ViewModel, IOperationObserver
    {
        private double progress;

        public string Title { get; }

        public double Progress
        {
            get { return this.progress; }
            set
            {
                if (value.Equals(this.progress)) return;
                th