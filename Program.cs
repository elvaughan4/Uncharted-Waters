using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Uncharted_Waters
{
    class Program
    {
        static void Main(string[] args)
        {
            //initilize graph
                int[,,] graph = GetSatelliteData();

            //determine density of submaires on surface level
                double surfaceDensity = 0;
                int surfaceLevel = 0;
                surfaceDensity = Density(graph, surfaceLevel);
                Console.WriteLine("Surface sub density\t\t- {0}", surfaceDensity);
            
            //determine density of submarines on underwater level
                int underwaterLevel = 1;
                double underwaterDensity = Density(graph, underwaterLevel);
                Console.WriteLine("Underwater sub density\t\t- {0}", underwaterDensity);

            //determine density of submaines on deepwater level
                int deepwaterLevel = 2;
                double deepwaterDensity = Density(graph, deepwaterLevel);
                Console.WriteLine("Deepwater sub density\t\t- {0}", deepwaterDensity);

            //determine ratio of US subs to enemy subs
                double ratio = Ratio(graph);
                Console.WriteLine("Sub / Enemy Sub ratio\t\t- {0}", ratio);

            //determine if US subs should attack on surface level
                bool attackSurface = Attack(graph, 0);
                Console.WriteLine("Go for surface attack\t\t- {0}", attackSurface);

            //determine is US subs should attack on underwater level
                bool attackUnderwater = Attack(graph, 1);
                Console.WriteLine("Go for underwater attack\t- {0}", attackUnderwater);

            //determine if US subs should attack on deepwater level
                bool attackDeepwater = Attack(graph, 2);
                Console.WriteLine("Go for deepwater attack\t\t- {0}", attackDeepwater);
                Console.WriteLine("");

            //display surface graph
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("SURFACE");
                DisplyGraph(graph, 0);
                Console.WriteLine("");

            //display underwater graph
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("UNDERWATER");
                DisplyGraph(graph, 1);
                Console.WriteLine("");

            //display deepwater graph
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("DEEPWATER");
                DisplyGraph(graph, 2);

            //reset console forecolor
                Console.ResetColor();

        }//end main

        static int [,,] GetSatelliteData()
        {
            Random rand = new Random();
            int[,,] data = new int[10, 10, 3];
            for (int z = 0; z < data.GetLength(2); z++)
            {
                for (int y = 0; y < data.GetLength(1); y++)
                {
                    for (int x = 0; x < data.GetLength(0); x++)
                    {
                        if (rand.Next(0, 101) < 25)
                        {
                            data[x, y, z] = rand.Next(1, 3);
                        }
                    }
                }
            }
            return data;
        }//end GetSatelliteData()

        static double Density(int[,,] array, int level)
        {
            double num = 0;
            for (int x = 0; x < array.GetLength(0); x++)
            {
                for (int y = 0; y < array.GetLength(1); y++)
                {
                    for (int z = 0; z < array.GetLength(2); z++)
                    {
                        if (array[x, y, level] == 1 || array[x, y, level] == 2) num++;
                    }
                }
            }
            return num / (array.GetLength(0) * array.GetLength(1));
        }//end Density()

        static double Ratio(int[,,] array)
        {
            double USNavy = 0;
            double losers = 0;

            for (int x = 0; x < array.GetLength(0); x++)
            {
                for (int y = 0; y < array.GetLength(1); y++)
                {
                    for (int z = 0; z < array.GetLength(2); z++)
                    {
                        if (array[x, y, z] == 1) USNavy++;
                        if (array[x, y, z] == 2) losers++;
                    }
                }
            }

            return USNavy / losers;
        }//end density()

        static bool Attack(int [,,] array, int level)
        {
            int USNavy = 0;
            int losers = 0;

            for (int x = 0; x < array.GetLength(0); x++)
            {
                for (int y = 0; y < array.GetLength(1); y++)
                {
                        if (array[x, y, level] == 1) USNavy++;
                        if (array[x, y, level] == 2) losers++; 
                }
            }

            if (USNavy > losers) return true;
            else return false;
        }//end Attack()

        static void DisplyGraph(int[,,] array, int level)
        {
            for (int x = 0; x < array.GetLength(0); x++)
            {
                for (int y = 0; y < array.GetLength(1); y++)
                {
                    Console.Write("{0} ", array[x, y, level]);
                }
                Console.WriteLine("");
            }
        }//end DisplayGraph
    }
}
