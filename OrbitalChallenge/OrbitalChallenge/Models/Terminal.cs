using System.Collections.Generic;

namespace OrbitalChallenge.Models
{
    class Terminal : Node
    {
        public Terminal(string name, double longitude, double latitude)
            : base(name, longitude, latitude, 0)
        {
        }
    }
}
