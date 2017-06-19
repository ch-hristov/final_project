using System;
using QuickGraph;
using System.Windows.Media.Media3D;
using System.Linq;

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
            var vertexQuery = graph.Vertices.ToLookup(v => v.IsReceiver);

            var staticVertices = vertexQuery[true];
            var movingVertices = vertexQuery[false];

            var first = staticVertices.Take(2);
            var second = staticVertices.Skip(2).Take(2);

            foreach(var vertex in movingVertices)
            {
                
            }
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
