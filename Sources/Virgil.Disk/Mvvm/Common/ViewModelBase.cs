namespace Infrastructure.Mvvm.Common
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using Properties;

    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyCha