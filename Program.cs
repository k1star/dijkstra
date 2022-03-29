using System;


namespace Dijekstra
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var graph = new Classes.Graph();

            Console.Write("Enter a path to file: ");
            string pathFile = Console.ReadLine();

            try
            {
                using (Classes.ExcelHelper helper = new Classes.ExcelHelper())
                {
                    if (helper.Open(pathFile: Path.Combine(Environment.CurrentDirectory, pathFile)))
                    {
                        int colNumber = helper.ColNumber(),
                            rowNumber = helper.RowNumber();

                        for (int i = 1; i <= colNumber; i++)
                        {
                            var vertex = new Classes.Vertex(int.Parse(helper.ReadVertex(i)));
                            graph.AddVertex(vertex);
                        }

                        string firstVertex, secondVertex, longRoute;

                        for (int i = 4; i <= rowNumber; i++)
                        {
                            firstVertex = helper.ReadCell(i, 2);
                            secondVertex = helper.ReadCell(i, 3);
                            longRoute = helper.ReadCell(i, 4);

                            var v1 = new Classes.Vertex(int.Parse(firstVertex));
                            var v2 = new Classes.Vertex(int.Parse(secondVertex));

                            graph.AddEdge(v1, v2, int.Parse(longRoute));
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error with opening file or searching path: {0}", pathFile);
                        Console.ReadKey();
                        Environment.Exit(0);
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                Environment.Exit(0);
            }
          
            int firstV = 0, secondV = 0;

            try
            {
                Console.Write("Data loaded succesually!\n\nEnter a first vertex: ");
                firstV = int.Parse(Console.ReadLine());

                Console.Write("Enter a second vertex: ");
                secondV = int.Parse(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                Environment.Exit(0);
            }
            if (firstV == secondV || firstV < 1 || secondV < 1 || firstV > graph.VertexCount || secondV > graph.VertexCount)
            {
                Console.WriteLine("Error! You have entered a wrong data.");
                Console.ReadKey();
                Environment.Exit(0);
            }

            var vertexStart = new Classes.Vertex(firstV);
            var vertexEnd = new Classes.Vertex(secondV);

            var vertexes = new List<Classes.Vertex>();
            vertexes = graph.FindShortestPath(vertexStart, vertexEnd);

            graph.PrintPath(vertexes);

            Console.WriteLine();
            Console.ReadLine();
        }
    }
}