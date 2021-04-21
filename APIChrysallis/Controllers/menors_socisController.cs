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
    public class menors_socisController : ApiController
    {
        private CHRYSALLISEntities db = new CHRYSALLISEntities();

        // GET: api/menors_socis
        public IQueryable<menors_socis> Getmenors_socis()
        {
            return db.menors_socis;
        }

        // GET: api/menors_socis/5
        [ResponseType(typeof(menors_socis))]
        public async Task<IHttpActionResult> Getmenors_socis(int id)
        {
            menors_socis menors_socis = await db.menors_socis.FindAsync(id);
            if (menors_socis == null)
            {
                return NotFound();
            }

            return Ok(menors_socis);
        }

        // PUT: api/menors_socis/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putmenors_socis(int id, menors_socis menors_socis)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != menors_socis.id_soci)
            {
                return BadRequest();
            }

            db.Entry(menors_socis).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!menors_socisExists(id))
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

        // POST: api/menors_socis
        [ResponseType(typeof(menors_socis))]
        public async Task<IHttpActionResult> Postmenors_socis(menors_socis menors_socis)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.menors_socis.Add(menors_socis);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (menors_socisExists(menors_socis.id_soci))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = menors_socis.id_soci }, menors_socis);
        }

        // DELETE: api/menors_socis/5
        [ResponseType(typeof(menors_socis))]
        public async Task<IHttpActionResult> Deletemenors_socis(int id)
        {
            menors_socis menors_socis = await db.menors_socis.FindAsync(id);
            if (menors_socis == null)
            {
                return NotFound();
            }

            db.menors_socis.Remove(menors_socis);
            await db.SaveChangesAsync();

            return Ok(menors_socis);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool menors_socisExists(int id)
        {
            return db.menors_socis.Count(e => e.id_soci == id) > 0;
        }
    }
}