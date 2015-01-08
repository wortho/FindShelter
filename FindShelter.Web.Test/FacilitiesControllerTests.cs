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
    [TestClass]
    public class FacilitiesControllerTests
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void FacilitiesControllerHasFacilityService()
        {
            using (var fixture = new FacilitiesControllerFixture())
            {
                var sut = fixture.CreateSUT();
                Assert.IsNotNull(sut.FacilityService);
            }
        }

        [TestMethod]
        public async Task GetFacilityReturnsActionResult()
        {
            using (var fixture = new FacilitiesControllerFixture())
            {
                var sut = fixture.CreateSUT();
                IHttpActionResult contentResult = await sut.GetFacility(0);
                Assert.IsNotNull(contentResult);
                fixture.FacilityService.AsMock().Verify(s => s.GetFacility(0), Times.Once);
            }
        }

        [TestMethod]
        public async Task GetFacilityReturnsNotFoundWithInvalidId()
        {
            using (var fixture = new FacilitiesControllerFixture())
            {
                var sut = fixture.CreateSUT();
                fixture.FacilityService.AsMock().Setup(s => s.GetFacility(-1)).ReturnsAsync(null);
                IHttpActionResult actionResult = await sut.GetFacility(-1);
                Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
            }
        }

        [TestMethod]
        public async Task GetFacilityReturnsFaciltyWithId()
        {
            using (var fixture = new FacilitiesControllerFixture())
            {
                var sut = fixture.CreateSUT();
                Facility expected = fixture.FacilityBuilder.WithId(1);
                fixture.FacilityService.AsMock().Setup(s => s.GetFacility(1)).ReturnsAsync(expected);
                IHttpActionResult actionResult = await sut.GetFacility(1);
                fixture.FacilityService.AsMock().Verify(s => s.GetFacility(1), Times.Once);
                var contentResult = actionResult as OkNegotiatedContentResult<Facility>;
                Assert.IsNotNull(contentResult);
                var actual = contentResult.Content;
                Assert.IsTrue(actual is Facility);
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public async Task GetFacilitiesWithInvalidLocationReturnsBadRequest()
        {
            using (var fixture = new FacilitiesControllerFixture())
            {
                var sut = fixture.CreateSUT();
                IHttpActionResult actionResult = await sut.GetFacilities(95, 190, -1, -1);
                Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
                fixture.FacilityService.AsMock().Verify(s => s.GetFacilities(It.IsAny<BoundingBox>()), Times.Never);
            }
        }

        [TestMethod]
        public async Task GetFacilitiesReturnsActionResult()
        {
            using (var fixture = new FacilitiesControllerFixture())
            {
                var sut = fixture.CreateSUT();
                IHttpActionResult actionResult = await sut.GetFacilities(0, 0, 1, 1);
                var box = new BoundingBox(new GeoCoordinate(0, 0), new GeoCoordinate(1, 1));
                Assert.IsNotNull(actionResult);
                fixture.FacilityService.AsMock().Verify(s => s.GetFacilities(It.IsAny<BoundingBox>()), Times.Once);
            }
        }

        [TestMethod]
        public async Task GetFacilitiesReturnsNotFoundWithNoMatchingFacilites()
        {
            using (var fixture = new FacilitiesControllerFixture())
            {
                var sut = fixture.CreateSUT();
                fixture.FacilityService.AsMock().Setup(s => s.GetFacilities(It.IsAny<BoundingBox>())).ReturnsAsync(null);
                IHttpActionResult actionResult = await sut.GetFacilities(0, 0, 0, 0);
                Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
            }
        }

        [TestMethod]
        public async Task GetFacilitiesReturnsFacilites()
        {
            using (var fixture = new FacilitiesControllerFixture())
            {
                var sut = fixture.CreateSUT();
                var expected = new Facility[]
                { 
                    fixture.FacilityBuilder.WithId(1),
                    fixture.FacilityBuilder.WithId(2),
                    fixture.FacilityBuilder.WithId(3)
                };

                fixture.FacilityService.AsMock().Setup(s => s.GetFacilities(It.IsAny<BoundingBox>())).ReturnsAsync(expected);

                IHttpActionResult actionResult = await sut.GetFacilities(0, 0, 1, 1);
                var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<Facility>>;
                Assert.IsNotNull(contentResult);
                var actual = contentResult.Content;
                Assert.IsTrue(actual is IEnumerable<Facility>);
                Assert.AreEqual(expected.Length, actual.Count());
                Assert.AreEqual(expected.First(), actual.First());
            }
        }
    }
}
