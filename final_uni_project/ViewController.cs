using System;
using QuickGraph;
using System.Collections.ObjectModel;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;

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
            foreach (var node in graph.Vertices)
            {
                Models.Add(new SphereVisual3D()
                {
                    Center = node.Position,
                    Radius = 1500
                });
            }
        }
    }
}