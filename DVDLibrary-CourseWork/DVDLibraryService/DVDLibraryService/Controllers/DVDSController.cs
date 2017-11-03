using DVDLibraryService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DVDLibraryService.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DVDSController : ApiController
    {
       
        
        [Route("dvds/")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetAll()
        {
            IRepository repo = RepoFactory.Create();
            List<DVDListView> DVDs = repo.GetAll().ToList();
            return Ok(DVDs);
        }

        [Route("dvd/{id}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetByID(int id)
        {
            IRepository repo = RepoFactory.Create();
            DVDDetailView selected = repo.GetByID(id);
            if (selected == null)
            {
                return NotFound();
            }
            return Ok(selected);
        }
      //  [EnableCors(origins: "*", headers: "*", methods: "post")]
        [Route("dvd/")]
        [AcceptVerbs("POST","GET")]
        public IHttpActionResult AddDVD(DVD ToAdd)
        {
            if (!string.IsNullOrWhiteSpace(ToAdd.Title)&&!string.IsNullOrWhiteSpace(ToAdd.Director)&& ToAdd.realeaseYear>0) 
            {
                IRepository repo = RepoFactory.Create();
                repo.AddDVD(ToAdd);
                return Created($"DVD/{ToAdd.DVDID}", ToAdd);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [Route("dvd/{id}")]
        [AcceptVerbs("DELETE")]
        public void DeleteDvd(int id)
        {
            IRepository repo = RepoFactory.Create();
            repo.DeleteDvD(id);

        }
        [Route("dvd/{id}")]
        [AcceptVerbs("PUT")]
        public IHttpActionResult UpdateDVD(int id, DVD ToEdit)
        {
            if (ModelState.IsValid)
            {
                IRepository repo = RepoFactory.Create();
                repo.EditDVD(ToEdit);

                return Ok(repo.GetByID(id));
            }
            else
            {
                return BadRequest(ModelState);
            }

        }





    }
}
