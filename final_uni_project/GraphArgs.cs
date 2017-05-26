using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace final_uni_project
{
    public class GraphArgs : EventArgs
    {
        public GraphArgs(List<List<double>> weights)
        {
            this.Weights = weights;
        }

        public List<List<double>> Weights { get; private set; }
    }
}
