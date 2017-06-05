using System.Windows;

namespace final_uni_project
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var vm = new VisualizeViewModel();
            DataContext = vm;
        }
    }
}
