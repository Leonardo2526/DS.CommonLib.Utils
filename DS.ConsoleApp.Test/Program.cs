using DS.ClassLib.FileSystemUtils;
using DS.ClassLib.VarUtils;
using DS.ClassLib.VarUtils.Graphs;
using Rhino.Geometry;
using Rhino.Geometry.Intersect;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DS.ConsoleApp.Test
{
    internal class Program
    {

        static void Main(string[] args)
        {
            new QuadrantModelTest();
            //new BoxOriginTest();
            //new BoundingBoxTest();
            //new LinesIntersectionTest();
            //new EnumeratorTest();
            Console.ReadLine();
        }

        [STAThread]
        static void Main_Graph(string[] args)
        {
            var test = new GraphTest();

            SimpleGraph graph = test.CreatePlanarGraph90();
            //var graph = test.CreateNotPlanarGraph();

            var nodes = test.MinimizeNodes(graph).Nodes;

            Console.WriteLine(nodes.Count);
            foreach (var node in nodes)
            {
                Console.WriteLine(node.ToString());
            }
            Console.ReadLine();
        }

        //static void Main(string[] args)
        //{
        //    //var number = 0.010;
        //    var number = 80.0555;
        //    var fractLength = number.FractionString().Length;
        //    var (wholeNumber, fractNumber) = number.Split();
        //    Console.WriteLine("The fractLength is: " + fractLength);
        //    Console.WriteLine("The wholeNumber is: " + wholeNumber);
        //    Console.WriteLine("The fractNumber is: " + fractNumber);
        //    //GetFractionTest.Run();
        //    Console.ReadLine();
        //}
    }
}
