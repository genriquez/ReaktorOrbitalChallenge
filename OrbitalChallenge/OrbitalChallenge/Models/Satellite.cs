using System.Collections.Generic;

namespace OrbitalChallenge.Models
{
    class Satellite : Node
    {
        public Satellite(string name, double longitude, double latitude, double altitude)
            : base(name, longitude, latitude, altitude)
        {
        }
    }
}
