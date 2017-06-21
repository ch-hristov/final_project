using HelixToolkit.Wpf;
using System.Windows;
using System.Windows.Data;

namespace final_uni_project
{
    public partial class MainWindow : Window
    {
        public IDataFeed feed;
        public MainWindow()
        {
            InitializeComponent();
            var port = new HelixViewport3D();
            feed = new SampleFeed();
            var vm = new VisualizeViewModel(feed, port);
            port.ItemsSource = vm.ViewController.Models;
            grid.Children.Add(port);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var window = new OptionsWindow();
            window.ShowDialog();
            var nodes = window.GetNodes();
            var thisVm = DataContext as VisualizeViewModel;
            feed.Stop();
            feed.Configure(nodes);
            feed.Start();
        }
    }
}
