using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace final_uni_project
{
    public class SampleFeed : IDataFeed
    {
        private CancellationTokenSource src;
        public event EventHandler<GraphArgs> DataReceived = delegate { };
        private Random _rand = new Random();
        private List<List<double>> _prev;
        private const int normalizingFactor = 10;

        public IList<MenuNode> Nodes { get; private set; }

        public SampleFeed()
        {
            src = new CancellationTokenSource();
        }

        private List<List<double>> RandomGraph(int otherNodes, int movingNodes)
        {
            var resultGraph = new List<List<double>>();
            var generateNew = false;

            if (_prev == null) generateNew = true;

            if (generateNew)
                return _prev = GenerateGraph(otherNodes, movingNodes);

            else return _prev = UpdateGraph(movingNodes);
        }

        private List<List<double>> UpdateGraph(int movingNodes)
        {
            for (int i = 0; i < movingNodes; i++)
            {
                for (int j = movingNodes; j < _prev[i].Count; j++)
                {
                    _prev[i][j] = _prev[j][i] += (0.1);
                }
            }

            return _prev;
        }

        private List<List<double>> GenerateGraph(int otherNodes, int movingNodes)
        {
            var graph = new List<List<double>>();

            int total = otherNodes + movingNodes;

            for (int i = 0; i < total; i++)
            {
                var connectivity = new List<double>();

                for (int j = 0; j < total; j++)
                    connectivity.Add(double.NegativeInfinity);

                graph.Add(connectivity);
            }

            for (int i = 0; i < movingNodes; i++)
            {
                for (int j = movingNodes; j < total; j++)
                {
                    graph[i][j] = graph[j][i] = _rand.NextDouble() * normalizingFactor;
                }
            }

            return graph;
        }

        private void PrintGraph(List<List<double>> result)
        {
            result.ForEach((list) =>
            {
                list.ForEach((node) => Console.Write(node + " "));
                Debug.WriteLine(Environment.NewLine);
            });
        }

        public void Start()
        {
            src = new CancellationTokenSource();
            if (Nodes != null && Nodes.Count() > 0)
            {
                Task.Run(async () =>
            {

                while (true)
                {
                    if (src.Token.IsCancellationRequested) break;
                    await Application.Current.Dispatcher.InvokeAsync(() =>
                    {
                        int staticCount = Nodes.Count;
                        int movingCount = int.Parse(ConfigurationManager.AppSettings["Moving"]);
                        var g = RandomGraph(staticCount, movingCount).Select((node) =>
                        {
                            return node.Select(z => z).ToList();
                        }).ToList();

                        DataReceived(this, new GraphArgs(g, movingCount, Nodes));
                    });

                    await Task.Delay(int.Parse(ConfigurationManager.AppSettings["TickTime"]));

                }
            }, src.Token);
            }
        }

        public void Stop()
        {
            try
            {
                src.Cancel();
                if (Nodes != null)
                    Nodes.Clear();
            }
            catch (OperationCanceledException) { }
        }

        public void Configure(IEnumerable<MenuNode> nodes)
        {
            if (nodes != null)
            {
                Nodes = nodes.ToList();
            }
        }
    }
}