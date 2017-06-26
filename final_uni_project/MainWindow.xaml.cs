using HelixToolkit.Wpf;
using HX7_Render;
using System.Windows;
using System.Windows.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Linq;

namespace final_uni_project
{
    public partial class MainWindow : Window
    {
        public IDataFeed feed;
        private SerialInterface _port;

        public MainWindow()
        {
            InitializeComponent();
            var port = new HelixViewport3D();

            feed = new DataFeed();
            var vm = new VisualizeViewModel(feed, port);

            port.ItemsSource = vm.ViewController.Models;
            grid.Children.Add(port);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var window = new OptionsWindow();
            if (window.ShowDialog() == true)
            {
                var nodes = window.GetNodes();
                var thisVm = DataContext as VisualizeViewModel;
                feed.Stop();
                this.nodes = nodes;
                feed.Configure(nodes);
                feed.Start();
            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            var window = new ConnectWindow();
            window.onPortSelected += Window_onPortSelected;
            window.Show();

        }

        private void Window_onPortSelected(SerialInterface port)
        {
            try
            {
                _port = port;
                _port.NewSerialDataRecieved += DataRecieved;
                _port.StartListening();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /// <summary>
        /// Serial Interface On Data Received variables
        /// </summary>
        private string dataBlock = null;
        private Dictionary<string, List<Tuple<string, double>>> graph = new Dictionary<string, List<Tuple<string, double>>>();
        private Dictionary<string, Point> points = new Dictionary<string, Point>();
        private IEnumerable<MenuNode> nodes;

        private void DataRecieved(object sender, SerialDataEventArgs e)
        {
            string dataReceived = Encoding.ASCII.GetString(e.Data);

            // Forming Data Block
            if (dataReceived.Length > 0) dataBlock += dataReceived;
            string[] splittedDataBlock = dataBlock.Split(new char[] { '\n' });

            //  Process Received Data   
            foreach (var line in splittedDataBlock)
            {
                try
                {
                    // Find measurements
                    if (line.Contains("R") && line.Contains("T") && line.Contains("A") && line.IndexOf("A") != line.Length - 1)
                    {
                        // "R" = _node, "T" = _key, "A" = _value 
                        string _node = line.Substring(line.IndexOf("R") + 1, line.IndexOf("T") - line.IndexOf("R") - 2);
                        string _key = line.Substring(line.IndexOf("T") + 1, line.IndexOf("Q") - line.IndexOf("T") - 2);
                        double _value = double.Parse(line.Substring(line.IndexOf("A") + 1, line.Length - line.IndexOf("A") - 1));

                        // without self connected nodes
                        if (_node != _key)
                        {
                            // If node exist?
                            if (graph.ContainsKey(_node))
                            {
                                Tuple<string, double> Exist = graph[_node].Find(i => i.Item1 == _key);
                                if (Exist != null)
                                {
                                    // update connection
                                    graph[_node].Remove(Exist);
                                    graph[_node].Add(new Tuple<string, double>(_key, _value));
                                }
                                else
                                {
                                    // add new connection
                                    graph[_node].Add(new Tuple<string, double>(_key, _value));
                                }

                            }
                            else
                            {
                                // add new node
                                graph.Add(_node, new List<Tuple<string, double>>() { new Tuple<string, double>(_key, _value) });
                            }
                        }
                    }
                }
                catch (Exception ex) {; ; }
            }

            var nd = new Dictionary<string, List<Tuple<string, double>>>();

            // Chorbata na Ico
            var rem = new List<string>();

            foreach (var node in graph)
                if (nodes.Any(x => x.Id == node.Key))
                    rem.Add(node.Key);

            foreach (var i in graph)
                if (!rem.Contains(i.Key))
                    nd.Add(i.Key, new List<Tuple<string, double>>(graph[i.Key].Select(x => new Tuple<string, double>(x.Item1, x.Item2))));


            feed.Push(nd, graph);
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            var window = new ScryptWindow()
            {
                _port = _port
            };
            window.Show();
        }
    }
}
