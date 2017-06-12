using QuickGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace final_uni_project
{
    public interface IGraphCoordinator
    {
        void Coordinate(IBidirectionalGraph<Vertex, Edge> graph);
    }
}
