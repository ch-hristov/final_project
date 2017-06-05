using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media.Media3D;

namespace final_uni_project
{
    public class Vertex
    {
        public int ID { get; set; }

        public Point3D Position { get; set; }

        public bool IsReceiver { get; private set; }

        private ICollection<Edge> _edges;
        public ICollection<Edge> Edges
        {
            get
            {
                return _edges;
            }
            set
            {
                _edges = value;

            }
        }

        public Vertex(int id)
        {
            ID = id;
            var collection = new ObservableCollection<Edge>();

            collection.CollectionChanged += (a, b) =>
            {
                IsReceiver = Edges.All(x => x.Target.ID == x.Source.ID || !double.IsNegativeInfinity(x.Weight));
            };

            Edges = collection;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Vertex)) return false;
            var node = obj as Vertex;
            return node.ID == ID;
        }
        public override int GetHashCode()
        {
            return ID;
        }

        public bool IsConnectedTo(Vertex v)
        {
            return Edges.Any(edge => edge.Target == v && !double.IsNegativeInfinity(edge.Weight));
        }
    }
}
