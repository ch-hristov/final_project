using System;
using System.Collections.Generic;

namespace final_uni_project
{
    public class DataFeed : IDataFeed
    {
        public event EventHandler<GraphArgs> DataReceived = delegate { };

        public void Configure(IEnumerable<MenuNode> nodes)
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
