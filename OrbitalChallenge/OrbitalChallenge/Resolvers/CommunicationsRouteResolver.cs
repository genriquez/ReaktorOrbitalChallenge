using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrbitalChallenge.Resolvers
{
    class CommunicationsRouteResolver
    {
        public static IEnumerable<CommunicationsNode> ResolveRoute(CommunicationsNode origin, CommunicationsNode destination)
        {
            return ResolveRoute(Enumerable.Empty<CommunicationsNode>(), origin, destination);
        }

        private static IEnumerable<CommunicationsNode> ResolveRoute(IEnumerable<CommunicationsNode> currentRoute, CommunicationsNode currentNode, CommunicationsNode destination)
        {
            currentRoute = currentRoute.Concat(new[] { currentNode });
            if (currentNode == destination)
            {
                return currentRoute;
            }

            foreach (var node in currentNode.ReachableNodes.Except(currentRoute))
            {
                var route = ResolveRoute(currentRoute, node, destination);
                if (route != null)
                {   // A route was found, complete the search
                    return route;
                }
            }

            // No route was found
            return null;
        }
    }
}
