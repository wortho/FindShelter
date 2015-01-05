using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FindShelter.Web.Controllers;
using FindShelter.Model;
using System.Web.Http;
using System.Threading.Tasks;
using System.Web.Http.Results;

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
            var sut = new FacilitiesController();
            Assert.IsNotNull(sut);
        }

        [TestMethod]
        public async Task GetFacilityReturnsActionResult()
        {
            var sut = new FacilitiesController();
            IHttpActionResult contentResult = await sut.GetFacility(0);
            Assert.IsNotNull(contentResult);
        }

        [TestMethod]
        public async Task GetFacilityReturnsEnumerableFacilites()
        {
            var sut = new FacilitiesController();

            IHttpActionResult actionResult = await sut.GetFacility(0);

            var contentResult = actionResult as OkNegotiatedContentResult<Facility>;
            Assert.IsNotNull(contentResult);

            var facilites = contentResult.Content;

            Assert.IsTrue(facilites is Facility);
        }

        [TestMethod]
        public async Task GetFacilitiesReturnsActionResult()
        {
            var sut = new FacilitiesController();
            IHttpActionResult contentResult = await sut.GetFacilities();
            Assert.IsNotNull(contentResult);
        }

        [TestMethod]
        public async Task GetFacilitiesReturnsEnumerableFacilites()
        {
            var sut = new FacilitiesController();

            IHttpActionResult actionResult = await sut.GetFacilities();
            
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<Facility>>;
            Assert.IsNotNull(contentResult);

            var facilites = contentResult.Content;

            Assert.IsTrue(facilites is IEnumerable<Facility>);
        }
    }
}
