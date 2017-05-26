using QuickGraph;

namespace final_uni_project
{
    public class Edge : IEdge<Vertex>
    {
        public Edge(Vertex src, Vertex target)
        {
            Source = src;
            Target = target;
        }

        public Vertex Source { get; }

        public Vertex Target { get; }
    }
}
