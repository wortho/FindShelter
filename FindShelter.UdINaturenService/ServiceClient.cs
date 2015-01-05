using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FindShelter.UdINaturenService
{
    class ServiceClient
    {

        /// <summary>
        /// FindFacilities/{LanguageID}/{SubCategoryIDList}/{RouteMinLength}/{RouteMaxLength}?bbox={BBox}&attributeIdList={AttributeIDList}
        /// </summary>
        /// <param name="languageID"></param>
        /// <param name="box"></param>
        /// <returns></returns>
        public async Task<SearchResult> FindFacilities(int languageID, string box)
        {
            HttpClient httpClient = new HttpClient();
            Uri resourceUri = new Uri(string.Format("http://udinaturen.naturstyrelsen.dk/wcf/Service.svc/json/FindFacilities/{0}/38,40,41/0/0?bbox={1}",languageID, box));
            HttpResponseMessage response = await httpClient.GetAsync(resourceUri);
            response.EnsureSuccessStatusCode();
            string responseString = await response.Content.ReadAsStringAsync();
            SearchResult result = JsonConvert.DeserializeObject<SearchResult>(responseString);
            return result;
        }

        public async Task<List<SearchResultItem>> GetSearchResultItems(int searchResultId)
        {
            HttpClient httpClient = new HttpClient();
            Uri resourceUri = new Uri(string.Format("http://udinaturen.naturstyrelsen.dk/wcf/Service.svc/json/GetSearchResultItems/{0}/0/100/1/1", searchResultId));
            HttpResponseMessage response = await httpClient.GetAsync(resourceUri);
            response.EnsureSuccessStatusCode();
            string responseString = await response.Content.ReadAsStringAsync();
            SearchResultItemListContainer container = JsonConvert.DeserializeObject<SearchResultItemListContainer>(responseString);
            List<SearchResultItem> results = new List<SearchResultItem>(container.SearchResultItemList);
            return results;
        }
        public async Task<Facility> GetFacility(int languageID, int facilityID)
        {
            HttpClient httpClient = new HttpClient();
            Uri resourceUri = new Uri(string.Format("http://udinaturen.naturstyrelsen.dk/wcf/Service.svc/json/GetFacility/{0}/{1}", languageID, facilityID));
            HttpResponseMessage response = await httpClient.GetAsync(resourceUri);
            response.EnsureSuccessStatusCode();
            string responseString = await response.Content.ReadAsStringAsync();
            Facility result = JsonConvert.DeserializeObject<Facility>(responseString);
            return result;
        }
    }
}
