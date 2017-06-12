using System;

namespace final_uni_project
{
    public class DataFeed : IDataFeed
    {
        public event EventHandler<GraphArgs> DataReceived = delegate { };

        public void Start()
        {
            throw new NotImplementedException();
        }
    }
}
