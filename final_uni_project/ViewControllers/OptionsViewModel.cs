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
                        pt = new Point3D(10.321, 10.432, 10.532);
                        break;
                    case 1:
                        pt = new Point3D(1000.321, 10.321314, 10.435);
                        break;
                    case 2:
                        pt = new Point3D(1000.312, 10.352, 1000.534);
                        break;
                    case 3:
                        pt = new Point3D(10.213, 10.532, 1000.21);
                        break;
                    default:
                        break;
                }
                OptionNodes.Add(new MenuNode((21 + i).ToString(), pt));
            }
        }


    }
}