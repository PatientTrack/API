using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using API.Models;

namespace API.Controllers
{
    [EnableCors(origins: "http://localhost:55058", headers: "*", methods: "*")]
    public class CarerController : ApiController
    {
        // GET api/carers
        [Route("api/carers")]
        public HttpResponseMessage Get()
        {
            List<Carer> carers = CarerModel.GetAllCarers();
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound);
            if (carers != null)
            {
                response = Request.CreateResponse(HttpStatusCode.OK, carers);
            }            
            return response;
        }

        // GET api/carer/5
        [Route("api/carers/{id?}")]
        public HttpResponseMessage Get(int id)
        {
            Carer carers = CarerModel.GetCarer(id);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound);
            if (carers != null)
            {
                response = Request.CreateResponse(HttpStatusCode.OK, carers);
            }
            return response;
        }

        // GET api/carer/John
        [Route("api/carers/{name:alpha}")]
        public HttpResponseMessage Get(string name)
        {
            Carer carers = CarerModel.SearchCarerByName(name);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound);
            if (carers != null)
            {
                response = Request.CreateResponse(HttpStatusCode.OK, carers);
            }
            return response;
        }

        // POST api/carers
        [Route("api/carers")]
        public HttpResponseMessage Post(Carer c)
        {
            List<Carer> carers = CarerModel.InsertCarer(c);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound);
            if (carers != null)
            {
                response = Request.CreateResponse(HttpStatusCode.OK, carers);
            }
            return response;
        }

        // PUT api/carers
        [Route("api/carers")]
        public HttpResponseMessage Put(Carer c)
        {
            List<Carer> carers = CarerModel.UpdateCarer(c);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound);
            if (carers != null)
            {
                response = Request.CreateResponse(HttpStatusCode.OK, carers);
            }
            return response;
        }

        // DELETE api/carers
        [Route("api/carers")]
        public HttpResponseMessage Delete(Carer c)
        {
            List<Carer> carers = CarerModel.DeleteCarer(c);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound);
            if (carers != null)
            {
                response = Request.CreateResponse(HttpStatusCode.OK, carers);
            }
            return response;
        }
    }
}
