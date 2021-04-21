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
    public class rolsController : ApiController
    {
        private CHRYSALLISEntities db = new CHRYSALLISEntities();

        // GET: api/rols
        public IQueryable<rols> Getrols()
        {
            return db.rols;
        }

        // GET: api/rols/5
        [ResponseType(typeof(rols))]
        public async Task<IHttpActionResult> Getrols(int id)
        {
            rols rols = await db.rols.FindAsync(id);
            if (rols == null)
            {
                return NotFound();
            }

            return Ok(rols);
        }

        // PUT: api/rols/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putrols(int id, rols rols)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rols.id)
            {
                return BadRequest();
            }

            db.Entry(rols).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!rolsExists(id))
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

        // POST: api/rols
        [ResponseType(typeof(rols))]
        public async Task<IHttpActionResult> Postrols(rols rols)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.rols.Add(rols);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = rols.id }, rols);
        }

        // DELETE: api/rols/5
        [ResponseType(typeof(rols))]
        public async Task<IHttpActionResult> Deleterols(int id)
        {
            rols rols = await db.rols.FindAsync(id);
            if (rols == null)
            {
                return NotFound();
            }

            db.rols.Remove(rols);
            await db.SaveChangesAsync();

            return Ok(rols);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool rolsExists(int id)
        {
            return db.rols.Count(e => e.id == id) > 0;
        }
    }
}