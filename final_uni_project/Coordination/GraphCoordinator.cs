using System;
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

        /// <summary>
        /// Determine coordinates of 'vertex' relative to the state of 'graph'
        /// </summary>
        /// <param name="vertex"></param>
        /// <param name="graph"></param>
        private void DeterminePosition(Vertex vertex, IBidirectionalGraph<Vertex, Edge> graph)
        {
            if (!vertex.IsReceiver) return;


        }
    }
}
