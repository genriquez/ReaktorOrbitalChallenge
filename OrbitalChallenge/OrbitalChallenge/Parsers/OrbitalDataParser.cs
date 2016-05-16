using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using OrbitalChallenge.Models;

namespace OrbitalChallenge.Parsers
{
    class OrbitalDataParser
    {
        /// <summary>
        /// EN-US format provider is used as all decimal numbers are provided with dot decimal separator
        /// </summary>
        private static readonly IFormatProvider FormatProvider = new CultureInfo("en-us");

        private Dictionary<string, Action<string>> lineParsers = new Dictionary<string, Action<string>>();

        private List<Satellite> satellites = new List<Satellite>();
        private Terminal origin;
        private Terminal destination;

        public OrbitalDataParser()
        {
            // Line parsers are evaluated checking the begining of each line
            this.lineParsers.Add("SAT", line => this.ParseSatellite(line));
            this.lineParsers.Add("ROUTE", line => this.ParseRoute(line));
        }

        public OrbitalData ParseOrbitalData(string filepath)
        {
            var fileLines = this.ReadDataFileLines(filepath);

            foreach (var line in fileLines)
            {
                var parserKey = this.lineParsers.Select(lp => lp.Key).FirstOrDefault(lpk => line.StartsWith(lpk));
                if (parserKey != null)
                {
                    this.lineParsers[parserKey](line);
                }
            }

            return new OrbitalData
            {
                Origin = this.origin,
                Destination = this.destination,
                Satellites = this.satellites
            };
        }

        private IEnumerable<string> ReadDataFileLines(string filepath)
        {
            using (var reader = File.OpenText(filepath))
            {
                while (!reader.EndOfStream)
                {   // No need to load whole file into memory as string at once
                    yield return reader.ReadLine();
                }
            }
        }

        private void ParseSatellite(string line)
        {
            var values = line.Split(',');
            this.satellites.Add(new Satellite(values[0], ParseDouble(values[2]), ParseDouble(values[1]), ParseDouble(values[3])));
        }

        private void ParseRoute(string line)
        {
            var values = line.Split(',');

            this.origin = new Terminal("ORIGIN", ParseDouble(values[2]), ParseDouble(values[1]));
            this.destination = new Terminal("DESTINATION", ParseDouble(values[4]), ParseDouble(values[3]));
        }

        private static double ParseDouble(string input)
        {
            return double.Parse(input, FormatProvider);
        }
    }
}
