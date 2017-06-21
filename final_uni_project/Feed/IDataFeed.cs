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
    }
}
