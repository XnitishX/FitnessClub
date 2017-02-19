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
    [RoutePrefix("api/RegTrainings")]
    public class RegTrainingController : ApiController
    {

        private readonly IRegTrainingServices _RegTrainingServices;

        public RegTrainingController(IRegTrainingServices RegTrainingServices)
        {
            //_RegTrainingServices = new RegTrainingServices();
            _RegTrainingServices = RegTrainingServices;
        }
        // GET: api/RegTraining
        [Route("")]
        public HttpResponseMessage Get()
        {
            var RegTrainings = _RegTrainingServices.GetAllRegTrainings();
            if (RegTrainings != null)
            {
                var RegTrainingEntities = RegTrainings as List<RegTrainingEntity> ?? RegTrainings.ToList();
                if (RegTrainingEntities.Any())
                    return Request.CreateResponse(HttpStatusCode.OK, RegTrainingEntities);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "RegTrainings not found");
        }

        // GET: api/RegTraining/5
        [Route("{id:int}")]
        public HttpResponseMessage Get(int id)
        {
            var RegTraining = _RegTrainingServices.GetRegTrainingById(id);
            if (RegTraining != null)
                return Request.CreateResponse(HttpStatusCode.OK, RegTraining);
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No RegTraining found for this id");
        }

        // POST: api/RegTraining
        public int Post([FromBody]RegTrainingEntity RegTrainingEntity)
        {
            return _RegTrainingServices.CreateRegTraining(RegTrainingEntity);
        }

        // PUT: api/RegTraining/5
        public bool Put(int id, [FromBody]RegTrainingEntity RegTrainingEntity)
        {
            if (id > 0)
            {
                return _RegTrainingServices.UpdateRegTraining(id, RegTrainingEntity);
            }
            return false;
        }

        // DELETE: api/RegTraining/5
        public bool Delete(int id)
        {
            if (id > 0)
            {
                return _RegTrainingServices.DeleteRegTraining(id);
            }
            return false;
        }
    }
}
