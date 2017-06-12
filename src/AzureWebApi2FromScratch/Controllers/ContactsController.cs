using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;
using AzureWebApi2FromScratch.Models;

namespace AzureWebApi2FromScratch.Controllers
{
    public class ContactsController : ApiController
    {
        // GET api/contacts        
        [SwaggerOperation("GetAll")]
        public IEnumerable<Contact> Get() {
            ApplicationDbContext dbContext = new ApplicationDbContext();
            return dbContext.Contacts.ToArray();
        }

        // GET api/contacts/1
        [SwaggerOperation("GetById")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public IHttpActionResult Get(int id) {
            ApplicationDbContext dbContext = new ApplicationDbContext();
            var result = dbContext.Contacts.SingleOrDefault(c => c.ContactId == id);
            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }

        // POST api/contacts
        [SwaggerOperation("Create")]
        [SwaggerResponse(HttpStatusCode.Created)]
        public IHttpActionResult Post([FromBody]Contact contact) {
            try {
                ApplicationDbContext dbContext = new ApplicationDbContext();
                dbContext.Contacts.Add(contact);
                dbContext.SaveChanges();
                return Created(new Uri(Request.RequestUri + contact.ContactId.ToString()), contact);
            }
            catch {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NoContent);
                return ResponseMessage(response);
            }            
        }

        // PUT api/contacts/1
        [SwaggerOperation("Update")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public IHttpActionResult Put(int id, [FromBody]Contact updatedContact) {
            ApplicationDbContext dbContext = new ApplicationDbContext();
            var contact = dbContext.Contacts.SingleOrDefault(c => c.ContactId == id);
            if (contact != null) {
                if (!string.IsNullOrEmpty(updatedContact.DisplayName))
                    contact.DisplayName = updatedContact.DisplayName;
                if (!string.IsNullOrEmpty(updatedContact.FirstName))
                    contact.FirstName = updatedContact.FirstName;
                if (!string.IsNullOrEmpty(updatedContact.LastName))
                    contact.LastName = updatedContact.LastName;
                if (!string.IsNullOrEmpty(updatedContact.Email))
                    contact.Email = updatedContact.Email;
                if (!string.IsNullOrEmpty(updatedContact.Phone))
                    contact.Phone = updatedContact.Phone;
                dbContext.SaveChanges();
                return Ok(); // Ok(result);
            }
            else
                return NotFound();        

            //dbContext.Contacts.Attach(updatedContact);
            //dbContext.Entry(updatedContact).State = System.Data.Entity.EntityState.Modified;
            //dbContext.SaveChanges();
            //return Ok();
        }

        // DELETE api/contacts/1
        [SwaggerOperation("Delete")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public IHttpActionResult Delete(int id) {
            ApplicationDbContext dbContext = new ApplicationDbContext();
            var result = dbContext.Contacts.SingleOrDefault(c => c.ContactId == id);
            if (result != null) {
                dbContext.Contacts.Remove(result);
                dbContext.SaveChanges();
                return Ok(); // Ok(result);
            }
            else
                return NotFound();
        }
    }
}
