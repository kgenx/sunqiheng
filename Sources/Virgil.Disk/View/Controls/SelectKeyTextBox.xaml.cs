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
            