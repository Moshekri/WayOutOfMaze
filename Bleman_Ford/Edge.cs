using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bleman_Ford
{
    public class Edge
    {

        public Vertex Source { get; set; }
        public Vertex Destination { get; set; }
        public int Weight { get; set; }
        public Edge()
        {
            Weight = 1;
        }

    }
}
