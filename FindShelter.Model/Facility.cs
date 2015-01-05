using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindShelter.Model
{
    public class Facility
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string LongDescription { get; private set; }
        public GeoCoordinate Location { get; private set; }
        public Facility(int id, string name, string longDescription, GeoCoordinate location)
        {
            this.Name = name;
            this.Id = id;
            this.LongDescription = longDescription;
            this.Location = location;
        }
    }
}
