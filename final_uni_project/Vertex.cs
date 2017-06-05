using System.Windows.Media.Media3D;

namespace final_uni_project
{
    public class Vertex
    {
        public int ID { get; set; }

        public Point3D Position { get; set; }

        public Vertex(int id)
        {
            this.ID = id;
        }
    }
}
