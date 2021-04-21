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
    public class valoracionsController : ApiController
    {
        private CHRYSALLISEntities db = new CHRYSALLISEntities();

        // GET: api/valoracions
        public IQueryable<valoracions> Getvaloracions()
        {
            return db.valoracions;
        }

        // GET: api/valoracions/5
        [ResponseType(typeof(valoracions))]
        public async Task<IHttpActionResult> Getvaloracions(int id)
        {
            valoracions valoracions = await db.valoracions.FindAsync(id);
            if (valoracions == null)
            {
                return NotFound();
            }

            return Ok(valoracions);
        }

        // PUT: api/valoracions/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putvaloracions(int id, valoracions valoracions)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != valoracions.id_soci)
            {
                return BadRequest();
            }

            db.Entry(valoracions).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!valoracionsExists(id))
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

        // POST: api/valoracions
        [ResponseType(typeof(valoracions))]
        public async Task<IHttpActionResult> Postvaloracions(valoracions valoracions)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.valoracions.Add(valoracions);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (valoracionsExists(valoracions.id_soci))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = valoracions.id_soci }, valoracions);
        }

        // DELETE: api/valoracions/5
        [ResponseType(typeof(valoracions))]
        public async Task<IHttpActionResult> Deletevaloracions(int id)
        {
            valoracions valoracions = await db.valoracions.FindAsync(id);
            if (valoracions == null)
            {
                return NotFound();
            }

            db.valoracions.Remove(valoracions);
            await db.SaveChangesAsync();

            return Ok(valoracions);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool valoracionsExists(int id)
        {
            return db.valoracions.Count(e => e.id_soci == id) > 0;
        }
    }
}