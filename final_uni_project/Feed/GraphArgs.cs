using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace final_uni_project
{
    public class GraphArgs : EventArgs
    {
        public int MovingTargets { get; private set; }

        public GraphArgs(List<List<double>> weights, int moving, IList<MenuNode> nodes)
        {
            Weights = weights;
            this.MovingTargets = moving;
            this.StaticNodes = nodes;
        }

        public List<List<double>> Weights { get; private set; }
        public IList<MenuNode> StaticNodes { get; private set; }
    }
}
