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

        public static readonly DependencyProperty SettingsStageProperty = DependencyProperty.Register(
            "SettingsStage", typeof (SettingsStage), typeof (FolderSettingsView), new PropertyMetadata(SettingsStage.None,
                PropertyChangedCallback));

        private static void PropertyChangedCallback(DependencyObject source, DependencyPropertyChangedEventArgs args)
        {
            var @this = (FolderSettingsView) source;
            var state = (SettingsStage) args.NewValue;

            switch (state)
            {
                case SettingsStage.IntroStart:
                    VisualStateManager.GoToState(@this, "Initial", false);
                    break