using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrbitalChallenge.Models
{
    class OrbitalData
    {
        public Terminal Origin { get; set; }

        public Terminal Destination { get; set; }

        public IEnumerable<Satellite> Satellites { get; set; }
    }
}
