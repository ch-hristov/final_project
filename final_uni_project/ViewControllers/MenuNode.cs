using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace final_uni_project
{
    public class MenuNode : INotifyPropertyChanged
    {
        public MenuNode(string label, Point3D pt)
        {
            Id = label;
            this.X = pt.X;
            this.Y = pt.Y;
            this.Z = pt.Z;
        }

        private double x;
        public double X
        {
            get { return x; }
            set
            {
                x = value;
                PropertyChanged(this, new PropertyChangedEventArgs("X"));
            }
        }
        private double y;
        public double Y { get { return y; } set { y = value; PropertyChanged(this, new PropertyChangedEventArgs("Y")); } }

        private double z;
        public double Z { get { return z; } set { z = value; PropertyChanged(this, new PropertyChangedEventArgs("Z")); } }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };


        private string id;
        public string Id
        {
            get
            {
                return id;
            }
            set
            {
                this.id = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Id"));
            }
        }
    }
}