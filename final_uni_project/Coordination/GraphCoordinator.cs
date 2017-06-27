using System;
using QuickGraph;
using System.Windows.Media.Media3D;
using System.Linq;
using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra;
using System.Diagnostics;

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
            var movingVertices = vertexQuery[false];

            foreach (var vertex in movingVertices)
            {
                try
                {
                    DeterminePosition(vertex, graph);
                }
                catch (Exception ex) { }
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

            Edge e;
            if (!graph.TryGetEdge(vertex, points[0], out e))
            {
                return;
            }
            var r1 = e.Weight;
            if (!graph.TryGetEdge(vertex, points[1], out e))
            {
                return;
            }
            var r2 = e.Weight;
            if (!graph.TryGetEdge(vertex, points[2], out e))
            {
                return;
            }
            var r3 = e.Weight;
            if (!graph.TryGetEdge(vertex, points[3], out e))
            {
                return;
            }
            var r4 = e.Weight;

            var b21 = (Math.Pow(r1, 2) - Math.Pow(r2, 2) + Math.Pow(d21, 2)) / 2;
            var b31 = (Math.Pow(r1, 2) - Math.Pow(r3, 2) + Math.Pow(d31, 2)) / 2;
            var b41 = (Math.Pow(r1, 2) - Math.Pow(r4, 2) + Math.Pow(d41, 2)) / 2;


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

            var vectPoint = new Vector3D(X[0], X[1], X[2]);

            var vectors = new[] { points[0], points[1], points[2], points[3] }
                                .Select(n => new Vector3D(n.Position.X, n.Position.Y, n.Position.Z));

            var mid = vectors.Aggregate((curr, next) => { return curr += next; }) / vectors.Count();
            var midToPoint = mid - vectPoint;
            var length = midToPoint.Length;
            midToPoint.Normalize();

            var final = midToPoint * (0.0001 * length);

            vertex.Position = new Point3D(final.X, final.Y, final.Z);
        }

        private static int GetScaleIndex(Vector<double> X, int sp)
        {
            for (int i = 0; i < X.Count; i++)
            {
                for (int j = 0; j < X.Count; j++)
                {
                    if (X[i] >= X[j] * 10)
                    {
                        sp = i;
                        break;
                    }
                }
                if (sp != -1)
                    break;
            }

            return sp;
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
