using System;
using ImageHunt.Model;
using ImageHunt.Model.Node;

namespace ImageHunt.Computation
{
    public static class GeographyComputation
    {
        private const double EarthRayon = 6378137;
        public static double Distance(this Node point1, Node point2)
        {
            return Math.Acos(Math.Sin(point1.Latitude) * Math.Sin(point2.Latitude) + Math.Cos(point1.Latitude) *
                      Math.Cos(point2.Latitude) * Math.Cos(point2.Longitude - point1.Longitude)) * EarthRayon;
        }
        public static void DegToRad(this Node point)
        {
            point.Longitude = Math.PI * point.Longitude / 180.0;
            point.Latitude = Math.PI * point.Latitude / 180.0;
        }

    }
}
