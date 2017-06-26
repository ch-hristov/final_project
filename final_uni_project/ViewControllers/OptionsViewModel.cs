using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Windows.Media.Media3D;

namespace final_uni_project
{
    public class OptionsViewModel
    {
        public ObservableCollection<MenuNode> OptionNodes { get; set; }
        public OptionsViewModel()
        {
            OptionNodes = new ObservableCollection<MenuNode>();

            for (int i = 0; i < int.Parse(ConfigurationManager.AppSettings["Static"]); i++)
            {
                var pt = default(Point3D);

                switch (i)
                {
                    case 0:
                        pt = new Point3D(0, 0, 0);
                        break;
                    case 1:
                        pt = new Point3D(100, 0, 0);
                        break;
                    case 2:
                        pt = new Point3D(100, 0, 100);
                        break;
                    case 3:
                        pt = new Point3D(0, 0, 100);
                        break;
                    default:
                        break;
                }
                OptionNodes.Add(new MenuNode((21 + i).ToString(), pt));
            }
        }


    }
}