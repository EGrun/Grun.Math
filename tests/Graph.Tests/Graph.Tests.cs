using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;

namespace Graph.Tests
{

    public class GraphTests
    {
        public const int MaxHashSetSize = 110921543;

        [Fact]
        public void HasCycle_Null_vertex_does_not_throw_returns_false()
        {
            var digraph = new Digraph<object>(v => new object[0]);
            Assert.Equal(false, digraph.HasCycle(null));
        }

        [Fact]
        public void HasCycle_Null_edges_does_not_throw()
        {
            var digraph = new Digraph<object>(v => null);
            Assert.Equal(false, digraph.HasCycle(new object()));
        }

        [Theory]
        [MemberData(nameof(HasCycle_Dictionary_TestData))]
        public void HasCycle_Dictionary_Tests<T>
            (IDictionary<T, IEnumerable<T>> dataset, T node, bool expected)
        {
            var digraph = new Digraph<T>(v => dataset[v]);
            Assert.Equal(expected, digraph.HasCycle(node));
        }

        public static IEnumerable<object[]> HasCycle_Dictionary_TestData()
        {
            //1-node
            yield return new object[] {
                new Dictionary<int, IEnumerable<int>> {{0, new int[] {}}}, 0, false };

            //1-node with cycle
            yield return new object[] {
                new Dictionary<int, IEnumerable<int>> {{0, new[] {0}}}, 0, true };

            //2-node dipole
            var twoNode = new Dictionary<int, IEnumerable<int>>
            {
                [0] = new[] { 1, 1, 1, 1, 1, 1, 1, 1 },
                [1] = new[] { 0, 0, 0, 0, 0, 0, 0, 0 }
            };

            yield return new object[] { twoNode, 0, true };
            yield return new object[] { twoNode, 1, true };

            //Multi-node
            var multiNode = new Dictionary<int, IEnumerable<int>>
            {
                [0] = new[] { 1, 2 },
                [1] = new int[] { },
                [2] = new int[] { }
            };

            yield return new object[] { multiNode, 0, false };
            yield return new object[] { multiNode, 1, false };
            yield return new object[] { multiNode, 2, false };

            //3-node cycle
            var threeNode = new Dictionary<int, IEnumerable<int>>
            {
                [0] = new[] { 1 },
                [1] = new[] { 2 },
                [2] = new[] { 0 }
            };

            yield return new object[] { threeNode, 0, true };
            yield return new object[] { threeNode, 1, true };
            yield return new object[] { threeNode, 2, true };

            //graph of nodes in cycle and not in cycle
            var threeNode2 = new Dictionary<int, IEnumerable<int>>
            {
                [0] = new[] { 1, 2 },
                [1] = new int[] { },
                [2] = new[] { 0 }
            };

            yield return new object[] { threeNode2, 0, true };
            yield return new object[] { threeNode2, 1, false };
            yield return new object[] { threeNode2, 2, true };

            //disconnected graph
            var disconnectedGraph = new Dictionary<int, IEnumerable<int>>
            {
                [0] = new int[] { },
                [1] = new int[] { },
                [2] = new int[] { }
            };

            yield return new object[] { disconnectedGraph, 0, false };
            yield return new object[] { disconnectedGraph, 1, false };
            yield return new object[] { disconnectedGraph, 2, false };

            //string graph
            var stringsGraph = new Dictionary<string, IEnumerable<string>>
            {
                ["Apple"] = new[] { "Banana", "Canteloupe" },
                ["Banana"] = new string[] { },
                ["Canteloupe"] = new string[] { }
            };

            yield return new object[] { stringsGraph, "Apple", false };
            yield return new object[] { stringsGraph, "Banana", false };
            yield return new object[] { stringsGraph, "Canteloupe", false };

            //string graph with cycle
            var stringsGraphWithCycle = new Dictionary<string, IEnumerable<string>>
            {
                ["Apple"] = new[] { "Banana" },
                ["Banana"] = new[] { "Canteloupe" },
                ["Canteloupe"] = new[] { "Apple" }
            };

            yield return new object[] { stringsGraphWithCycle, "Apple", true };
            yield return new object[] { stringsGraphWithCycle, "Banana", true };
            yield return new object[] { stringsGraphWithCycle, "Canteloupe", true };

            //string graph comparer test
            var comparer = StringComparer.OrdinalIgnoreCase;
            var stringsGraphComparer = new Dictionary<string, IEnumerable<string>>(comparer)
            {
                ["Apple"] = new[] { "BANANA", "CANTELOUPE" },
                ["Banana"] = new[] { "APPLE" },
                ["Canteloupe"] = new[] { "DURIAN" },
                ["Durian"] = new string[] { }
            };

            yield return new object[] { stringsGraphComparer, "Apple", true };
            yield return new object[] { stringsGraphComparer, "Banana", true };
            yield return new object[] { stringsGraphComparer, "Canteloupe", false };
            yield return new object[] { stringsGraphComparer, "Durian", false };

        }

