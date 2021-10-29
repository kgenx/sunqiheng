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
            this.MouseDown += this.OnMouseDown