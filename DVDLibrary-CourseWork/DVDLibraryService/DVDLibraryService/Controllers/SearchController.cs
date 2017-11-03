using DVDLibraryService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DVDLibraryService.Controllers
{
    public class SearchController : ApiController
    {
        [HttpGet]
        [Route("search/{category}/{searchterm}")]
        public IHttpActionResult FindByQuery(string category, string searchTerm)
        {
            IRepository repo = RepoFactory.Create();
            List<DVDDetailView> results = new List<DVDDetailView>();
            switch (category.ToLower())
            {

               case "title":
                    results = repo.FindByTitle(searchTerm).ToList();
                    break;
                case "director":
                    results = repo.FindByDirector(searchTerm).ToList();
                    break;
                case "realeaseyear":
                    results = repo.FindbyYear(int.Parse(searchTerm)).ToList();
                    break;
            }
            return Ok(results);
        }
    }
}
