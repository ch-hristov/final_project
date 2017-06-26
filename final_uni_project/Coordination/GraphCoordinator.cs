using System;
using QuickGraph;
using System.Windows.Media.Media3D;
using System.Linq;
using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra;

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

            foreach (var vertex in movingVertices)
            {
                DeterminePosition(vertex, graph);
            }
        }

        /// <summary>
        /// Determine coordinates of 'vertex' relative to the state of 'graph'
        /// </summary>
        /// <param name="vertex"></param>
        /// <param name="graph"></param>
        private void DeterminePosition(Vertex vertex, IBidirectionalGraph<Vertex, Edge> graph)
        {
            if (vertex.IsReceiver) return;

            if (graph.VertexCount != 5) return;

            // Points
            var points = graph.Vertices.Where(n => n.IsReceiver).OrderBy(n => n.ID).ToList();

            var d21 = Euclidean(points[1].Position, points[0].Position);
            var d31 = Euclidean(points[2].Position, points[0].Position);
            var d41 = Euclidean(points[3].Position, points[0].Position);


            var r1 = vertex.Edges.FirstOrDefault(n => n.Target == points[0]).Weight;
            var r2 = vertex.Edges.FirstOrDefault(n => n.Target == points[1]).Weight;
            var r3 = vertex.Edges.FirstOrDefault(n => n.Target == points[2]).Weight;
            var r4 = 123;//vertex.Edges.FirstOrDefault(n => n.Target == points[3]).Weight;

            double b21 = (Math.Pow(r1, 2) - Math.Pow(r2, 2) + Math.Pow(d21, 2)) / 2;
            double b31 = (Math.Pow(r1, 2) - Math.Pow(r3, 2) + Math.Pow(d31, 2)) / 2;
            double b41 = (Math.Pow(r1, 2) - Math.Pow(r4, 2) + Math.Pow(d41, 2)) / 2;


            // Linear Equation Systems
            // http://numerics.mathdotnet.com/LinearEquations.html
            var A = Matrix<double>.Build.DenseOfArray(new double[,]
            {
                {points[1].Position.X-points[0].Position.X,points[1].Position.Y - points[0].Position.Y,points[1].Position.Z-points[0].Position.Z },
                {points[2].Position.X-points[0].Position.X,points[2].Position.Y - points[0].Position.Y,points[2].Position.Z-points[0].Position.Z },
                {points[3].Position.X-points[0].Position.X,points[3].Position.Y - points[0].Position.Y,points[3].Position.Z-points[0].Position.Z }
            });

            var B = Vector<double>.Build.Dense(new double[] { b21, b31, b41 });
            var X = A.Solve(B);

            vertex.Position = new Point3D(X[0], X[1], X[2]);
        }




        /// <summary>
        /// Return the Euclidean distance between 2 points
        /// </summary>
        private double Euclidean(Point3D a, Point3D b)
        {
            return Math.Sqrt(Math.Pow(a.X - b.X, 2.0f) + Math.Pow(a.Y - b.Y, 2.0f) + Math.Pow(a.Z - b.Z, 2.0f));
        }
    }
}
