using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convertor.Dijkstra.Exceptions
{
    public class GraphBuilderException: Exception
    {
        public GraphBuilderException(string message):base(message)
        {

        }
    }
}
