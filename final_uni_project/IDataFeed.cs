using System;

namespace final_uni_project
{
    public interface IDataFeed
    {
        event EventHandler<GraphArgs> DataReceived;

        void Start();
    }
}
