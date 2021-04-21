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
    public class comunitatsController : ApiController
    {
        private CHRYSALLISEntities db = new CHRYSALLISEntities();

        // GET: api/comunitats
        public IQueryable<comunitats> Getcomunitats()
        {
            return db.comunitats;
        }

        // GET: api/comunitats/5
        [ResponseType(typeof(comunitats))]
        public async Task<IHttpActionResult> Getcomunitats(int id)
        {
            comunitats comunitats = await db.comunitats.FindAsync(id);
            if (comunitats == null)
            {
                return NotFound();
            }

            return Ok(comunitats);
        }

        // PUT: api/comunitats/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putcomunitats(int id, comunitats comunitats)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != comunitats.id)
            {
                return BadRequest();
            }

            db.Entry(comunitats).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!comunitatsExists(id))
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

        // POST: api/comunitats
        [ResponseType(typeof(comunitats))]
        public async Task<IHttpActionResult> Postcomunitats(comunitats comunitats)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.comunitats.Add(comunitats);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = comunitats.id }, comunitats);
        }

        // DELETE: api/comunitats/5
        [ResponseType(typeof(comunitats))]
        public async Task<IHttpActionResult> Deletecomunitats(int id)
        {
            comunitats comunitats = await db.comunitats.FindAsync(id);
            if (comunitats == null)
            {
                return NotFound();
            }

            db.comunitats.Remove(comunitats);
            await db.SaveChangesAsync();

            return Ok(comunitats);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool comunitatsExists(int id)
        {
            return db.comunitats.Count(e => e.id == id) > 0;
        }
    }
}