using FindShelter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindShelter.Web.Test
{
    internal class FacilityBuilder
    {
        private int id;
        private string name;
        private string shortDescription;
        private string longDescription;
        private GeoCoordinate location;

        internal FacilityBuilder()
        {
            this.id = 1;
            this.name = "Facility1";
            this.shortDescription = "Short Description";
            this.longDescription = "Long Description";
            this.location = new GeoCoordinate(0, 0);
        }

        internal FacilityBuilder WithId(int id)
        {
            this.id = id;
            return this;
        }

        internal FacilityBuilder WithLocation(GeoCoordinate location)
        {
            this.location = location;
            return this;
        }

        internal Facility Build()
        {
            return new Facility(this.id, this.name, this.shortDescription, this.longDescription, this.location);
        }

        public static implicit operator Facility(FacilityBuilder builder)
        {
            return builder.Build();
        }
    }
}
