using System;
using QuickGraph;
using System.Collections.ObjectModel;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;
using System.Windows.Media;

namespace final_uni_project
{
    public class ViewController
    {
        public ViewController()
        {
            Models = new ObservableCollection<Visual3D>();

        }

        public ObservableCollection<Visual3D> Models { get; set; }

        public void Load(IBidirectionalGraph<Vertex, Edge> graph)
        {
            Models.Clear();

            Models.Add(new SunLight());

            foreach (var node in graph.Vertices)
            {
                Models.Add(new SphereVisual3D()
                {
                    Center = node.Position,
                    Radius = 1,
                    Fill = node.IsReceiver ? Brushes.Red : Brushes.Green,
                    BackMaterial = new DiffuseMaterial(Brushes.Red)
                });
            }
        }
    }
}