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
    public class menorsController : ApiController
    {
        private CHRYSALLISEntities db = new CHRYSALLISEntities();

        // GET: api/menors
        public IQueryable<menors> Getmenors()
        {
            return db.menors;
        }

        // GET: api/menors/5
        [ResponseType(typeof(menors))]
        public async Task<IHttpActionResult> Getmenors(int id)
        {
            menors menors = await db.menors.FindAsync(id);
            if (menors == null)
            {
                return NotFound();
            }

            return Ok(menors);
        }

        // PUT: api/menors/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putmenors(int id, menors menors)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != menors.id)
            {
                return BadRequest();
            }

            db.Entry(menors).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!menorsExists(id))
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

        // POST: api/menors
        [ResponseType(typeof(menors))]
        public async Task<IHttpActionResult> Postmenors(menors menors)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.menors.Add(menors);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = menors.id }, menors);
        }

        // DELETE: api/menors/5
        [ResponseType(typeof(menors))]
        public async Task<IHttpActionResult> Deletemenors(int id)
        {
            menors menors = await db.menors.FindAsync(id);
            if (menors == null)
            {
                return NotFound();
            }

            db.menors.Remove(menors);
            await db.SaveChangesAsync();

            return Ok(menors);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool menorsExists(int id)
        {
            return db.menors.Count(e => e.id == id) > 0;
        }
    }
}