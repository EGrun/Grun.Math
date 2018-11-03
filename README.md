[![Build status](https://ci.appveyor.com/api/projects/status/qbtvpwwh7ok54fik/branch/master?svg=true)](https://ci.appveyor.com/project/EGrun/grun-math/branch/master)
[![Build Status](https://travis-ci.org/EGrun/Grun.Math.svg?branch=master)](https://travis-ci.org/EGrun/Grun.Math)

# Grun.Math

A set of Graph computations where a graph is defined as a source vertex
and a function which computes the adjacency list for a vertex.
This definition of graph is useful in situations where computing the entire graph
is computationally or memory intensive or in distributed computing scenarios.
Since edges are not precomputed, an algorithm may be shortcircuited in a more
efficient manner than if the entire graph were loaded into memory.

Define graph G:=(V,E) where:
    
set V of vertices in G
```int[] V = { 0, 1, 2 };```
    
set E of edges in G
```var E = new(int v1, int v2)[] {(0,1), (0, 2)};```
    
adjacency computation - define graph as function
```var lookup = E.ToLookup(e => e.v1, e => e.v2);
Func<int, IEnumerable<int>> graph = (v) => lookup[v];
    
Assert.Equal(false, graph.HasCycle(0));```
