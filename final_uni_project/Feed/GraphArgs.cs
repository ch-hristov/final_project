using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace final_uni_project
{
    public class GraphArgs : EventArgs
    {
        public Dictionary<int, List<Tuple<int, double>>> MovingTargets { get; private set; }

        public GraphArgs(
            Dictionary<int, List<Tuple<int, double>>> weights,
            Dictionary<int, List<Tuple<int, double>>> moving,
            IList<MenuNode> nodes)
        {
            StaticVertices = weights;
            MovingTargets = moving;
            StaticNodes = nodes;
        }

        public Dictionary<int, List<Tuple<int, double>>> StaticVertices { get; private set; }

        public IList<MenuNode> StaticNodes { get; private set; }
    }
}
