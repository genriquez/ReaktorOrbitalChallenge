using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace OrbitalChallenge.Resolvers
{
    class CommunicationsRouteResolver
    {
        public static IEnumerable<CommunicationsNode> ResolveRoute(CommunicationsNode origin, CommunicationsNode destination)
        {
            return ResolveRoute(Enumerable.Empty<CommunicationsNode>(), origin, destination, Enumerable.Empty<CommunicationsNode>());
        }

        /// <summary>
        /// Recursively check any possible node path combination avoiding duplicate nodes in the same path.
        /// This is a basic depth-first search algorithm, with some tweaks for the current scenario to make the search shorter.
        /// Each valid route is compared with the previous one, and the shortest route is preserved.
        /// </summary>
        private static IEnumerable<CommunicationsNode> ResolveRoute(IEnumerable<CommunicationsNode> currentRoute, CommunicationsNode currentNode, CommunicationsNode destination, IEnumerable<CommunicationsNode> excludeNodes)
        {
            Debug.WriteLine("Resolving for " + currentNode.Name);

            currentRoute = currentRoute.Concat(new[] { currentNode });
            if (currentNode == destination)
            {
                return currentRoute;
            }

            var exclude = excludeNodes.ToList();
            double currentRouteLength = 0;
            IEnumerable<CommunicationsNode> route = null;

            // Nodes within the current route are excluded, as it makes no sense looping over already included nodes
            // Previously iterated nodes are excluded, as all possible routes through those nodes will always be longer than the routes
            //  that go directly to that node in the first place
            foreach (var node in currentNode.ReachableNodes.Except(currentRoute).Except(exclude).ToList())
            {
                var newRoute = ResolveRoute(currentRoute, node, destination, exclude);
                exclude.Add(node);

                if (newRoute != null)
                {   // A route was found, keep it only if it's the shorter than the existing one
                    var newRouteLenght = CalculateRouteLength(newRoute);

                    if (route == null || newRouteLenght < currentRouteLength)
                    {   // The new route is shorter, preserve it
                        route = newRoute;
                        currentRouteLength = newRouteLenght;
                    }
                }
            }

            return route;
        }

        private static double CalculateRouteLength(IEnumerable<CommunicationsNode> route)
        {
            var currentNode = route.First();
            route = route.Skip(1);

            double routeLength = 0;
            foreach(var node in route)
            {
                routeLength += (node.Position - currentNode.Position).Magnitude;
                currentNode = node;
            }

            return routeLength;
        }
    }
}
