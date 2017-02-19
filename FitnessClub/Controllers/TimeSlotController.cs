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
    [RoutePrefix("api/TimeSlots")]
    public class TimeSlotController : ApiController
    {

        private readonly ITimeSlotServices _TimeSlotServices;

        public TimeSlotController(ITimeSlotServices TimeSlotServices)
        {
            //_TimeSlotServices = new TimeSlotServices();
            _TimeSlotServices = TimeSlotServices;
        }
        // GET: api/TimeSlot
        [Route("")]
        public HttpResponseMessage Get()
        {
            var TimeSlots = _TimeSlotServices.GetAllTimeSlots();
            if (TimeSlots != null)
            {
                var TimeSlotEntities = TimeSlots as List<TimeSlotEntity> ?? TimeSlots.ToList();
                if (TimeSlotEntities.Any())
                    return Request.CreateResponse(HttpStatusCode.OK, TimeSlotEntities);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "TimeSlots not found");
        }

        // GET: api/TimeSlot/5
        [Route("{id:int}")]
        public HttpResponseMessage Get(int id)
        {
            var TimeSlot = _TimeSlotServices.GetTimeSlotById(id);
            if (TimeSlot != null)
                return Request.CreateResponse(HttpStatusCode.OK, TimeSlot);
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No TimeSlot found for this id");
        }

        // POST: api/TimeSlot
        public int Post([FromBody]TimeSlotEntity TimeSlotEntity)
        {
            return _TimeSlotServices.CreateTimeSlot(TimeSlotEntity);
        }

        // PUT: api/TimeSlot/5
        public bool Put(int id, [FromBody]TimeSlotEntity TimeSlotEntity)
        {
            if (id > 0)
            {
                return _TimeSlotServices.UpdateTimeSlot(id, TimeSlotEntity);
            }
            return false;
        }

        // DELETE: api/TimeSlot/5
        public bool Delete(int id)
        {
            if (id > 0)
            {
                return _TimeSlotServices.DeleteTimeSlot(id);
            }
            return false;
        }
    }
}
