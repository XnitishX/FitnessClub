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
    [RoutePrefix("api/Registration")]
    public class RegistrationController : ApiController
    {

        private readonly IRegistrationServices _RegistrationServices;

        public RegistrationController(IRegistrationServices RegistrationServices)
        {
            _RegistrationServices = RegistrationServices;
        }
        // GET: api/Registration
        [Route("")]
        public HttpResponseMessage Get()
        {
            var Registrations = _RegistrationServices.GetAllRegistrations();
            if (Registrations != null)
            {
                var RegistrationEntities = Registrations as List<RegistrationEntity> ?? Registrations.ToList();
                if (RegistrationEntities.Any())
                    return Request.CreateResponse(HttpStatusCode.OK, RegistrationEntities);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Registrations not found");
        }

        // GET: api/Registration/5
        [Route("{id:int}")]
        public HttpResponseMessage Get(int id)
        {
            var Registration = _RegistrationServices.GetRegistrationById(id);
            if (Registration != null)
                return Request.CreateResponse(HttpStatusCode.OK, Registration);
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Registration found for this id");
        }

        // POST: api/Registration
        public int Post([FromBody]RegistrationEntity RegistrationEntity)
        {
            return _RegistrationServices.CreateRegistration(RegistrationEntity);
        }

        // PUT: api/Registration/5
        public bool Put(int id, [FromBody]RegistrationEntity RegistrationEntity)
        {
            if (id > 0)
            {
                return _RegistrationServices.UpdateRegistration(id, RegistrationEntity);
            }
            return false;
        }

        // DELETE: api/Registration/5
        public bool Delete(int id)
        {
            if (id > 0)
            {
                return _RegistrationServices.DeleteRegistration(id);
            }
            return false;
        }
    }
}
