


namespace Expeditious.Common
{
    static public class DoubleHelper
    {
        public const double GIS_COORD_TOLERANCE = 0.00000001;

        public static bool AreDoublesEqual(double d1, double d2, double tolerance = GIS_COORD_TOLERANCE)
        {
            // NaN никогда не равен ничему (включая себя)
            if (double.IsNaN(d1) || double.IsNaN(d2))
                return false;

            if (double.IsInfinity(d1) && double.IsInfinity(d2))
                return true;

            return Math.Abs(d1 - d2) <= tolerance;
        }
    }
}