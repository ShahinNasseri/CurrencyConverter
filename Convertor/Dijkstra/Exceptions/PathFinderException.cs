using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convertor.Dijkstra.Exceptions
{
    public class PathFinderException: Exception
    {
        public PathFinderException(string message): base(message)
        {

        }
    }
}
