namespace Virgil.Sync.View
{
    using System;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;

    /// <summary>
    /// Interaction logic for Container.xaml
    /// </summary>
    public partial class ContainerWindow : Window
    {
        private readonly double AspectRatio;

        public static ContainerWindow CurrentInstance { get; private set; } = null;

        public ContainerWindow() : base()
        {
            InitializeComponent();
            this.MouseDown += this.OnMouseDown;
            CurrentInstance = this;

            this.AspectRatio = this.Width / this.Height;
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
            CurrentInstance = null;
        }

        private void Minimize(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        public void PositionWindowOnScreen(double horizontalShift = 0, double verticalShift