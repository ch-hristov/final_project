using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace final_uni_project
{
    public class SampleFeed : IDataFeed
    {
        public event EventHandler<GraphArgs> DataReceived = delegate { };
        private Random _rand = new Random();
        private List<List<SampleFeedEdge>> _prev;
        private const int normalizingFactor = 10;

        private List<List<SampleFeedEdge>> RandomGraph(int otherNodes, int movingNodes)
        {
            var resultGraph = new List<List<SampleFeedEdge>>();
            var generateNew = false;

            if (_prev == null) generateNew = true;

            if (generateNew)
                return _prev = GenerateGraph(otherNodes, movingNodes);

            else return _prev = UpdateGraph(movingNodes);
        }

        private List<List<SampleFeedEdge>> UpdateGraph(int movingNodes)
        {
            for (int i = 0; i < movingNodes; i++)
            {
                for (int j = 0; j < _prev[i].Count; j++)
                {
                    if (_prev[i][j] == SampleFeedEdge.Empty) continue;

                    _prev[i][j].StepInDirection();

                }
            }

            return _prev;
        }

        private List<List<SampleFeedEdge>> GenerateGraph(int otherNodes, int movingNodes)
        {
            var graph = new List<List<SampleFeedEdge>>();

            int total = otherNodes + movingNodes;

            for (int i = 0; i < total; i++)
            {
                var connectivity = new List<SampleFeedEdge>();

                for (int j = 0; j < total; j++)
                    connectivity.Add(SampleFeedEdge.Empty);

                graph.Add(connectivity);
            }

            for (int i = movingNodes; i < graph.Count; i++)
            {
                for (int j = 0; j < movingNodes; j++)
                {
                    bool forward = _rand.NextDouble() <= 0.5 ? true : false;
                    if (graph[i][j].Distance == double.NegativeInfinity) graph[i][j].Distance = 0.0;
                    graph[i][j].Distance = forward ? graph[i][j].Distance + _rand.NextDouble()
                        : graph[i][j].Distance - _rand.NextDouble();
                }
            }

            return graph;
        }

        private void PrintGraph(List<List<SampleFeedEdge>> result)
        {
            result.ForEach((list) =>
            {
                list.ForEach((node) => Console.Write(node + " "));
                Debug.WriteLine(Environment.NewLine);
            });
        }

        public void Start()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    await Application.Current.Dispatcher.InvokeAsync(() =>
                    {
                        var g = RandomGraph(5, 2).Select((node) =>
                        {
                            return node.Select(z => z.Distance).ToList();
                        }).ToList();

                        DataReceived(this, new GraphArgs(g));
                    });

                    await Task.Delay(300);
                }
            });
        }
    }
}