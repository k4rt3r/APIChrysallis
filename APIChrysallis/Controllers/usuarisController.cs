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
    public class usuarisController : ApiController
    {
        private CHRYSALLISEntities db = new CHRYSALLISEntities();

        // GET: api/usuaris
        public IQueryable<usuaris> Getusuaris()
        {
            return db.usuaris;
        }

        // GET: api/usuaris/5
        [ResponseType(typeof(usuaris))]
        public async Task<IHttpActionResult> Getusuaris(int id)
        {
            usuaris usuaris = await db.usuaris.FindAsync(id);
            if (usuaris == null)
            {
                return NotFound();
            }

            return Ok(usuaris);
        }

        // PUT: api/usuaris/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putusuaris(int id, usuaris usuaris)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != usuaris.id)
            {
                return BadRequest();
            }

            db.Entry(usuaris).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!usuarisExists(id))
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

        // POST: api/usuaris
        [ResponseType(typeof(usuaris))]
        public async Task<IHttpActionResult> Postusuaris(usuaris usuaris)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.usuaris.Add(usuaris);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = usuaris.id }, usuaris);
        }

        // DELETE: api/usuaris/5
        [ResponseType(typeof(usuaris))]
        public async Task<IHttpActionResult> Deleteusuaris(int id)
        {
            usuaris usuaris = await db.usuaris.FindAsync(id);
            if (usuaris == null)
            {
                return NotFound();
            }

            db.usuaris.Remove(usuaris);
            await db.SaveChangesAsync();

            return Ok(usuaris);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool usuarisExists(int id)
        {
            return db.usuaris.Count(e => e.id == id) > 0;
        }
    }
}