using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FindShelter.Model;

namespace FindShelter.GeoConverter.UnitTests
{
    [TestClass]
    public class ConverterTests
    {
        [TestMethod]
        public void GetZone()
        {
            int result = Converter.GetZone(12.3727);
            Assert.AreEqual(33, result);
        }

        [TestMethod]
        public void GetBand()
        {
            char result = Converter.GetBand(55.8127);
            Assert.AreEqual('U', result);
        }

        [TestMethod]
        public void ConvertWGS84ToUtm()
        {
            UTMCoordinate result = Converter.ConvertWGS84ToUtm(new GeoCoordinate(55.8127, 12.3727));
            Assert.IsNotNull(result);
            Assert.AreEqual(6188357, result.Northing);
            Assert.AreEqual(335370, result.Easting);
            Assert.AreEqual(33, result.Zone);
            Assert.AreEqual('U', result.Band);

        }

        [TestMethod]
        public void ConvertUtmToGeo()
        {
            GeoCoordinate result = Converter.ConvertUtmToWGS84(new UTMCoordinate(33, 'U', 335370, 6188357));
            Assert.IsNotNull(result);
            Assert.AreEqual(55.8127, Math.Round(result.Latitude, 4));
            Assert.AreEqual(12.3727, Math.Round(result.Longitude, 4));
        }

        [TestMethod]
        public void ConvertWGS84ToUtmAndBack()
        {
            GeoCoordinate target = new GeoCoordinate(55.8127, 12.3727);
            int zone = Converter.GetZone(target.Longitude);
            char band = Converter.GetBand(target.Latitude);

            UTMCoordinate resultUtm = Converter.ConvertWGS84ToUtm(target);
            Assert.IsNotNull(resultUtm);

            GeoCoordinate resultWGS84 = Converter.ConvertUtmToWGS84(resultUtm);
            Assert.IsNotNull(resultWGS84);

            const int precision = 4;

            Assert.AreEqual(Math.Round(target.Latitude, precision), Math.Round(resultWGS84.Latitude, precision));
            Assert.AreEqual(Math.Round(target.Longitude, precision), Math.Round(resultWGS84.Longitude, precision));


        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ConvertInvalidLongitude()
        {
            UTMCoordinate resultUtm = Converter.ConvertWGS84ToUtm(new GeoCoordinate(20, -190));
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ConvertInvalidLatitude()
        {
            UTMCoordinate resultUtm = Converter.ConvertWGS84ToUtm(new GeoCoordinate(2000, 120));
        }
    }
}
