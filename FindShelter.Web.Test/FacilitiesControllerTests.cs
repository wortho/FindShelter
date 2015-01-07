using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FindShelter.Web.Controllers;
using FindShelter.Model;
using System.Web.Http;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Moq;

namespace FindShelter.Web.Test
{
    /// <summary>
    /// Summary description for FacilitiesControllerTests
    /// </summary>
    [TestClass]
    public class FacilitiesControllerTests
    {

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void CreateFacilitiesController()
        {
            var serviceMock = new Mock<IFacilityService>();
            var sut = new FacilitiesController(serviceMock.Object);
            Assert.IsNotNull(sut);
        }

        [TestMethod]
        public async Task GetFacilityReturnsActionResult()
        {
            var serviceMock = new Mock<IFacilityService>();
            var sut = new FacilitiesController(serviceMock.Object);
            IHttpActionResult contentResult = await sut.GetFacility(0);
            Assert.IsNotNull(contentResult);
        }

        [TestMethod]
        public async Task GetFacility()
        {
            var serviceMock = new Mock<IFacilityService>();
            var sut = new FacilitiesController(serviceMock.Object);
            var expected = new Facility(1, "Name1", "ShortDescription1", "LongDescription1", new GeoCoordinate(1, 0));
            serviceMock.Setup(s => s.GetFacility(1)).ReturnsAsync(expected);
            IHttpActionResult actionResult = await sut.GetFacility(1);
            var contentResult = actionResult as OkNegotiatedContentResult<Facility>;
            Assert.IsNotNull(contentResult);
            var actual = contentResult.Content;
            Assert.IsTrue(actual is Facility);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task GetFacilitiesReturnsActionResult()
        {
            var serviceMock = new Mock<IFacilityService>();
            var sut = new FacilitiesController(serviceMock.Object);
            IHttpActionResult contentResult = await sut.GetFacilities(0, 0, 1, 1);
            var box = new BoundingBox(new GeoCoordinate(0, 0), new GeoCoordinate(1, 1));
            Assert.IsNotNull(contentResult);
            serviceMock.Verify(s => s.GetFacilities(It.IsAny<BoundingBox>()), Times.Once);
        }

        [TestMethod]
        public async Task GetFacilitiesReturnsEnumerableFacilites()
        {
            var serviceMock = new Mock<IFacilityService>();
            var box = new BoundingBox(new GeoCoordinate(0, 0), new GeoCoordinate(1, 1));
            var sut = new FacilitiesController(serviceMock.Object);
            var facilities = new[] { 
                new Facility(1, "Name1", "ShortDescription1","LongDescription1", new GeoCoordinate(1, 0)),
                new Facility(2, "Name2", "ShortDescription2","LongDescription2", new GeoCoordinate(2, 0)),
                new Facility(3, "Name3", "ShortDescription3","LongDescription3", new GeoCoordinate(2, 0))
            };

            serviceMock.Setup(s => s.GetFacilities(It.IsAny<BoundingBox>())).ReturnsAsync(facilities);

            IHttpActionResult actionResult = await sut.GetFacilities(0, 0, 1, 1);

            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<Facility>>;
            Assert.IsNotNull(contentResult);
            var actual = contentResult.Content;
            Assert.IsTrue(actual is IEnumerable<Facility>);
            Assert.AreEqual(facilities[0], actual.First());
        }
    }
}
