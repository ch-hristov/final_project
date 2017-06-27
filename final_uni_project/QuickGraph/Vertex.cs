﻿using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media.Media3D;

namespace final_uni_project
{
    public class Vertex
    {
        public int ID { get; set; }

        public Point3D Position { get; set; }

        public bool IsReceiver { get; private set; }

        public Vertex(int id, bool isReceiver, Vector3D position = default(Vector3D))
        {
            IsReceiver = isReceiver;
            ID = id;
            var collection = new ObservableCollection<Edge>();
            Position = new Point3D(position.X, position.Y, position.Z);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Vertex))
            {
                return false;
            }

            var node = obj as Vertex;

            return node.ID == ID;
        }
        public override int GetHashCode()
        {
            return ID;
        }
    }
}
