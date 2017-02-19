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
    [RoutePrefix("api/ImageDetails")]
    public class ImageDetailController : ApiController
    {

        private readonly IImageDetailServices _ImageDetailServices;

        public ImageDetailController(IImageDetailServices ImageDetailServices)
        {
            //_ImageDetailServices = new ImageDetailServices();
            _ImageDetailServices = ImageDetailServices;
        }
        // GET: api/ImageDetail
        [Route("")]
        public HttpResponseMessage Get()
        {
            var ImageDetails = _ImageDetailServices.GetAllImageDetail();
            if (ImageDetails != null)
            {
                var ImageDetailEntities = ImageDetails as List<ImageDetailEntity> ?? ImageDetails.ToList();
                if (ImageDetailEntities.Any())
                    return Request.CreateResponse(HttpStatusCode.OK, ImageDetailEntities);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "ImageDetails not found");
        }

        // GET: api/ImageDetail/5
        [Route("{id:int}")]
        public HttpResponseMessage Get(int id)
        {
            var ImageDetail = _ImageDetailServices.GetImageDetailById(id);
            if (ImageDetail != null)
                return Request.CreateResponse(HttpStatusCode.OK, ImageDetail);
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No ImageDetail found for this id");
        }

        // POST: api/ImageDetail
        public int Post([FromBody]ImageDetailEntity ImageDetailEntity)
        {
            return _ImageDetailServices.CreateImageDetail(ImageDetailEntity);
        }

        // PUT: api/ImageDetail/5
        public bool Put(int id, [FromBody]ImageDetailEntity ImageDetailEntity)
        {
            if (id > 0)
            {
                return _ImageDetailServices.UpdateImageDetail(id, ImageDetailEntity);
            }
            return false;
        }

        // DELETE: api/ImageDetail/5
        public bool Delete(int id)
        {
            if (id > 0)
            {
                return _ImageDetailServices.DeleteImageDetail(id);
            }
            return false;
        }
    }
}
