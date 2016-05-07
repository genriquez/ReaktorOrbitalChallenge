using System;

namespace OrbitalChallenge.MathUtils
{
    class Vector3
    {
        public Vector3(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public double X { get; private set; }

        public double Y { get; private set; }

        public double Z { get; private set; }

        public double Magnitude { get { return System.Math.Sqrt(X * X + Y * Y + Z * Z); } }

        public Vector3 Normalize
        {
            get
            {
                var sqrMgtd = Math.Sqrt(this.Magnitude);
                return new Vector3(X / sqrMgtd, Y / sqrMgtd, Z / sqrMgtd);
            }
        }

        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static Vector3 operator -(Vector3 a)
        {
            return new Vector3(-a.X, -a.Y, -a.Z);
        }

        public static Vector3 FromAngularCoordinates(double rotation, double elevation, double magnitude)
        {
            var z = Math.Sin(elevation / 180 * Math.PI) * magnitude;
            var xy = Math.Cos(elevation / 180 * Math.PI) * magnitude;
            var x = xy * Math.Cos(rotation / 180 * Math.PI);
            var y = xy * Math.Sin(rotation / 180 * Math.PI);

            return new Vector3(x, y, z);
        }

        public static Vector3 FromGeodeticCoordinates(double lon, double lat, double altitude)
        {   // Based on http://mathforum.org/library/drmath/view/51832.html
            // f is 0, as the earth is considered perfectly round
            // s is = to c, as f is 0

            var a = Constants.EarthRadiusInKilometers;
            var h = altitude;
            var s = 1 / Math.Sqrt(Math.Pow(Math.Cos(Math.PI * lat / 180), 2) + Math.Pow(Math.Sin(Math.PI * lat / 180), 2));

            var x = (a * s + h) * Math.Cos(lat) * Math.Cos(lon);
            var y = (a * s + h) * Math.Cos(lat) * Math.Sin(lon);
            var z = (a * s + h) * Math.Sin(lat);

            return new Vector3(x, y, z);
        }

        public static double AngleBetween(Vector3 a, Vector3 b)
        {
            a = a.Normalize;
            b = b.Normalize;

            var dotProduct = a.X * b.X + a.Y * b.Y + a.Z * b.Z;

            return Math.Acos(dotProduct / (a.Magnitude * b.Magnitude));
        }
    }
}
