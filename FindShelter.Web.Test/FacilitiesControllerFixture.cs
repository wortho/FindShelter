using FindShelter.Model;
using FindShelter.Web.Controllers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindShelter.Web.Test
{
    internal class FacilitiesControllerFixture : IDisposable
    {
        public IFacilityService FacilityService { get; private set; }

        public FacilityBuilder FacilityBuilder { get; private set; }

        internal FacilitiesControllerFixture()
        {
            FacilityBuilder = new FacilityBuilder();
            FacilityService = new Mock<IFacilityService>().Object;
        }

        internal FacilitiesController CreateSUT()
        {
            return new FacilitiesController(FacilityService);
        }

        public void Dispose()
        {

        }
    }
}
