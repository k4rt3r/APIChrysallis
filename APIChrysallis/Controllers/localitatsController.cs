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
    public class localitatsController : ApiController
    {
        private CHRYSALLISEntities db = new CHRYSALLISEntities();

        // GET: api/localitats
        public IQueryable<localitats> Getlocalitats()
        {
            return db.localitats;
        }

        // GET: api/localitats/5
        [ResponseType(typeof(localitats))]
        public async Task<IHttpActionResult> Getlocalitats(int id)
        {
            localitats localitats = await db.localitats.FindAsync(id);
            if (localitats == null)
            {
                return NotFound();
            }

            return Ok(localitats);
        }

        // PUT: api/localitats/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putlocalitats(int id, localitats localitats)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != localitats.id)
            {
                return BadRequest();
            }

            db.Entry(localitats).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!localitatsExists(id))
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

        // POST: api/localitats
        [ResponseType(typeof(localitats))]
        public async Task<IHttpActionResult> Postlocalitats(localitats localitats)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.localitats.Add(localitats);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = localitats.id }, localitats);
        }

        // DELETE: api/localitats/5
        [ResponseType(typeof(localitats))]
        public async Task<IHttpActionResult> Deletelocalitats(int id)
        {
            localitats localitats = await db.localitats.FindAsync(id);
            if (localitats == null)
            {
                return NotFound();
            }

            db.localitats.Remove(localitats);
            await db.SaveChangesAsync();

            return Ok(localitats);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool localitatsExists(int id)
        {
            return db.localitats.Count(e => e.id == id) > 0;
        }
    }
}