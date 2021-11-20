namespace Virgil.Sync.View
{
    using System.Windows;
    using System.Windows.Controls;
    using ViewModels;

    /// <summary>
    /// Interaction logic for FolderSettings.xaml
    /// </summary>
    public partial class FolderSettingsView : UserControl
    {
        public FolderSettingsView()
        {
            this.InitializeComponent();

            this.UpdateAnimation(false);
        }

        public static readonly DependencyProperty SettingsStageProperty = DependencyProperty.Re