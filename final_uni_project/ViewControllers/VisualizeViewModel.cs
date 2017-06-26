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
                  Graph = ParseGraph(b);

                  Application.Current.Dispatcher.InvokeAsync(() =>
                  {
                      Updated(this, new EventArgs());
                  });

              });

            Coordinator = new GraphCoordinator();

            var viewController = new ViewController(port.CameraController);

            Updated += (a, b) =>
            {
                Coordinator.Coordinate(Graph);
                viewController.Load(Graph);
            };

            ViewController = viewController;
        }

        private IBidirectionalGraph<Vertex, Edge> ParseGraph(GraphArgs b)
        {
            var graph = new BidirectionalGraph<Vertex, Edge>();
            int i = 0;

            foreach (var node in b.Weights)
            {
                bool isStatic = i >= b.MovingTargets;
                graph.AddVertex(new Vertex(
                    i,
                    isStatic,
                    isStatic ? new Vector3D(b.StaticNodes[i - b.MovingTargets].X, b.StaticNodes[i - b.MovingTargets].Y, b.StaticNodes[i - b.MovingTargets].Z) : default(Vector3D)));
                i++;
            }

            var vertices = graph.Vertices.ToList();

            i = 0;

            b.Weights.ForEach(node =>
            {
                var j = 0;
                foreach (var edge in node)
                {
                    Edge e;
                    graph.AddEdge(e = new Edge(vertices[i], vertices[j])
                    {
                        Weight = edge
                    });

                    vertices[i].Edges.Add(e);
                    j++;
                }
                i++;
            });

            return graph;
        }

        public event EventHandler Updated = delegate { };
    }
}