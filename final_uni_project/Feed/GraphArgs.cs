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

        public GraphArgs(List<List<double>> weights, int moving)
        {
            Weights = weights;
            this.MovingTargets = moving;
        }

        public List<List<double>> Weights { get; private set; }
    }
}
