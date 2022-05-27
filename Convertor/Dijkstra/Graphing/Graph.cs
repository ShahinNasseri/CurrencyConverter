using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convertor.Dijkstra.Graphing
{
    public class Graph
    {
        private readonly List<Node> _nodes;

        private Graph()
        {
            _nodes = new List<Node>();
        }

        public IReadOnlyList<Node> Nodes => _nodes;

        internal static Graph Create()
        {
            return new Graph();
        }

        internal void Add(Node node)
        {
            _nodes.Add(node);
        }
    }
}
