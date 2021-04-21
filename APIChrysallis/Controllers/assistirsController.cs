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
    public class assistirsController : ApiController
    {
        private CHRYSALLISEntities db = new CHRYSALLISEntities();

        // GET: api/assistirs
        public IQueryable<assistir> Getassistir()
        {
            return db.assistir;
        }

        // GET: api/assistirs/5
        [ResponseType(typeof(assistir))]
        public async Task<IHttpActionResult> Getassistir(int id)
        {
            assistir assistir = await db.assistir.FindAsync(id);
            if (assistir == null)
            {
                return NotFound();
            }

            return Ok(assistir);
        }

        // PUT: api/assistirs/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putassistir(int id, assistir assistir)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != assistir.id_soci)
            {
                return BadRequest();
            }

            db.Entry(assistir).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!assistirExists(id))
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

        // POST: api/assistirs
        [ResponseType(typeof(assistir))]
        public async Task<IHttpActionResult> Postassistir(assistir assistir)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.assistir.Add(assistir);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (assistirExists(assistir.id_soci))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = assistir.id_soci }, assistir);
        }

        // DELETE: api/assistirs/5
        [ResponseType(typeof(assistir))]
        public async Task<IHttpActionResult> Deleteassistir(int id)
        {
            assistir assistir = await db.assistir.FindAsync(id);
            if (assistir == null)
            {
                return NotFound();
            }

            db.assistir.Remove(assistir);
            await db.SaveChangesAsync();

            return Ok(assistir);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool assistirExists(int id)
        {
            return db.assistir.Count(e => e.id_soci == id) > 0;
        }
    }
}