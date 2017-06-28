using System;
using QuickGraph;
using System.Collections.ObjectModel;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;
using System.Windows.Media;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace final_uni_project
{
    public class ViewController : INotifyPropertyChanged
    {

        private Dictionary<Vertex, SphereVisual3D> _vertexVisuals;
        private Dictionary<Edge, LinesVisual3D> _edgeVisuals;

        public ViewController(CameraController camera)
        {
            Models = new ObservableCollection<Visual3D>();
            _vertexVisuals = new Dictionary<Vertex, SphereVisual3D>();
            _edgeVisuals = new Dictionary<Edge, LinesVisual3D>();
            _camera = camera;
        }

        public ObservableCollection<Visual3D> Models { get; set; }

        private CameraController _camera;


        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        /// <summary>
        /// Load visual models
        /// </summary>
        /// <param name="graph"></param>
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
                    Radius = 10,
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

        //private void RenderPlane()
        //{
        //    try
        //    {
        //        var bill = new RectangleVisual3D()
        //        {
        //            Origin = this.Models.Where(x => x is SphereVisual3D).Cast<SphereVisual3D>().First().Center,
        //            Width = 10,
        //            Fill = Brushes.Red
        //        };

        //        Models.Add(bill);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //    }
        //}
    }
}