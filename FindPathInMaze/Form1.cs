using Bleman_Ford;
using MazeHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FindPathInMaze
{
    public partial class Form1 : Form
    {

        Graph theGraph = null;
        int size;
        string maze;
        Helper helper;
        public Form1()
        {
            helper = new Helper();
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            maze = helper.CreatRandomMaze(int.Parse(txtMazeSize.Text));
            txtDestination.Text = $"{int.Parse(txtMazeSize.Text) - 1},{int.Parse(txtMazeSize.Text) - 1}";
            size = maze.Split('\n')[0].Length;
            PrintGraph(maze);

        }

        private void PrintGraph(string maze)
        {
            Graphics g = Graphics.FromHwnd(txtBoxMaze.Handle);
            int cellWidth = txtBoxMaze.Size.Width / int.Parse(txtMazeSize.Text);
            int cellHieght = txtBoxMaze.Size.Height / int.Parse(txtMazeSize.Text);
            g.Clear(Color.White);
            Bitmap map = new Bitmap(txtBoxMaze.Size.Width, txtBoxMaze.Size.Height);
            Graphics test = Graphics.FromImage(map);
            // g.FillRectangle(new SolidBrush(Color.Black), 5, 5, 50, 50);
            txtBoxMaze.Clear();
            StringBuilder sb = new StringBuilder();
            var lines = maze.Split('\n');
            for (int i = 0; i < lines[0].Length; i++)
            {
                for (int j = 0; j < lines[0].Length; j++)
                {


                    Rectangle rec = new Rectangle(new Point(i * cellWidth, j * cellHieght), new Size(cellWidth, cellHieght));
                    if (lines[i][j] == 'w')
                    {

                        test.FillRectangle(new SolidBrush(Color.Black), rec);
                    }
                    else if (lines[i][j] == '.')
                    {
                        test.FillRectangle(new SolidBrush(Color.Wheat), rec);
                    }
                }

            }
            g.DrawImage(map, new Point(0, 0));
            //txtBoxMaze.Text = sb.ToString();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            theGraph = helper.CreateGraph(maze);
            theGraph.vertices[0, 0].Cost = 0;

            int SourceX = int.Parse(txtOrigin.Text.Split(',')[0]);
            int Sourcey = int.Parse(txtOrigin.Text.Split(',')[1]);
            int destinationX = int.Parse(txtDestination.Text.Split(',')[0]);
            int destinationY = int.Parse(txtDestination.Text.Split(',')[1]);


            theGraph.ProcessGraph(theGraph.vertices[SourceX, Sourcey]);

            var path = theGraph.GetPathToDestination(theGraph.vertices[destinationX, destinationY]);
            if (path == null)
            {
                txtBoxSolved.Text = "No Solution Found, Sorry you are foever stuck in this Maze \n :-("; return;
            }
            path.Insert(0, theGraph.vertices[0, 0]);
            PrintPath(maze, path);
        }

        private void PrintPath(string maze, List<Vertex> path)
        {
            Bitmap tempImage = new Bitmap(txtBoxSolved.Size.Width, txtBoxSolved.Size.Height);
            Graphics temp = Graphics.FromImage(tempImage);
            
            Graphics g = Graphics.FromHwnd(txtBoxSolved.Handle);
            int cellWidth = txtBoxSolved.Size.Width / int.Parse(txtMazeSize.Text);
            int cellHieght = txtBoxSolved.Size.Height / int.Parse(txtMazeSize.Text);

            //  StringBuilder sb = new StringBuilder();
            //   txtBoxSolved.Clear();
            //   Stopwatch sw = new Stopwatch();
            //  sw.Start();
            var lines = maze.Split('\n');
            g.Clear(Color.White);

            for (int i = 0; i < lines[0].Length; i++)
            {
                for (int j = 0; j < lines[0].Length; j++)
                {
                    var v = path.SingleOrDefault(ver => ver.Position.X == i && ver.Position.Y == j);
                    if (v != null)
                    {
                        temp.FillRectangle(new SolidBrush(Color.Red), new Rectangle(new Point(i * cellWidth, j * cellHieght), new Size(cellWidth, cellHieght)));
                    }
                    else if (lines[i][j] == 'w')
                    {
                        temp.FillRectangle(new SolidBrush(Color.Black), new Rectangle(new Point(i * cellWidth, j * cellHieght), new Size(cellWidth, cellHieght)));

                    }
                    else if (lines[i][j] == '.')
                    {
                        temp.FillRectangle(new SolidBrush(Color.Wheat), new Rectangle(new Point(i * cellWidth, j * cellHieght), new Size(cellWidth, cellHieght)));

                    }
                    {
                        // sb.Append($"  {lines[i][j]}  ");
                    }
                }
                // sb.Append(Environment.NewLine);

            }
            //sw.Stop();
            //long elapsed = sw.ElapsedMilliseconds;
            g.DrawImage(tempImage,new PointF(0,0));
            // txtBoxSolved.Text = sb.ToString();
        }
    }
}
