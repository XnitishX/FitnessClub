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
    [RoutePrefix("api/RegGoalss")]
    public class RegGoalsController : ApiController
    {

        private readonly IRegGoalsServices _RegGoalsServices;

        public RegGoalsController(IRegGoalsServices RegGoalsServices)
        {
            //_RegGoalsServices = new RegGoalsServices();
            _RegGoalsServices = RegGoalsServices;
        }
        // GET: api/RegGoals
        [Route("")]
        public HttpResponseMessage Get()
        {
            var RegGoalss = _RegGoalsServices.GetAllRegGoals();
            if (RegGoalss != null)
            {
                var RegGoalsEntities = RegGoalss as List<RegGoalsEntity> ?? RegGoalss.ToList();
                if (RegGoalsEntities.Any())
                    return Request.CreateResponse(HttpStatusCode.OK, RegGoalsEntities);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "RegGoalss not found");
        }

        // GET: api/RegGoals/5
        [Route("{id:int}")]
        public HttpResponseMessage Get(int id)
        {
            var RegGoals = _RegGoalsServices.GetRegGoalsById(id);
            if (RegGoals != null)
                return Request.CreateResponse(HttpStatusCode.OK, RegGoals);
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No RegGoals found for this id");
        }

        // POST: api/RegGoals
        public int Post([FromBody]RegGoalsEntity RegGoalsEntity)
        {
            return _RegGoalsServices.CreateRegGoals(RegGoalsEntity);
        }

        // PUT: api/RegGoals/5
        public bool Put(int id, [FromBody]RegGoalsEntity RegGoalsEntity)
        {
            if (id > 0)
            {
                return _RegGoalsServices.UpdateRegGoals(id, RegGoalsEntity);
            }
            return false;
        }

        // DELETE: api/RegGoals/5
        public bool Delete(int id)
        {
            if (id > 0)
            {
                return _RegGoalsServices.DeleteRegGoals(id);
            }
            return false;
        }
    }
}