        public static IEnumerable<object[]> GraphSizes()
        {
            yield return new object[] { 1 };
            yield return new object[] { 10 };
            yield return new object[] { 1000 };
            yield return new object[] { MaxHashSetSize };
        }

        [Theory]
        [MemberData(nameof(GraphSizes))]
        public void OneDimensionalGraphTest(int size)
        {
            var digraph = new Digraph<int>(v => v < size - 1 ? new[] { v + 1 } : new int[0]);
            Assert.Equal(false, digraph.HasCycle(0, size));
        }

        [Theory]
        [MemberData(nameof(GraphSizes))]
        public void OneDimensionalCycleGraphTest(int size)
        {
            var digraph = new Digraph<int>(v => v < size - 1 ? new[] { v + 1 } : new[] { 0 });
            Assert.Equal(true, digraph.HasCycle(0, size));
        }

        [Theory]
        [MemberData(nameof(HasCycle_ArrayOfArrays_TestData))]
        public void HasCycle_ArrayOfArrays_Tests(int[][] graph, int node, bool expected)
        {
            var digraph = new Digraph<int>(v => graph[v]);
            Assert.Equal(expected, digraph.HasCycle(node));
        }

        public static IEnumerable<object[]> HasCycle_ArrayOfArrays_TestData()
        {
            //nodes mapped by index in array
            var graphMap = new[]
            {
                new[] {1,3},
                new[] {2},
                new int[] {},
                new[] {0},
            };

            yield return new object[] { graphMap, 0, true };
            yield return new object[] { graphMap, 1, false };
            yield return new object[] { graphMap, 2, false };
            yield return new object[] { graphMap, 3, true };
        }


        [Theory]
        [MemberData(nameof(HasCycle_ValueTupleEdgeSet_TestData))]
        public void HasCycle_ValueTupleEdgeSet_Tests<T>
            (T node, T[] V, (T v1, T v2)[] E, bool expected)
        {
            var lookup = E.ToLookup(e => e.v1, e => e.v2);

            var digraph = new Digraph<T>(v => lookup[v]);
            Assert.Equal(expected, digraph.HasCycle(node));
        }

        public static IEnumerable<object[]> HasCycle_ValueTupleEdgeSet_TestData()
        {
            //set V of vertices in G
            int[] V = { 0, 1, 2, 3 };

            //set E of edges in G
            var E = new(int v1, int v2)[]
            {(0,1), (1, 2), (0,3), (3,0)};

            yield return new object[] { 0, V, E, true };
            yield return new object[] { 1, V, E, false };
            yield return new object[] { 2, V, E, false };
            yield return new object[] { 3, V, E, true };
        }

        [Fact]
        public void DirectedGraph_Generic_Comparer_Tests()
        {

            //String graph ignore case
            var ignoreCaseComparer = StringComparer.OrdinalIgnoreCase;

            var animalSet = new Dictionary<string, IEnumerable<string>>(ignoreCaseComparer)
            {
                ["Angelfish"] = new[] { "cat", "dog" },
                ["Bear"] = new[] { "Angelfish", "Rabbit" },
                ["Camel"] = new string[] { },
                ["CAT"] = new[] { "dog" },
                ["Dog"] = new[] { "cat" },
                ["Rabbit"] = new[] { "CAMEL" }
            };

            var ignoreCaseGraph = new Digraph<string>(v => animalSet[v], ignoreCaseComparer);
            Assert.True(ignoreCaseGraph.HasCycle("Angelfish"));
            Assert.True(ignoreCaseGraph.HasCycle("Bear"));
            Assert.False(ignoreCaseGraph.HasCycle("Camel"));
            Assert.True(ignoreCaseGraph.HasCycle("Cat"));
            Assert.True(ignoreCaseGraph.HasCycle("Dog"));
            Assert.False(ignoreCaseGraph.HasCycle("Rabbit"));

            //maps data into congruent vertices
            const int moduloIndex = 4;
            var congruencyComparer = new CongruencyComparer<int>(moduloIndex);
            int getRandomCongruent(int x)
            {
                var random = new Random();
                return x + moduloIndex * random.Next(1, 1000);
            }

            var graphMap = new[]
            {
                new[] {1,3},
                new[] {2},
                new int[] {},
                new[] {0},
            };

            var arrayGraph = new Digraph<int>(v => graphMap[v % moduloIndex].Select(getRandomCongruent));

            Assert.True(arrayGraph.HasCycle(0));
            Assert.False(arrayGraph.HasCycle(1));
            Assert.False(arrayGraph.HasCycle(2));
            Assert.True(arrayGraph.HasCycle(3));
            Assert.True(arrayGraph.HasCycle(43));

        }

        class CongruencyComparer<T> : IEqualityComparer<T>
        {
            int _modulo;

            public CongruencyComparer(int modulo)
            {
                _modulo = modulo;
            }

            public bool Equals(T x, T y)
            {
                return (GetHashCode(x) - GetHashCode(y)) % _modulo == 0;
            }

            public int GetHashCode(T value)
            {
                return value.GetHashCode();
            }
        }
    }
}
