using FindShelter.Model;
using ProjNet.CoordinateSystems;
using ProjNet.CoordinateSystems.Transformations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindShelter.GeoConverter
{
    public class Converter
    {
        public static int GetZone(double longitude)
        {
            return (int)System.Math.Ceiling((longitude + 180) / 6);
        }

        public static char GetBand(double latitude)
        {
            if (latitude <= 84 && latitude >= 72)
                return 'X';
            else if (latitude < 72 && latitude >= 64)
                return 'W';
            else if (latitude < 64 && latitude >= 56)
                return 'V';
            else if (latitude < 56 && latitude >= 48)
                return 'U';
            else if (latitude < 48 && latitude >= 40)
                return 'T';
            else if (latitude < 40 && latitude >= 32)
                return 'S';
            else if (latitude < 32 && latitude >= 24)
                return 'R';
            else if (latitude < 24 && latitude >= 16)
                return 'Q';
            else if (latitude < 16 && latitude >= 8)
                return 'P';
            else if (latitude < 8 && latitude >= 0)
                return 'N';
            else if (latitude < 0 && latitude >= -8)
                return 'M';
            else if (latitude < -8 && latitude >= -16)
                return 'L';
            else if (latitude < -16 && latitude >= -24)
                return 'K';
            else if (latitude < -24 && latitude >= -32)
                return 'J';
            else if (latitude < -32 && latitude >= -40)
                return 'H';
            else if (latitude < -40 && latitude >= -48)
                return 'G';
            else if (latitude < -48 && latitude >= -56)
                return 'F';
            else if (latitude < -56 && latitude >= -64)
                return 'E';
            else if (latitude < -64 && latitude >= -72)
                return 'D';
            else if (latitude < -72 && latitude >= -80)
                return 'C';
            else
                throw new ArgumentOutOfRangeException("latitude");
        }

        public static UTMCoordinate ConvertWGS84ToUtm(GeoCoordinate geo)
        {
            return ConvertWGS84ToUtm(GetZone(geo.Longitude), GetBand(geo.Latitude), geo);
        }
        public static UTMCoordinate ConvertWGS84ToUtm(int zone, char band, GeoCoordinate geo)
        {
            //Transform to UTM
            bool IsNorthHemisphere = geo.Latitude > 0;
            ICoordinateTransformation trans = CreateUTMTransform(zone, IsNorthHemisphere);
            double[] point = trans.MathTransform.Transform(new double[] { geo.Longitude, geo.Latitude });
            return new UTMCoordinate(zone, band, (int)point[0], (int)point[1]);

        }

        private static ICoordinateTransformation CreateUTMTransform(int zone, bool IsNorthHemisphere)
        {
            CoordinateTransformationFactory ctfac = new CoordinateTransformationFactory();
            ICoordinateSystem wgs84geo = ProjNet.CoordinateSystems.GeographicCoordinateSystem.WGS84;
            ICoordinateSystem utm = ProjNet.CoordinateSystems.ProjectedCoordinateSystem.WGS84_UTM(zone, IsNorthHemisphere);
            ICoordinateTransformation trans = ctfac.CreateFromCoordinateSystems(wgs84geo, utm);
            return trans;
        }

        public static GeoCoordinate ConvertUtmToWGS84(UTMCoordinate utm)
        {
            //Transform to WGS84
            bool IsNorthHemisphere = IsZoneNorth(utm.Band);
            ICoordinateTransformation trans = CreateUTMTransform(utm.Zone, IsNorthHemisphere);
            double[] point = trans.MathTransform.Inverse().Transform(new double[] { utm.Easting, utm.Northing });
            return new GeoCoordinate(point[1], point[0]);

        }

        private static bool IsZoneNorth(char band)
        {
            return (band.CompareTo('M') > 0);
        }
    }
}
