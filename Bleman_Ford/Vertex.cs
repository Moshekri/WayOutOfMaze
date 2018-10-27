using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bleman_Ford
{
    public class Vertex
    {
        public long Cost { get; set; }
        public Vertex Ancestor { get; set; }
        public Point Position { get; set; }
        public List<Edge> OutgoingEdges { get; set; }
        public Vertex()
        {
            Cost = long.MaxValue - int.MaxValue;
            OutgoingEdges = new List<Edge>();
        }
    }
}
