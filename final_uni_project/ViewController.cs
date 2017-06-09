using System;
using QuickGraph;
using System.Collections.ObjectModel;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;
using System.Windows.Media;
using System.Collections.Generic;

namespace final_uni_project
{
    public class ViewController
    {
        private Dictionary<Vertex, SphereVisual3D> _vertexVisuals;
        private Dictionary<Edge, LinesVisual3D> _edgeVisuals;
        public ViewController()
        {
            Models = new ObservableCollection<Visual3D>();
            _vertexVisuals = new Dictionary<Vertex, SphereVisual3D>();
            _edgeVisuals = new Dictionary<Edge, LinesVisual3D>();
        }

        public ObservableCollection<Visual3D> Models { get; set; }

        public void Load(IBidirectionalGraph<Vertex, Edge> graph)
        {
            Models.Clear();
            _vertexVisuals.Clear();
            _edgeVisuals.Clear();

            Models.Add(new SunLight());

            foreach (var node in graph.Vertices)
            {
                SphereVisual3D visual;
                Models.Add(visual = new SphereVisual3D()
                {
                    Center = node.Position,
                    Radius = 1,
                    Fill = node.IsReceiver ? Brushes.Red : Brushes.Green,
                    BackMaterial = new DiffuseMaterial(Brushes.Red)
                });
                _vertexVisuals.Add(node, visual);
            }

            foreach (var edge in graph.Edges)
            {
                if (!double.IsNegativeInfinity(edge.Weight))
                {
                    LinesVisual3D lv3;
                    Models.Add(lv3 = new LinesVisual3D()
                    {
                        Points = new Point3DCollection()
                        {
                            edge.Source.Position,
                            edge.Target.Position
                        }
                    });
                    _edgeVisuals.Add(edge, lv3);
                }

            }
        }
    }
}