using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrbitalChallenge.Parsers;
using OrbitalChallenge.Resolvers;

namespace OrbitalChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            var parser = new OrbitalDataParser();
            var data = parser.ParseOrbitalData("orbitaldata.txt");

            var mesh = new CommunicationsMesh(data);
            var route = CommunicationsRouteResolver.ResolveRoute(mesh.OriginNode, mesh.DestinationNode);

            if (route != null)
            {
                Console.WriteLine(String.Join(",", route.Select(r => r.Name)));
            }
            else
            {
                Console.WriteLine("No route found");
            }
        }
    }
}
