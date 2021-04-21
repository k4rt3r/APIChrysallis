using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using APIChrysallis.Models;

namespace APIChrysallis.Controllers
{
    public class documentsController : ApiController
    {
        private CHRYSALLISEntities db = new CHRYSALLISEntities();

        // GET: api/documents
        public IQueryable<documents> Getdocuments()
        {
            return db.documents;
        }

        // GET: api/documents/5
        [ResponseType(typeof(documents))]
        public async Task<IHttpActionResult> Getdocuments(int id)
        {
            documents documents = await db.documents.FindAsync(id);
            if (documents == null)
            {
                return NotFound();
            }

            return Ok(documents);
        }

        // PUT: api/documents/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putdocuments(int id, documents documents)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != documents.id)
            {
                return BadRequest();
            }

            db.Entry(documents).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!documentsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/documents
        [ResponseType(typeof(documents))]
        public async Task<IHttpActionResult> Postdocuments(documents documents)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.documents.Add(documents);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = documents.id }, documents);
        }

        // DELETE: api/documents/5
        [ResponseType(typeof(documents))]
        public async Task<IHttpActionResult> Deletedocuments(int id)
        {
            documents documents = await db.documents.FindAsync(id);
            if (documents == null)
            {
                return NotFound();
            }

            db.documents.Remove(documents);
            await db.SaveChangesAsync();

            return Ok(documents);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool documentsExists(int id)
        {
            return db.documents.Count(e => e.id == id) > 0;
        }
    }
}