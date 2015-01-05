using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using FindShelter.UdINaturenService;
using System.Collections.Generic;

namespace FindShelter.UdINaturenService.Tests
{
    [TestClass]
    public class UdiNaturenServiceTests
    {
        [TestMethod]
        public void CreateUdiNaturenLib()
        {
            ServiceClient svc = new ServiceClient();
        }
        
        [TestMethod]
        public async Task FindFacilities()
        {
            ServiceClient svc = new ServiceClient();
            SearchResult result = await svc.FindFacilities(1,"436351,6045231,1073190,6423994");
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.SearchType);
        }

        [TestMethod]
        public async Task GetSearchResultItems()
        {
            ServiceClient svc = new ServiceClient();
            SearchResult findResult = await svc.FindFacilities(1,"720175, 6213286, 710175, 6113286");
            List<SearchResultItem> result = await svc.GetSearchResultItems(findResult.SearchResultID);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetFacility()
        {
            ServiceClient svc = new ServiceClient();
            int facilityId = 5216;
            Facility actual = await svc.GetFacility(1, facilityId);
            Assert.IsNotNull(actual);
            Assert.AreEqual(facilityId, actual.FacilityID);
        }

        [TestMethod]
        public async Task GetInvalidFacilityReturnsNullFacility()
        {
            ServiceClient svc = new ServiceClient();
            int facilityId = -1;
            Facility actual = await svc.GetFacility(1, facilityId);
            Assert.IsNotNull(actual);
            Assert.AreEqual(0, actual.FacilityID);
        }
    }
}
