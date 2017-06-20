using System.Collections.Generic;
using System.Windows.Input;

namespace final_uni_project
{
    public class MenuNode
    {
        public ICommand Run { get; set; }

        public string Label { get; set; }


        public MenuNode(ICommand command, string label)
        {
            Run = command;
            Label = label;
            MenuItems = new List<MenuNode>();
        }


        public IList<MenuNode> MenuItems { get; private set; }
    }
}