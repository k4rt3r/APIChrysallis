using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using APIChrysallis.Models;

namespace APIChrysallis.Controllers
{
    public class esdevenimentsController : ApiController
    {
        private CHRYSALLISEntities db = new CHRYSALLISEntities();

        // GET: api/esdeveniments
        public IQueryable<esdeveniments> Getesdeveniments()
        {
            //cuando vaya a buscar un evento solo encontrará un evento
            db.Configuration.LazyLoadingEnabled = false;
            return db.esdeveniments;
        }

        // GET: api/esdeveniments/5
        [ResponseType(typeof(esdeveniments))]
        public async Task<IHttpActionResult> Getesdeveniments(int id)
        {
            IHttpActionResult result;
            esdeveniments esdeveniments = await db.esdeveniments.FindAsync(id);
            /*
            esdeveniments evento = db.esdeveniments
                .Include("localitat")
                .Where(c => c.id == id)
                .FirstOrDefaultAsync();
            */

            if (esdeveniments == null)
            {
                result= NotFound();
            }
            else
            {
                result = Ok(esdeveniments);
            }

            return result;
        }

        [HttpGet]
        [Route("api/esdeveniments/titol/{titol}")]
        public async Task<IHttpActionResult> FindByNombre (String nombre)
        {
            IHttpActionResult result;
            db.Configuration.LazyLoadingEnabled = false;

            List<esdeveniments> _eventos = db.esdeveniments
                .Where(c => c.titol.Contains(nombre))
                .ToList();

            return Ok(_eventos);
        }

        // PUT: api/esdeveniments/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putesdeveniments(int id, esdeveniments _evento)
        {
            IHttpActionResult result;
            String mensaje="";

            if (!ModelState.IsValid)
            {
                result= BadRequest(ModelState);
            }
            else
            {
                if (id != _evento.id)
                {
                    result= BadRequest();
                }
                else
                {
                db.Entry(_evento).State = EntityState.Modified;
                   

                    try
                {
                    await db.SaveChangesAsync();
                        result = StatusCode(HttpStatusCode.NoContent);
                    }
                catch (DbUpdateConcurrencyException)
                {
                    if (!esdevenimentsExists(id))
                    {
                        result= NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                    catch (DbUpdateException ex)
                    {
                        SqlException sqlException = (SqlException)ex.InnerException.InnerException;
                        mensaje = Clases.Utilidad.MissatgeError(sqlException);
                        result = BadRequest(mensaje);
                    }
                }

             

            }


            return result;
        }

        // POST: api/esdeveniments
        [ResponseType(typeof(esdeveniments))]
        public async Task<IHttpActionResult> Postesdeveniments(esdeveniments _evento)
        {
            IHttpActionResult result;

            //control errors per a rebre request correcta
            if (!ModelState.IsValid)
            {
                result= BadRequest(ModelState);
            }
            else
            {
            db.esdeveniments.Add(_evento);
                String mensaje = "";
                try
                {
                    await db.SaveChangesAsync();
                    result = CreatedAtRoute("DefaultApi", new { id = _evento.id }, _evento);


                }
                catch (DbUpdateException ex)
                {
                    SqlException sqlException = (SqlException)ex.InnerException.InnerException;
                    mensaje = Clases.Utilidad.MissatgeError(sqlException);
                    result = BadRequest(mensaje);
                }

            }

            return result;
        }

        // DELETE: api/esdeveniments/5
        [ResponseType(typeof(esdeveniments))]
        public async Task<IHttpActionResult> Deleteesdeveniments(int id)
        {
            IHttpActionResult result;

            esdeveniments _evento = await db.esdeveniments.FindAsync(id);
            if (_evento == null)
            {
                result=NotFound();
            }
            else
            {
                String mensaje = "";

                try
                {
                    db.esdeveniments.Remove(_evento);
                    await db.SaveChangesAsync();
                    result = Ok(_evento);

                }
                catch (DbUpdateException ex)
                { 
                    SqlException sqlException = (SqlException)ex.InnerException.InnerException;
                    mensaje = Clases.Utilidad.MissatgeError(sqlException);
                    result = BadRequest(mensaje);
                }
              
            }


            return result;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool esdevenimentsExists(int id)
        {
            return db.esdeveniments.Count(e => e.id == id) > 0;
        }
    }
}