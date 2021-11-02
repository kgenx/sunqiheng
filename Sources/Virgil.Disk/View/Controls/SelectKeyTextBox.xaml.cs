using System.Windows;

namespace Virgil.Sync.View.Controls
{
    using System.Windows.Forms;
    using Infrastructure.Mvvm;
    using Ookii.Dialogs.Wpf;
    using UserControl = System.Windows.Controls.UserControl;

    /// <summary>
    /// Interaction logic for SelectKeyTextBox.xaml
    /// </summary>
    public partial class SelectKeyTextBox : UserControl
    {
        public SelectKeyTextBox()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty SelectedPathProperty = DependencyProperty.Register(
            "SelectedPath", typeof(string), typeof(SelectKeyTextBox), new PropertyMetadata(default(string)));

        public string SelectedPath
        {
            get { return (string)GetValue(SelectedPathProperty); }
            set { SetValue(SelectedPathProperty, value); }
        }

        public static readonly DependencyProperty OnFileChangedCommandProperty = DependencyProperty.Register(
            "OnFileChangedCommand", typeof(RelayCommand<string>), typeof(SelectKeyTextBox), new PropertyMetadata(default(RelayCommand<string>)));

        public RelayCommand<string> OnFileChangedCommand
        {
            get { return (RelayCommand<string>)Ge