using Programming.DAL;
using ProgrammingAPI.Attributes;
using ProgrammingAPI.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace ProgrammingAPI.Controllers
{
    public class LanguagesController : ApiController
    {

        LanguagesDAL languagesDAL = new LanguagesDAL();
        [ResponseType(typeof(IEnumerable<Languages>))]
        //[ApiExceptionAttribute]-->class level try catch blog
        [Authorize]
        public IHttpActionResult Get()//--->HttpResponseMessage -->IHttpActionResult interface
        {
            var languages = languagesDAL.GetAllLanguages();
            // return Request.CreateResponse(HttpStatusCode.OK, languages);  IHttpActionResult ->Ok interface..
            return Ok(languages);
        }
        [ResponseType(typeof(Languages))]
        //[Authorize]
        [ApiAuthorize(Roles="A")]
        public IHttpActionResult Get(int id)
        {
            var language = languagesDAL.GetLanguageById(id);
            if (language == null)
            {
                // return Request.CreateResponse(HttpStatusCode.NotFound, "ID is not Found!");
                return NotFound();
            }
            // return Request.CreateResponse(HttpStatusCode.OK, language); 
            return Ok(language);
        }
        [ResponseType(typeof(Languages))]
        public IHttpActionResult Post(Languages l)
        {
            if (ModelState.IsValid) { 
            var createdLanguage = languagesDAL.CreateLanguage(l);
                //return Request.CreateResponse(HttpStatusCode.Created, createdLanguage);
                return CreatedAtRoute("DefaultApi", new { id = createdLanguage.ID }, createdLanguage);
            }
            else
            {
                //return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                return BadRequest(ModelState);
            }
        }
        [ResponseType(typeof(Languages))]

        public IHttpActionResult Put(int id,Languages l)
        {
            //id is not found
            if (languagesDAL.IsThereAnyLanguage(id) == false)
            {
                //return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found for ID..");
                return NotFound();
            }
            //language model Is valid false
            else if (ModelState.IsValid == false)
            {
                //return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                return BadRequest(ModelState);
            }
            //200 : OK
            else
            {
                //return Request.CreateResponse(HttpStatusCode.OK, languagesDAL.UpdateLanguage(id,l));
                return Ok(languagesDAL.UpdateLanguage(id, l));
            }
        }
        public IHttpActionResult Delete(int id)
        {
            if (languagesDAL.IsThereAnyLanguage(id) == false)
            {
                //return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found for ID");
                return NotFound();
            }
            else
            {
                languagesDAL.DeleteLanguage(id);
                //return Request.CreateResponse(HttpStatusCode.NoContent);
                return Ok();
            }
           
        }
    }
}
