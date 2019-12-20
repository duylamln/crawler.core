using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Crawler.API.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Crawler.API.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChemistWarehouseController : ControllerBase
    {
        private const string _searchUrl = "https://www.chemistwarehouse.com.au/cmsglobalfiles/handlers/predictive_search.ashx?term=";

        private HttpClient _httpClient;
        public ChemistWarehouseController()
        {
            _httpClient = new HttpClient();
        }

        // GET api/values
        [HttpGet]
        [Route("search/{term}")]
        public async Task<ActionResult> Search(string term)
        {
            if (string.IsNullOrEmpty(term)) return NotFound();
            var response = await _httpClient.GetAsync(_searchUrl + term);
            if (response.IsSuccessStatusCode)
            {
                 return Ok(await response.Content.ReadAsAsync<IEnumerable<CWPreditiveSearchResponse>>());
            }
            return NotFound();
        }
    }
}
