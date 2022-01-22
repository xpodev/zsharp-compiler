using System;
using System.Collections;
using System.Collections.Generic;

namespace ZSharp.Engine
{
    internal class DependencyGraph<T> : IEnumerable<KeyValuePair<T, IReadOnlySet<T>>>
    {
        private readonly Dictionary<T, HashSet<T>> _graph = new();

        private enum NodeStatus
        {
            NotVisited,
            Visiting,
            Visited
        }

        private HashSet<T> GetOrCreateDependencyList(T dependant)
        {
            if (!_graph.TryGetValue(dependant, out HashSet<T> deps))
            {
                _graph.Add(dependant, deps = new());
            }
            return deps;
        }

        public void AddDependency(T dependant, T dependency)
        {
            AddDependencies(dependant, dependency);
        }

        public void AddDependencies(T dependant, params T[] dependencies)
        {
            AddDependencies(dependant, (IEnumerable<T>)dependencies);
        }

        public void AddDependencies(T dependant, IEnumerable<T> dependencies)
        {
            GetOrCreateDependencyList(dependant).UnionWith(dependencies);
        }

        public bool Contains(T item) => _graph.ContainsKey(item);

        public bool IsDirectDependency(T dependant, T dependency)
        {
            if (_graph.TryGetValue(dependant, out HashSet<T> dependencies))
                return dependencies.Contains(dependency);
            return false;
        }

        public bool IsDependency(T dependant, T dependency)
        {
            if (IsDirectDependency(dependant, dependency)) return true;
            foreach (T item in _graph[dependant])
            {
                if (IsDependency(dependant, item)) return true;
            }
            return false;
        }

        public IEnumerable<IEnumerable<T>> GetDependencyOrder()
        {
            Dictionary<T, int> levels = new();
            Dictionary<T, NodeStatus> visited = new();

            foreach (T dependant in _graph.Keys)
            {
                levels.Add(dependant, default);
                visited.Add(dependant, default);
            }

            void UpdateDependencyLevel(T dependant)
            {
                int GetMaxDependencyLevel(T dependant)
                {
                    // if we already have this dependant level, return it
                    if (visited[dependant] is NodeStatus.Visited) return levels[dependant];
                    visited[dependant] = NodeStatus.Visiting;

                    // find the max dependency level of this dependant's dependencies
                    int maxDependencyLevel = default;
                    T maxDependency = default;
                    foreach (T dependency in _graph[dependant])
                    {
                        if (Equals(dependant, dependency))
                            throw new Exception($"Circular dependency found: {dependant} depends on itself!");

                        int dependencyLevel;
                        try
                        {
                            dependencyLevel = visited[dependency] switch
                            {
                                NodeStatus.NotVisited => GetMaxDependencyLevel(dependency),
                                NodeStatus.Visited => levels[dependency],
                                NodeStatus.Visiting => throw new CircularDependencyException<T>(),
                                _ => throw new Exception($"Invalid node status: {visited[dependency]}")
                            };
                        } catch (CircularDependencyException<T> ex)
                        {
                            ex.Chain.Insert(0, dependency);
                            throw ex;
                        }

                        if (dependencyLevel > maxDependencyLevel)
                        {
                            maxDependency = dependency;
                            maxDependencyLevel = dependencyLevel;
                        }
                    }
                    if (!Equals(maxDependency, default(T)) && maxDependencyLevel <= levels[dependant])
                        throw new Exception($"Circular dependency found: {dependant} <-> {maxDependency}");
                    visited[dependant] = NodeStatus.Visited;
                    return levels[dependant] = ++maxDependencyLevel;
                }

                try
                {
                    GetMaxDependencyLevel(dependant);
                } catch (CircularDependencyException<T> ex)
                {
                    ex.Chain.Insert(0, dependant);
                    throw ex;
                }
            }

            SortedList<int, List<T>> ordered = new();

            foreach (T dependant in _graph.Keys)
            {
                UpdateDependencyLevel(dependant);
            }

            foreach (KeyValuePair<T, int> pair in levels)
            {
                if (!ordered.ContainsKey(pair.Value))
                    ordered.Add(pair.Value, new());
                ordered[pair.Value].Add(pair.Key);
            }

            return ordered.Values;
        }

        public IEnumerator<KeyValuePair<T, IReadOnlySet<T>>> GetEnumerator()
        {
            foreach (KeyValuePair<T, HashSet<T>> item in _graph)
            {
                yield return new(item.Key, item.Value);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
