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
    [RoutePrefix("api/Status")]
    public class StatusController : ApiController
    {

        private readonly IStatusServices _StatusServices;

        public StatusController(IStatusServices StatusServices)
        {
            //_StatusServices = new StatusServices();
            _StatusServices = StatusServices;
        }
        // GET: api/Status
        [Route("")]
        public HttpResponseMessage Get()
        {
            var Statuss = _StatusServices.GetAllStatus();
            if (Statuss != null)
            {
                var StatusEntities = Statuss as List<StatusEntity> ?? Statuss.ToList();
                if (StatusEntities.Any())
                    return Request.CreateResponse(HttpStatusCode.OK, StatusEntities);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Statuss not found");
        }

        // GET: api/Status/5
        [Route("{id:int}")]
        public HttpResponseMessage Get(int id)
        {
            var Status = _StatusServices.GetStatusById(id);
            if (Status != null)
                return Request.CreateResponse(HttpStatusCode.OK, Status);
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Status found for this id");
        }

        // POST: api/Status
        public int Post([FromBody]StatusEntity StatusEntity)
        {
            return _StatusServices.CreateStatus(StatusEntity);
        }

        // PUT: api/Status/5
        public bool Put(int id, [FromBody]StatusEntity StatusEntity)
        {
            if (id > 0)
            {
                return _StatusServices.UpdateStatus(id, StatusEntity);
            }
            return false;
        }

        // DELETE: api/Status/5
        public bool Delete(int id)
        {
            if (id > 0)
            {
                return _StatusServices.DeleteStatus(id);
            }
            return false;
        }
    }
}
