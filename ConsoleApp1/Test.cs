using DS.ClassLib.VarUtils.Graphs;
using QuickGraph;
using QuickGraph.Algorithms;
using QuickGraph.Algorithms.Search;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace ConsoleApp1
{
    class Test
    {
        private static DepthFirstSearchAlgorithm<IVertex, Edge<IVertex>> _dfs;

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
            var v0 = new TaggedGVertex<Point3d>(0, new Point3d());
            var v1 = new TaggedGVertex<Point3d>(1, new Point3d(1, 0, 0));
            var v2 = new TaggedGVertex<Point3d>(2, new Point3d(2, 0, 0));
            var v3 = new TaggedGVertex<Point3d>(3, new Point3d(3, 0, 0));
            var v4 = new TaggedGVertex<Point3d>(4, new Point3d(4, 0, 0));
            var v5 = new TaggedGVertex<Point3d>(5, new Point3d(0, 0, 1));
            var v6 = new TaggedGVertex<Point3d>(6, new Point3d(0, 0, -1));

            var edges = new[]
            {
                new Edge<IVertex>(v1, v2),
                new Edge<IVertex>(v2, v6),
                new Edge<IVertex>(v1, v3),
                new Edge<IVertex>(v3, v4),
                new Edge<IVertex>(v0, v5),
                new Edge<IVertex>(v0, v1),
            };

            //var v5 = new TaggedGVertex<int>(4, 123456);
            //var v6 = new TaggedGVertex<int>(5,  567869);

            var graph = edges.ToAdjacencyGraph<IVertex, Edge<IVertex>>();

            //graph.AddEdge(new Edge<IVertex>(v3, v5));
            //graph.AddEdge(new Edge<IVertex>(v3, v6));


            graph.TryGetOutEdges(v3, out IEnumerable<Edge<IVertex>> outEdges);

            foreach (var vertex in graph.Vertices)
            {
                foreach (var edge in graph.OutEdges(vertex))
                {
                    Console.WriteLine(edge.Source.Id + "->" + edge.Target.Id);
                    //if(edge.Target is TaggedGVertex<int> tagged) { Console.WriteLine("tag: " + tagged.Tag); }
                }
            }

            int vd = graph.OutDegree(v1);
            Console.WriteLine("\nv1 outDegree = " + vd);

            //Test1(graph);
            Test2(graph);
        }


        private static void Test1(AdjacencyGraph<IVertex, Edge<IVertex>> graph)
        {
            var testVertex = graph.Vertices.FirstOrDefault(v => v.Id == 1);

            var bdGraph = graph.ToBidirectionalGraph<IVertex, Edge<IVertex>>();
            var inEdges = bdGraph.InEdges(testVertex);
            var outEges = bdGraph.OutEdges(testVertex);

            var s = graph.Sinks();

            var b = graph.TreeBreadthFirstSearch(testVertex);


            //var vc = graph.TopologicalSort();
            //var vbd = bdGraph.TopologicalSort();

            Dictionary<IVertex, Edge<IVertex>> verticesPredecessors;
            void TreeEdgeHandler(object sender, Edge<IVertex> edge)
            {
                verticesPredecessors[edge.Target] = edge;
            }

            var edges = new List<Edge<IVertex>>();

            _dfs = new DepthFirstSearchAlgorithm<IVertex, Edge<IVertex>>(graph);

            _dfs.DiscoverVertex += Dfs_DiscoverVertex;
            //_dfs.TreeEdge += _dfs_TreeEdge;
            _dfs.Compute();
            var v2 = graph.Vertices.ToArray()[1];
            _dfs.SetRootVertex(v2);
            _dfs.Compute();

            var black = _dfs.VertexColors.Where(c => c.Value == GraphColor.Black).ToList();

            //var aj = bdGraph.ToArrayAdjacencyGraph();

            //Console.WriteLine("Test vertex is: " + testVertex.Id);
            //Console.WriteLine(inEdges.Count());
            //Console.WriteLine(outEges.Count());
            void _dfs_TreeEdge(Edge<IVertex> e)
            {
                edges.Add(e);
            }
        }


        private static void Test2(AdjacencyGraph<IVertex, Edge<IVertex>> graph)
        {
            var bdGraph = graph.ToBidirectionalGraph();

            var algorithm = new BreadthFirstSearchAlgorithm<IVertex, Edge<IVertex>>(bdGraph);
            //var algorithm = new DepthFirstSearchAlgorithm<IVertex, Edge<IVertex>>(bdGraph);
            var iteratror = new GraphVertexIterator(algorithm);

            var txt = "Current vertex id: ";
            while (iteratror.MoveNext())
            {
                Debug.WriteLine(txt + iteratror.Current.ToString());
            }
        }


        private static void Dfs_DiscoverVertex(IVertex vertex)
        {
            Debug.WriteLine("Discovered vertex: " + vertex.Id);
            _currentVertex = vertex;
            _dfs.Abort();
        }

        private static IVertex _currentVertex;
    }

    class MyClass
    {
        public int MyProperty { get; set; } = 1000;

    }
}
