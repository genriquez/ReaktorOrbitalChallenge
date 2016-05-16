using System.Collections.Generic;
using System.Linq;
using OrbitalChallenge.Models;

namespace OrbitalChallenge.Resolvers
{
    class CommunicationsMesh
    {
        public CommunicationsMesh(OrbitalData orbitalData)
        {
            var nodes = orbitalData.Satellites.Select(s => new CommunicationsNode(s)).ToList();
            nodes.Add(this.OriginNode = new CommunicationsNode(orbitalData.Origin));
            nodes.Add(this.DestinationNode = new CommunicationsNode(orbitalData.Destination));

            // Origin and Destination nodes are part of the mesh, and should compute visibility with satellites
            this.Nodes = nodes;

            foreach (var node in nodes)
            {   // No need to check for reachability with nodes in past iterations of this loop
                var remainingNodes = nodes.SkipWhile(n => n != node).Skip(1);
                node.ResolveReachableNodes(remainingNodes);
            }
        }

        public IEnumerable<CommunicationsNode> Nodes { get; private set; }

        public CommunicationsNode OriginNode { get; private set; }

        public CommunicationsNode DestinationNode { get; private set; }
    }
}
