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
    public class socisController : ApiController
    {
        private CHRYSALLISEntities db = new CHRYSALLISEntities();

        // GET: api/socis
        [HttpGet]
        [Route("api/socis/")]
        [ResponseType(typeof(socis))]
        public IQueryable<socis> Getsocis()
        {
            //cuando vaya a buscar un socio solo encontrará un socio
            db.Configuration.LazyLoadingEnabled = false;
            return db.socis;
        }

        // GET: api/socis/email/contrasenya
        [HttpGet]
        [Route("api/socis/{email}/{contrasenya}/")]
        public async Task<IHttpActionResult> GetsocisLogin(string email, string contrasenya)
        {
            //cuando vaya a buscar un socio solo encontrará un socio
            db.Configuration.LazyLoadingEnabled = false;

            socis _socio = db.socis
                .Where(c => c.email.Equals(email) &&
                c.contrasenya.Equals(contrasenya)).FirstOrDefault();

            return Ok(_socio);
        }

        // GET: api/socis/5
        [ResponseType(typeof(socis))]
        public async Task<IHttpActionResult> Getsocis(int id)
        {
            socis socis = await db.socis.FindAsync(id);
            if (socis == null)
            {
                return NotFound();
            }

            return Ok(socis);
        }

        // PUT: api/socis/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putsocis(int id, socis socis)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != socis.id)
            {
                return BadRequest();
            }

            db.Entry(socis).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!socisExists(id))
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

        // POST: api/socis
        [ResponseType(typeof(socis))]
        public async Task<IHttpActionResult> Postsocis(socis socis)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.socis.Add(socis);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = socis.id }, socis);
        }

        // DELETE: api/socis/5
        [ResponseType(typeof(socis))]
        public async Task<IHttpActionResult> Deletesocis(int id)
        {
            socis socis = await db.socis.FindAsync(id);
            if (socis == null)
            {
                return NotFound();
            }

            db.socis.Remove(socis);
            await db.SaveChangesAsync();

            return Ok(socis);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool socisExists(int id)
        {
            return db.socis.Count(e => e.id == id) > 0;
        }
    }
}