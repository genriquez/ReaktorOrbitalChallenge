using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            foreach (var node in nodes)
            {
                //var remainingNodes = nodes.SkipWhile(n => n != node).Skip(1);
                node.ResolveReachableNodes(nodes);
            }
        }

        public IEnumerable<CommunicationsNode> Nodes { get; private set; }

        public CommunicationsNode OriginNode { get; private set; }

        public CommunicationsNode DestinationNode { get; private set; }
    }
}
