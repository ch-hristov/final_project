using HelixToolkit.Wpf;
using System.Windows;
using System.Windows.Data;

namespace final_uni_project
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var port = new HelixViewport3D();
            var vm = new VisualizeViewModel(port);
            port.ItemsSource = vm.ViewController.Models;
            grid.Children.Add(port);
        }
    }
}
