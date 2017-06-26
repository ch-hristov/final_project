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
            var data = new List<List<Tuple<string, double>>>();

            foreach (var node in moving)
                data.Add(node.Value.Select(x => x).ToList());

            foreach (var node in all.Where(t => !moving.ContainsKey(t.Key)))
                data.Add(node.Value.Select(z => z).ToList());

            DataReceived(this, new GraphArgs(data, moving.Count, nodes.ToList()));
        }

        public void Start()
        {

        }

        public void Stop()
        {

        }
    }
}
