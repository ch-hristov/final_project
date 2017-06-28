using Alsolos.Commons.Wpf.Mvvm;
using HelixToolkit.Wpf;
using QuickGraph;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media.Media3D;

namespace final_uni_project
{
    public class VisualizeViewModel
    {
        private object _lock = new object();
        private IDataFeed _feed;
        private IBidirectionalGraph<Vertex, Edge> _graph;
        private IGraphCoordinator Coordinator { get; set; }

        public IBidirectionalGraph<Vertex, Edge> Graph
        {
            get
            {
                return _graph;
            }
            set
            {
                _graph = value;
                Coordinator.Coordinate(_graph);
            }
        }

        public ViewController ViewController { get; private set; }

        public VisualizeViewModel(IDataFeed feed, HelixViewport3D port)
        {
            _feed = feed;
            _feed.Start();

            _feed.DataReceived += ((a, b) =>
             {
                 try
                 {
                     Application.Current.Dispatcher.InvokeAsync(() =>
                     {
                         Graph = ParseGraph(b);
                         Updated(this, new EventArgs());
                     });
                 }
                 catch (Exception) { }
             });

            Coordinator = new GraphCoordinator();

            var viewController = new ViewController(port.CameraController);

            Updated += (a, b) =>
            {
                lock (_lock)
                {
                    Coordinator.Coordinate(Graph);
                    viewController.Load(Graph);
                }
            };

            ViewController = viewController;
        }

        private IBidirectionalGraph<Vertex, Edge> ParseGraph(GraphArgs b)
        {
            var graph = new BidirectionalGraph<Vertex, Edge>();

            var edgeQ = b.StaticVertices.Union(b.MovingTargets)
                                        .ToDictionary(x => x.Key, y => y.Value);

            foreach (var node in b.StaticVertices)
            {
                var query = b.StaticNodes.FirstOrDefault(x => int.Parse(x.Id) == node.Key);
                graph.AddVertex(new Vertex(node.Key, true, new Vector3D(query.X, query.Y, query.Z)));
            }

            foreach (var node in b.MovingTargets)
                graph.AddVertex(new Vertex(node.Key, false));

            var vertices = graph.Vertices.ToList();

            foreach (var vertex in vertices)
            {
                foreach (var edge in edgeQ[vertex.ID])
                {
                    var q = vertices.FirstOrDefault(x => x.ID == edge.Item1);
                    if (q == null) continue;
                    var e = new Edge(vertex, q)
                    {
                        Weight = edge.Item2
                    };
                    graph.AddEdge(e);
                }
            }

            return graph;
        }

        public event EventHandler Updated = delegate { };
    }
}