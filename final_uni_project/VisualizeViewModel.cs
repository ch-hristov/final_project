﻿using QuickGraph;
using System;
using System.Collections.Generic;
using System.Linq;

namespace final_uni_project
{
    public class VisualizeViewModel
    {
        private IDataFeed _feed;
        private IBidirectionalGraph<Vertex, Edge> _graph;

        public IBidirectionalGraph<Vertex, Edge> Graph
        {
            get
            {
                return _graph;
            }
        }

        public ViewController ViewController { get; private set; }

        public VisualizeViewModel()
        {
            _feed = new SampleFeed(new[] { true, true, true, false });

            _feed.Start();

            _feed.DataReceived += ((a, b) =>
              {
                  _graph = ParseGraph(b);
                  Updated(this, new EventArgs());
              });

            var viewController = new ViewController();

            Updated += (a, b) =>
            {
                viewController.Load(Graph);
            };

            this.ViewController = viewController;
        }

        private IBidirectionalGraph<Vertex, Edge> ParseGraph(GraphArgs b)
        {
            var graph = new BidirectionalGraph<Vertex, Edge>();
            int i = 0;

            foreach (var node in b.Weights)
                graph.AddVertex(new Vertex(i++));

            var vertices = graph.Vertices.ToList();

            i = 0;
            b.Weights.ForEach(node =>
            {
                int j = 0;
                foreach (var edge in node)
                {
                    graph.AddEdge(new Edge(vertices[i], vertices[j]));
                    j++;
                }
            });

            return graph;
        }

        public event EventHandler Updated = delegate { };
    }
}