namespace Virgil.FolderLink.Core.Operations
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public abstract class Operation : IProgress<double>
    {
        private IOperationObserver observer;
        private double progress;
        public TaskCompletionSource<int> CompletionSource { get; } = new