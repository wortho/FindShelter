using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindShelter.Model
{
    public interface IFacilityService
    {
        Task<IEnumerable<Facility>> GetFacilities();

        Task<Facility> GetFacility(int id);
    }
}
