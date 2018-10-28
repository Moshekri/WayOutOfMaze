using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bleman_Ford
{
    public class Graph
    {
        public Vertex[,] vertices { get; set; }
        int size;
        Vertex _source;

        public Graph(int size)
        {
            vertices = new Vertex[size, size];
            this.size = size;
        }
        public void ProcessGraph(Vertex source)
        {
            int count = 0;
            foreach (var item in vertices)
            {
                if (item!=null)
                {
                    count++;
                }
            }
            bool isChanged = false;
            _source = source;
            for (int i = 0; i < count; i++)
            {
                isChanged = false;
                for (int x = 0; x < this.size; x++)
                {
                    for (int y = 0; y < this.size; y++)
                    {
                        if (vertices[x, y] != null)
                        {
                            foreach (Edge edge in vertices[x, y].OutgoingEdges)
                            {
                                if (edge.Destination.Cost > edge.Weight + edge.Source.Cost)
                                {
                                    edge.Destination.Cost = edge.Weight + edge.Source.Cost;
                                    edge.Destination.Ancestor = edge.Source;
                                    isChanged = true;
                                }
                            }
                        }
                    }
                }
                if (!isChanged)
                {
                    return;
                }
            }
            
        }
        public long GetCostToDestination(Vertex source, Vertex Destination)
        {
            ProcessGraph(source);
            return vertices[Destination.Position.X, Destination.Position.Y].Cost;
        }
        public List<Vertex> GetPathToDestination(Vertex Destination)
        {
            List<Vertex> path = new List<Vertex>();
            Vertex currant = Destination;
            
            do
            {
                if (null == currant || currant.Ancestor == null)
                {
                    return null;
                }
                path.Insert(0, currant);
                currant = currant.Ancestor;

            } while (currant != _source);
            return path;
        }

    }
}
