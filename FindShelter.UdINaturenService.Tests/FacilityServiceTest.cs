using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

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

            var facilities = await sut.GetFacilities();

            Assert.IsNotNull(facilities);
        }

        [TestMethod]
        public async Task GetFacility()
        {
            var sut = new FacilityService();
            var facilities = await sut.GetFacilities();
            Assert.IsNotNull(facilities);
            Assert.IsTrue(facilities.Count() > 0);
            var expected = facilities.First();
            var actual = await sut.GetFacility(expected.Id);
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Id, actual.Id);
        }
    }
}
