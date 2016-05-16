using System.Linq;
using System.Collections.Generic;
using OrbitalChallenge.MathUtils;
using OrbitalChallenge.Models;
using System;

namespace OrbitalChallenge.Resolvers
{
    class CommunicationsNode
    {
        private List<CommunicationsNode> reachableNodes = new List<CommunicationsNode>();

        /// <summary>
        /// Minimum distance to a node to evaluate elevation angle to check for reachability
        /// </summary>
        private double earthHorizonMagnitudeInKilometers;

        /// <summary>
        /// Minimum elevation angle to a node beyond the horizon magnitude for it to be reachable
        /// </summary>
        private double earthHorizonElevationInRadians;

        public CommunicationsNode(Node node)
        {
            this.Name = node.Name;
            this.Position = Vector3.FromGeodeticCoordinates(node.Longitude, node.Latitude, node.Altitude);

            this.CalculateEarthHorizonParameters(node.Altitude == 0);
        }

        public string Name { get; set; }

        public Vector3 Position { get; private set; }

        public IEnumerable<CommunicationsNode> ReachableNodes { get { return this.reachableNodes; } }

        public void ResolveReachableNodes(IEnumerable<CommunicationsNode> nodes)
        {
            foreach (var node in nodes)
            {
                var distanceToNode = node.Position - this.Position;

                                  // Node is within the earth horizon radius, visibility is guaranteed as it can't be behind earth
                var isReachable = distanceToNode.Magnitude < this.earthHorizonMagnitudeInKilometers ||

                                  // Elevation angle to other node is above earth horizon, thus not behind earth
                                  Vector3.AngleBetween(-this.Position, distanceToNode) > this.earthHorizonElevationInRadians;

                if (isReachable)
                {   
                    this.reachableNodes.Add(node);
                    node.reachableNodes.Add(this);
                }
            }

            // Clear any potential duplicates added from other nodes 
            this.reachableNodes = this.reachableNodes.Distinct().ToList();
        }

        private void CalculateEarthHorizonParameters(bool isOnSurface)
        {
            if (isOnSurface)
            {
                this.earthHorizonElevationInRadians = Math.PI / 2;
                this.earthHorizonMagnitudeInKilometers = 0;
            }
            else
            {
                // Angle between the position and horizon vectors originated from the center of the planet
                var horizonSurfacePointAngle = Math.Acos(Constants.EarthRadiusInKilometers / this.Position.Magnitude);

                // Elevation angle of the node pointing towards the horizon point on the surface of the planet
                this.earthHorizonElevationInRadians = Math.PI / 2 - horizonSurfacePointAngle;

                this.earthHorizonMagnitudeInKilometers = this.Position.Magnitude * Math.Cos(this.earthHorizonElevationInRadians);
            }
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
