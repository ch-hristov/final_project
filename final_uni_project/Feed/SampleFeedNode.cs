using System;
using System.Windows.Media.Media3D;

namespace final_uni_project
{
    public class SampleFeedEdge
    {
        private static Random _generator = new Random();

        public static SampleFeedEdge Empty = new SampleFeedEdge()
        {
            Distance = double.NegativeInfinity
        };

        public SampleFeedEdge()
        {
        }

        public double Distance { get; set; }

    }
}