using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi2_1ExceptionHandling.Attributes;

namespace WebApi2_1ExceptionHandling.Controllers
{
    [RoutePrefix("exception")]
    public class ValuesController : ApiController
    {
        // GET api/values
        [HttpGet]
        [Route("ok")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        [Route("Moved")]
        public IEnumerable<string> GetBasicException()
        {
            throw new HttpResponseException(HttpStatusCode.Moved);
            return new string[] { "value1", "value2" };
        }



        [Route("bad/{id}")]
        [HttpGet]
        public int Get(int id)
        {
            if (id > 3)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(string.Format("id > 3, your value: {0}", id)),
                    ReasonPhrase = "Your id is too big"
                };
                throw new HttpResponseException(resp);
            }
            return id;
        }

        [Route("filter/{id}")]
        [HttpGet]
        [ValidationExceptionFilter]
        public int GetWithFilterValidation(int id)
        {
            if (id > 3)
            {
                throw new ValidationException(string.Format("Your id is too big, your value: {0}", id));
            }
            
            return id;
        }

        [Route("unhandledValidation/{id}")]
        [HttpGet]
        public int GetWithGlobalValidation(int id)
        {
            if (id > 3)
            {
                throw new ValidationException(string.Format("Your id is too big, your value: {0}", id));
            }

            return id;
        }


        [Route("HttpError/{id}")]
        [HttpGet]
        public HttpResponseMessage GetProduct(int id)
        {
            if (id > 3)
            {
                var message = string.Format("id > 3 {0} ", id);
                HttpError err = new HttpError(message);
                err["error_id_Validation"] = 300;
                return Request.CreateResponse(HttpStatusCode.BadRequest, err);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, id);
            }
        }

        [Route("unhandled")]
        [HttpGet]
        public int UnhandledException()
        {
            throw new Exception("Unhandled Exception from value controller");
        }
    }
}
