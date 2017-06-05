using System.Collections;
using System.Collections.Generic;
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
                IsReceiver = Edges.All(x => !double.IsNegativeInfinity(x.Weight));
            }
        }

        public Vertex(int id)
        {
            ID = id;
            this.Edges = new List<Edge>();
        }
    }
}
