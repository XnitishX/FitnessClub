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
    [RoutePrefix("api/Goals")]
    public class GoalController : ApiController
    {

        private readonly IGoalServices _GoalServices;

        public GoalController(IGoalServices GoalServices)
        {
            //_GoalServices = new GoalServices();
            _GoalServices = GoalServices;
        }
        // GET: api/Goal
        [Route("")]
        public HttpResponseMessage Get()
        {
            var Goals = _GoalServices.GetAllGoals();
            if (Goals != null)
            {
                var GoalEntities = Goals as List<GoalEntity> ?? Goals.ToList();
                if (GoalEntities.Any())
                    return Request.CreateResponse(HttpStatusCode.OK, GoalEntities);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Goals not found");
        }

        // GET: api/Goal/5
        [Route("{id:int}")]
        public HttpResponseMessage Get(int id)
        {
            var Goal = _GoalServices.GetGoalById(id);
            if (Goal != null)
                return Request.CreateResponse(HttpStatusCode.OK, Goal);
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Goal found for this id");
        }

        // POST: api/Goal
        public int Post([FromBody]GoalEntity GoalEntity)
        {
            return _GoalServices.CreateGoal(GoalEntity);
        }

        // PUT: api/Goal/5
        public bool Put(int id, [FromBody]GoalEntity GoalEntity)
        {
            if (id > 0)
            {
                return _GoalServices.UpdateGoal(id, GoalEntity);
            }
            return false;
        }

        // DELETE: api/Goal/5
        public bool Delete(int id)
        {
            if (id > 0)
            {
                return _GoalServices.DeleteGoal(id);
            }
            return false;
        }
    }
}
