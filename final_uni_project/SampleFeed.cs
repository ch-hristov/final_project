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
        private List<List<double>> _prev;
        private const int normalizingFactor = 10;
        public List<bool> Connectivity { get; private set; }

        public SampleFeed(IEnumerable<bool> constants)
        {
            Connectivity = constants.ToList();
        }

        private List<List<double>> RandomGraph()
        {
            var result = new List<List<double>>();

            for (int i = 0; i < Connectivity.Count; i++)
            {
                var ans = new List<double>();

                for (int j = 0; j < Connectivity.Count; j++)
                {
                    if (j == i || (!Connectivity[j] && !Connectivity[i]) || (Connectivity[i]))
                    {
                        ans.Add(double.NegativeInfinity);
                        continue;
                    }

                    var weight = _prev == null ?
                        _rand.NextDouble() * normalizingFactor : (_prev[i][j] + _rand.NextDouble() / normalizingFactor)
                        * (_rand.NextDouble() <= 0.5 ? -1 : 1);

                    if (Connectivity[j])
                        result[j][i] = weight;

                    ans.Add(weight);
                }

                result.Add(ans);
            }

#if DEBUG
            PrintGraph(result);
#endif


            _prev = result;
            return result;
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
            Task.Run(async () =>
            {
                while (true)
                {
                    await Application.Current.Dispatcher.InvokeAsync(() =>
                    {
                        DataReceived(this, new GraphArgs(RandomGraph()));
                    });

                    await Task.Delay(300);
                }
            });
        }
    }
}