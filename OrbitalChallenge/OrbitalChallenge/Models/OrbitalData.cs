using System.Collections.Generic;

namespace OrbitalChallenge.Models
{
    class OrbitalData
    {
        public Terminal Origin { get; set; }

        public Terminal Destination { get; set; }

        public IEnumerable<Satellite> Satellites { get; set; }
    }
}
