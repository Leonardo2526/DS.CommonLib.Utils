using DS.ClassLib.VarUtils.Graphs;
using QuickGraph;
using QuickGraph.Algorithms;
using QuickGraph.Algorithms.Search;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace ConsoleApp1
{
    class Test
    {
        public static void Run1()
        {
            var c = new MyClass();
            new Edge<MyClass>(c, c);
            var edges = new[] { new Edge<int>(1, 2), new Edge<int>(1, 4), new Edge<int>(0, 6) };
            AdjacencyGraph<int, Edge<int>> graph = edges.ToAdjacencyGraph<int, Edge<int>>();

            int vertex1 = 1;
            int vertex2 = 3;

            graph.AddVertex(vertex1);
            graph.AddVertex(vertex2);

            var edge1 = new TaggedEdge<int, MyClass>(vertex1, vertex2, c);

            graph.AddEdge(edge1);
            int v1 = graph.OutDegree(0);
            Console.WriteLine("e1 = " + v1);

            Edge<int> e1 = graph.OutEdge(1, 2);
            Console.WriteLine("d1 = " + e1);
            foreach (int vertex in graph.Vertices)
            {
                foreach (Edge<int> edge in graph.OutEdges(vertex))
                {
                    Console.WriteLine(edge);
                }
            }



            // Create algorithm
            var dfs = new DepthFirstSearchAlgorithm<int, Edge<int>>(graph);

            // Do the search          
            dfs.Compute();           
        }

        public static void Run2()
        {
            var v1 = new LVertex(0, new Point3d());
            var v2 = new LVertex(1, new Point3d(1,0,0));
            var v3 = new LVertex(2, new Point3d(2,0,0));
            var v4 = new LVertex(3, new Point3d(3,0,0));

            var edges = new[] 
            { 
                new Edge<LVertex>(v1, v2),
                new Edge<LVertex>(v2, v3),
                new Edge<LVertex>(v3, v4),
            };

            var v5 = new TaggedLVertex<int>(4, new Point3d(0, 2, 0), 123456);
            var v6 = new TaggedLVertex<int>(5, new Point3d(0, 0, 1), 567869);

            var graph = edges.ToAdjacencyGraph<LVertex, Edge<LVertex>>();

            graph.AddEdge(new Edge<LVertex>(v3, v5));
            graph.AddEdge(new Edge<LVertex>(v3, v6));

            int vd = graph.OutDegree(v3);
            Console.WriteLine("v3 = " + vd);

            graph.TryGetOutEdges(v3, out IEnumerable<Edge<LVertex>> outEdges);          

            foreach (var vertex in graph.Vertices)
            {
                foreach (var edge in graph.OutEdges(vertex))
                {
                    Console.WriteLine(edge.Source.Id + "->" + edge.Target.Id);
                    if(edge.Target is TaggedLVertex<int> tagged) { Console.WriteLine("tag: " + tagged.Tag); }
                }
            }
        }
    }

    class MyClass
    {
        public int MyProperty { get; set; } = 1000;

    }
}
