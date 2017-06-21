using System.Windows;

namespace final_uni_project
{
    public partial class OptionsWindow : Window
    {
        public OptionsWindow()
        {
            InitializeComponent();
            DataContext = new OptionsViewModel();

        }
    }
}
