using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickGraph;
using System.Windows.Media.Media3D;

namespace final_uni_project
{
    public class GraphCoordinator : IGraphCoordinator
    {
        private Random _random;

        public GraphCoordinator()
        {
            _random = new Random();
        }

        public void Coordinate(IBidirectionalGraph<Vertex, Edge> graph)
        {
            foreach (var vertex in graph.Vertices)
                DeterminePosition(vertex, graph);
        }

        private void DeterminePosition(Vertex vertex, IBidirectionalGraph<Vertex, Edge> graph)
        {
            vertex.Position = new Point3D(_random.Next(10), _random.Next(10), _random.Next(10));
        }
    }
}
