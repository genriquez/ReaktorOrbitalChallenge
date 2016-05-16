using System;
using System.Linq;
using OrbitalChallenge.Parsers;
using OrbitalChallenge.Resolvers;

namespace OrbitalChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            // Step 1: Parse the data into POCOs
            var data = new OrbitalDataParser().ParseOrbitalData("orbitaldata.txt");

            // Step 2: Build graph of nodes based on node visibility
            var mesh = new CommunicationsMesh(data);

            // Step 3: Check all possible routes and use the shortest one
            var route = CommunicationsRouteResolver.ResolveRoute(mesh.OriginNode, mesh.DestinationNode);

            if (route != null)
            {
                Console.WriteLine(String.Join(",", route.Select(r => r.Name).ToArray()));
            }
            else
            {
                Console.WriteLine("No route found");
            }
        }
    }
}
