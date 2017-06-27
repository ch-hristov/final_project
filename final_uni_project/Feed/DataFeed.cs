using System;
using System.Collections.Generic;
using System.Linq;

namespace final_uni_project
{
    public class DataFeed : IDataFeed
    {
        private IEnumerable<MenuNode> nodes;

        public event EventHandler<GraphArgs> DataReceived = delegate { };

        public void Configure(IEnumerable<MenuNode> nodes)
        {
            this.nodes = nodes;
        }

        public void Push(Dictionary<string, List<Tuple<string, double>>> moving, Dictionary<string, List<Tuple<string, double>>> all)
        {
            var movingData = new Dictionary<int, List<Tuple<int, double>>>();
            var staticData = new Dictionary<int, List<Tuple<int, double>>>();


            foreach (var node in moving)
                movingData.Add(int.Parse(node.Key), node.Value.Select(x => new Tuple<int, double>(int.Parse(x.Item1), x.Item2)).ToList());

            foreach (var node in all.Where(t => !moving.ContainsKey(t.Key)))
                staticData.Add(int.Parse(node.Key), node.Value.Select(z => new Tuple<int, double>(int.Parse(z.Item1), z.Item2)).ToList());

            DataReceived(this, new GraphArgs(staticData, movingData, nodes.ToList()));
        }

        public void Start()
        {

        }

        public void Stop()
        {

        }
    }
}
