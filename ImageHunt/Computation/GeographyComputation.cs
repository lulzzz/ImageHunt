using System;
using System.Collections.Generic;
using System.Linq;
using ImageHunt.Model;
using ImageHunt.Model.Node;

namespace ImageHunt.Computation
{
    public static class GeographyComputation
    {
        private const double EarthRayon = 6378137;
        public static double Distance(this Node point1, Node point2)
        {
          return Distance((point1.Latitude, point1.Longitude), (point2.Latitude, point2.Longitude));
        }
        public static (double, double) DegToRad((double, double)degCoordinates)
        {
            return (Math.PI * degCoordinates.Item1 / 180.0, Math.PI * degCoordinates.Item2 / 180.0);
        }

      public static (double, double) CenterOfGeoPoints(IEnumerable<(double, double)> points)
      {
        var radPoints = new List<(double, double)>();
        // convert points in radian
        foreach (var point in points)
        {
          radPoints.Add(DegToRad(point));
        }
        var cartesianPoints = new List<(double, double, double)>();
        foreach (var point in radPoints)
        {
          cartesianPoints.Add(
            (Math.Cos(point.Item1) * Math.Cos(point.Item2),
             Math.Cos(point.Item1) * Math.Sin(point.Item2),
             Math.Sin(point.Item1)));
        }
        var average = cartesianPoints.Aggregate((A, B) => (A.Item1 + B.Item1, A.Item2 + B.Item2, A.Item3 + B.Item3));
        average.Item1 /= cartesianPoints.Count;
        average.Item2 /= cartesianPoints.Count;
        average.Item3 /= cartesianPoints.Count;
        var hyp = Math.Sqrt(average.Item1 * average.Item1 + average.Item2 * average.Item2);
        return RadToDeg((Math.Atan2(average.Item3, hyp), Math.Atan2(average.Item2, average.Item1)));
      }

      public static (double, double) RadToDeg((double, double) radCoordinates)
      {
        return (radCoordinates.Item1 * 180.0 / Math.PI, radCoordinates.Item2 * 180.0 / Math.PI);
      }

    public static double Distance((double, double) point1, (double, double) point2)
    {
      var radPoint1 = DegToRad(point1);
      var radPoint2 = DegToRad(point2);
      return Math.Acos(Math.Sin(radPoint1.Item1) * Math.Sin(radPoint2.Item1) + Math.Cos(radPoint1.Item1) *
                       Math.Cos(radPoint2.Item1) * Math.Cos(radPoint2.Item2 - radPoint1.Item2)) * EarthRayon;
    }
  }
}
