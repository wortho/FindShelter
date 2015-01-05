using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindShelter.UdINaturenService
{
    public class SearchResult
    {
        public string BBox;

        public int Count;

        public int SearchResultID;

        public int SearchType;

        public SearchResult(string bBox, int count, int searchResultID, int searchType)
        {
            this.BBox = bBox;
            this.Count = count;
            this.SearchResultID = searchResultID;
            this.SearchType = searchType;
        }
    }
}
