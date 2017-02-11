using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessServices;
using BusinessEntities;

namespace FitnessClub.Controllers
{
    [RoutePrefix("api/Users" )]
    public class UserController : ApiController
    {

        private readonly IUserServices _UserServices;

        public UserController(IUserServices UserServices)
        {
            //_UserServices = new UserServices();
            _UserServices = UserServices;
        }
        // GET: api/User
        [Route("")]
        public HttpResponseMessage Get()
        {
            var Users = _UserServices.GetAllUsers();
            if (Users != null)
            {
                var UserEntities = Users as List<UserEntity>?? Users.ToList();
                if (UserEntities.Any())
                    return Request.CreateResponse(HttpStatusCode.OK, UserEntities);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Users not found");
        }

        // GET: api/User/5
        [Route("{id:int}")]
        public HttpResponseMessage Get(int id)
        {
            var User = _UserServices.GetUserById(id);
            if (User != null)
              return Request.CreateResponse(HttpStatusCode.OK, User);              
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No User found for this id");
        }

        // POST: api/User
        public int Post([FromBody]UserEntity UserEntity)
        {
            return _UserServices.CreateUser(UserEntity);
        }

        // PUT: api/User/5
        public bool Put(int id, [FromBody]UserEntity UserEntity)
        {
            if (id > 0)
            {
                return _UserServices.UpdateUser(id,UserEntity);
            }
            return false;
        }

        // DELETE: api/User/5
        public bool Delete(int id)
        {
            if (id > 0)
            {
                return _UserServices.DeleteUser(id);
            }
            return false;
        }
    }
}
