using Bleman_Ford;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeHelper
{
    public class Helper
    {
        Random random = new Random();
        Graph graph = null;
        string[] lines;


        public Graph CreateGraph(string maze)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            lines = maze.Split('\n');
            int size =lines[0].Length;
            graph = new Graph(size);
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    //check if a vertex already exists for this potision
                    if (lines[x][y] == '.' )
                    {
                        // if we dont have a vertex see if this position is free '.' or has a wall in it 'w'
                        if (graph.vertices[x, y] == null)
                        {
                            graph.vertices[x, y] = new Vertex() { Position = new Point(x, y) };
                        }
                        
                        CheckCardinalDirection(CardinalDirections.Up,x,y,size);
                        CheckCardinalDirection(CardinalDirections.Down,x,y,size);
                        CheckCardinalDirection(CardinalDirections.Left,x,y,size);
                        CheckCardinalDirection(CardinalDirections.Right,x,y,size);
                        
                    }
                }
            }
            sw.Stop();
            var elapsed = sw.ElapsedMilliseconds;
            return graph;
        }
        private void CheckCardinalDirection(CardinalDirections directions,int x,int y,int size)
        {
            int x1 = x;
            int y1 = y;
            
            switch (directions)
            {
                case CardinalDirections.Up:
                    x = x - 1;
                    break;
                case CardinalDirections.Down:
                    x = x + 1;
                    break;
                case CardinalDirections.Left:
                    y = y - 1;
                    break;
                case CardinalDirections.Right:
                    y = y + 1;
                    break;
                default:
                    break;
            }
            if (x>=0 && x <size && y>=0 & y<size && lines[x][y] == '.')
            {
                
                    if (graph.vertices[x, y] == null)
                    {
                        graph.vertices[x, y] = new Vertex() { Position = new Point(x, y) };
                    }
                    Edge e = new Edge() { Destination = graph.vertices[x, y], Source = graph.vertices[x1, y1] };
                    
                    graph.vertices[x1, y1].OutgoingEdges.Add(e);
                
            }

            
        }
        public string CreatRandomMaze(int size)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if ((i==j && i==0) ||(i==j && i==size-1))
                    {
                        sb.Append('.');
                        continue;
                    }
                    int num =  random.Next(0, 1000);
                    if (num > 800)
                    {
                        sb.Append('w');
                    }
                    else
                    {
                        sb.Append('.');
                    }
                    
                }
                sb.Append('\n');

            }
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }
    }
}
