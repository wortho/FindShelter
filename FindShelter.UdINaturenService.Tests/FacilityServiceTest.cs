using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using FindShelter.Model;

namespace FindShelter.UdINaturenService.Tests
{
    [TestClass]
    public class FacilityServiceTest
    {
        [TestMethod]
        public void FacilityService()
        {
            var sut = new FacilityService();
        }

        [TestMethod]
        public async Task GetFacilities()
        {
            var sut = new FacilityService();

            var box = new BoundingBox(new GeoCoordinate(55.0, 12.0), new GeoCoordinate(56.0, 13.0));

            var facilities = await sut.GetFacilities(box);

            Assert.IsNotNull(facilities);
        }

        [TestMethod]
        public async Task GetFacility()
        {
            var sut = new FacilityService();
            var box = new BoundingBox(new GeoCoordinate(55.0, 12.0), new GeoCoordinate(56.0, 13.0));
            var facilities = await sut.GetFacilities(box);
            Assert.IsNotNull(facilities);
            Assert.IsTrue(facilities.Count() > 0);
            var expected = facilities.First();
            var actual = await sut.GetFacility(expected.Id);
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Id, actual.Id);
        }
    }
}
