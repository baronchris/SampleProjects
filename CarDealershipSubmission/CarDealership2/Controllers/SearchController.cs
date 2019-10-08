using CarDealership.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CarDealership2.Controllers
{
    public class SearchController : ApiController
    {

        [HttpGet]
        [Route("search/{searchterm}")]
        public IHttpActionResult QuickSearch(string searchterm)
        {
            List<CarDetailed> Cars = new List<CarDetailed>();
            CarRepoADO repo = new CarRepoADO();
            int searchyear = 0;
            bool IsYear = int.TryParse(searchterm, out searchyear);
            if (!IsYear || searchyear<1900)
            {
                Cars = repo.GetCarsByQuickSearch(searchterm);
            }
            else
            {
                Cars = repo.GetCarsByYear(searchyear, null);
            }
                return Ok(Cars);
        }
        [HttpGet]
        [Route("search/price/{min}/{max}")]
        public IHttpActionResult SearchByPriceRange(decimal min, decimal max)
        {
            List<CarDetailed> Cars = new List<CarDetailed>();
            CarRepoADO repo = new CarRepoADO();
            Cars = repo.GetCarsbyPrice(min, max);
            return Ok(Cars);
        }
        [HttpGet]
        [Route("search/price/{min}")]
        public IHttpActionResult SearchByPriceRange(decimal min)
        {
            List<CarDetailed> Cars = new List<CarDetailed>();
            CarRepoADO repo = new CarRepoADO();
            Cars = repo.GetCarsbyPrice(min, null);
            return Ok(Cars);
        }
        [HttpGet]
        [Route("search/year/{syear}/{endYear}")]
        public IHttpActionResult YearSearch(int syear, int endyear)
        {
            List<CarDetailed> Cars = new List<CarDetailed>();
            CarRepoADO repo = new CarRepoADO();
            Cars = repo.GetCarsByYear(syear, endyear);
            return Ok(Cars);
        }
        [HttpGet]
        [Route("search/year/{syear}")]
        public IHttpActionResult YearSearch(int syear)
        {
            List<CarDetailed> Cars = new List<CarDetailed>();
            CarRepoADO repo = new CarRepoADO();
            Cars = repo.GetCarsByYear(syear, null);
            return Ok(Cars);
        }
    }
}
