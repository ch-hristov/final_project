using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Linq;

namespace final_uni_project
{
    public class OptionsViewModel
    {
        public ObservableCollection<MenuNode> OptionNodes { get; set; }
        public OptionsViewModel()
        {
            OptionNodes = new ObservableCollection<MenuNode>();

            for (int i = 0; i < int.Parse(ConfigurationManager.AppSettings["Static"]); i++)
                OptionNodes.Add(new MenuNode((i + 1).ToString()));
        }


    }
}