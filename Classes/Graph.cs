using System;

namespace Dijekstra.Classes
{
    class Graph
    {
        List<Vertex> Vertexes = new List<Vertex>();
        List<Edge> Edges = new List<Edge>();

        public int VertexCount => Vertexes.Count;
        public int EdgeCount => Edges.Count;

        class DijkstraData
        {
            public Vertex Previous { get; set; }
            public int Price { get; set; }
        }

        public void Display()
        {
            foreach (var vertex in Vertexes)
                Console.Write(" " + vertex.ToString());
            Console.WriteLine();

            foreach (var edge in Edges)
            {
                Console.Write(edge.From + " " + edge.To + " " + edge.Weight);
                Console.WriteLine();
            }
        }

        public void AddVertex(Vertex vertex)
        {
            Vertexes.Add(vertex);
        }

        public void AddEdge(Vertex from, Vertex to, int weight)
        {
            var edge = new Edge(from, to, weight);
            Edges.Add(edge);
        }

        public List<Vertex> GetVertexLists(Vertex vertex)
        {
            var result = new List<Vertex>();

            foreach (var edge in Edges)
            {
                if (edge.From.Number == vertex.Number)
                {
                    result.Add(edge.To);
                }
            }
            return result;
        }

        public List<Edge> GetEdgeLists(Vertex vertex)
        {
            var result = new List<Edge>();

            foreach (var edge in Edges)
            {
                if (edge.From.Number == vertex.Number)
                {
                    result.Add(edge);
                }
            }
            return result;
        }

        public List<Vertex> FindShortestPath(Vertex start, Vertex end)
        {
            var notVisited = new List<Vertex>();
            notVisited = Vertexes.GetRange(0, Vertexes.Count);

            var track = new Dictionary<int, DijkstraData>();
            track[start.Number] = new DijkstraData { Previous = null, Price = 0 };

            while (true)
            {
                Vertex toOpen = null;
                int bestPrice = int.MaxValue;
                foreach (var v in notVisited)
                {
                    if (track.ContainsKey(v.Number) && track[v.Number].Price < bestPrice)
                    {
                        toOpen = v;
                        bestPrice = track[v.Number].Price;
                    }
                }
                if (toOpen == null) return null;
                if (toOpen.Number == end.Number) break;
                foreach (var e in GetEdgeLists(toOpen))
                {
                    var currentPrice = track[toOpen.Number].Price + e.Weight;
                    var nextVertex = e.To;
                    if (!track.ContainsKey(nextVertex.Number) || track[nextVertex.Number].Price > currentPrice)
                        track[nextVertex.Number] = new DijkstraData { Price = currentPrice, Previous = toOpen };
                }
                notVisited.Remove(toOpen);
            }
            var result = new List<Vertex>();
            while (end != null)
            {
                result.Add(end);
                end = track[end.Number].Previous;
            }
            result.Reverse();
            return result;
        }

        public int GetLengthEdge(Vertex start, Vertex end)
        {
            foreach (var _vertex1 in Vertexes)
            {
                if (_vertex1.Number == start.Number)
                    foreach (var _vertex2 in Vertexes)
                    {
                        if (end.Number == _vertex2.Number)
                            foreach (var _edge in Edges)
                            {
                                if (_edge.From.Number == _vertex1.Number && _edge.To.Number == _vertex2.Number)
                                {
                                    return _edge.Weight;
                                }
                            }
                    }
            }
            return -1;
        }

        public int GetRouteLength(List<Vertex> _vertexes)
        {
            int result = 0;

            for (int i = 1; i < _vertexes.Count; i++)
            {
                result += GetLengthEdge(_vertexes[i - 1], _vertexes[i]);
            }

            return result;
        }

        public void PrintPath(List<Vertex> _vertexes)
        {
            Console.WriteLine();

            if (_vertexes == null)
            {
                Console.WriteLine("No path");
                return;
            }

            var _lengthRoute = GetRouteLength(_vertexes);

            Console.Write("Shortest route");
            foreach (var vertex in _vertexes)
                Console.Write(" => |{0}|", vertex);

            Console.WriteLine("\nLength of the routh: {0}", _lengthRoute);
        }
    }
}
