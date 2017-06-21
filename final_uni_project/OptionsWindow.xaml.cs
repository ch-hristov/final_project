using System.Collections;
using System.Collections.Generic;
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

        public IEnumerable<MenuNode> GetNodes()
        {
            return (DataContext as OptionsViewModel).OptionNodes;
        }
    }
}
