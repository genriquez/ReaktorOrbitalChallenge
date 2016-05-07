using System.Collections.Generic;

namespace OrbitalChallenge.Models
{
    class Node
    {
        public Node(string name, double longitude, double latitude, double altitude)
        {
            this.Name = name;
            this.Latitude = latitude;
            this.Longitude = longitude;
            this.Altitude = altitude;
        }

        public string Name { get; private set; }

        public double Latitude { get; private set; }

        public double Longitude { get; private set; }

        public double Altitude { get; private set; }
    }
}
