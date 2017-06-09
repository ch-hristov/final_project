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
            VirutalPosition = new Vector3D();

            Direction = new Vector3D(
                _generator.NextDouble(),
                _generator.NextDouble(),
                _generator.NextDouble());
        }

        public double Distance { get; set; }

        public double Step { get; set; }

        public Vector3D VirutalPosition { get; private set; }

        public Vector3D Direction { get; private set; }

        public void StepInDirection()
        {
            VirutalPosition += Direction;
        }
    }
}