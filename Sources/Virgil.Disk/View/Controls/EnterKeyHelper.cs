namespace Virgil.Sync.View.Controls
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public static class EnterKeyHelper
    {
        public static ICommand GetEnterKeyCommand(DependencyObject target)
        {
            return (ICommand)target.G