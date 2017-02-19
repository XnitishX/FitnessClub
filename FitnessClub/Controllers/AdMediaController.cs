using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessServices;
using BusinessEntities;
using FitnessClub.Filters;
using FitnessClub.ActionFilters;
using WebApi.Filters;

namespace FitnessClub.Controllers
{
    [AuthorizationRequired]
    [RoutePrefix("api/AdMedias")]
    public class AdMediaController : ApiController
    {

        private readonly IAdMediaServices _AdMediaServices;

        public AdMediaController(IAdMediaServices AdMediaServices)
        {
            //_AdMediaServices = new AdMediaServices();
            _AdMediaServices = AdMediaServices;
        }
        // GET: api/AdMedia
        [Route("")]
        public HttpResponseMessage Get()
        {
            var AdMedias = _AdMediaServices.GetAllAdMedia();
            if (AdMedias != null)
            {
                var AdMediaEntities = AdMedias as List<AdMediaEntity> ?? AdMedias.ToList();
                if (AdMediaEntities.Any())
                    return Request.CreateResponse(HttpStatusCode.OK, AdMediaEntities);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "AdMedias not found");
        }

        // GET: api/AdMedia/5
        [Route("{id:int}")]
        public HttpResponseMessage Get(int id)
        {
            var AdMedia = _AdMediaServices.GetAdMediaById(id);
            if (AdMedia != null)
                return Request.CreateResponse(HttpStatusCode.OK, AdMedia);
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No AdMedia found for this id");
        }

        // POST: api/AdMedia
        public int Post([FromBody]AdMediaEntity AdMediaEntity)
        {
            return _AdMediaServices.CreateAdMedia(AdMediaEntity);
        }

        // PUT: api/AdMedia/5
        public bool Put(int id, [FromBody]AdMediaEntity AdMediaEntity)
        {
            if (id > 0)
            {
                return _AdMediaServices.UpdateAdMedia(id, AdMediaEntity);
            }
            return false;
        }

        // DELETE: api/AdMedia/5
        public bool Delete(int id)
        {
            if (id > 0)
            {
                return _AdMediaServices.DeleteAdMedia(id);
            }
            return false;
        }
    }
}
