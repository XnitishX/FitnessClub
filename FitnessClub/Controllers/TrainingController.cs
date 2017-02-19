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
    [RoutePrefix("api/Trainings")]
    public class TrainingController : ApiController
    {

        private readonly ITrainingServices _TrainingServices;

        public TrainingController(ITrainingServices TrainingServices)
        {
            //_TrainingServices = new TrainingServices();
            _TrainingServices = TrainingServices;
        }
        // GET: api/Training
        [Route("")]
        public HttpResponseMessage Get()
        {
            var Trainings = _TrainingServices.GetAllTrainings();
            if (Trainings != null)
            {
                var TrainingEntities = Trainings as List<TrainingEntity> ?? Trainings.ToList();
                if (TrainingEntities.Any())
                    return Request.CreateResponse(HttpStatusCode.OK, TrainingEntities);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Trainings not found");
        }

        // GET: api/Training/5
        [Route("{id:int}")]
        public HttpResponseMessage Get(int id)
        {
            var Training = _TrainingServices.GetTrainingById(id);
            if (Training != null)
                return Request.CreateResponse(HttpStatusCode.OK, Training);
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Training found for this id");
        }

        // POST: api/Training
        public int Post([FromBody]TrainingEntity TrainingEntity)
        {
            return _TrainingServices.CreateTraining(TrainingEntity);
        }

        // PUT: api/Training/5
        public bool Put(int id, [FromBody]TrainingEntity TrainingEntity)
        {
            if (id > 0)
            {
                return _TrainingServices.UpdateTraining(id, TrainingEntity);
            }
            return false;
        }

        // DELETE: api/Training/5
        public bool Delete(int id)
        {
            if (id > 0)
            {
                return _TrainingServices.DeleteTraining(id);
            }
            return false;
        }
    }
}
