using System;
using System.Collections.Generic;

namespace final_uni_project
{
    public interface IDataFeed
    {
        event EventHandler<GraphArgs> DataReceived;

        void Start();
        void Stop();
        void Configure(IEnumerable<MenuNode> nodes);

        void Push(Dictionary<string, List<Tuple<string, double>>> moving, Dictionary<string, List<Tuple<string, double>>> all);

    }
}
