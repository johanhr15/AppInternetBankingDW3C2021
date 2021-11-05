using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using API.Models;

namespace API.Controllers
{
    [Authorize]
    public class Traslado_PensionesController : ApiController
    {
        private INTERNET_BANKING_DW1_3C2021Entities db = new INTERNET_BANKING_DW1_3C2021Entities();

        // GET: api/Traslado_Pensiones
        public IQueryable<Traslado_Pensiones> GetTraslado_Pensiones()
        {
            return db.Traslado_Pensiones;
        }

        // GET: api/Traslado_Pensiones/5
        [ResponseType(typeof(Traslado_Pensiones))]
        public IHttpActionResult GetTraslado_Pensiones(int id)
        {
            Traslado_Pensiones traslado_Pensiones = db.Traslado_Pensiones.Find(id);
            if (traslado_Pensiones == null)
            {
                return NotFound();
            }

            return Ok(traslado_Pensiones);
        }

        // PUT: api/Traslado_Pensiones/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTraslado_Pensiones(int id, Traslado_Pensiones traslado_Pensiones)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != traslado_Pensiones.TRAS_CODIGO)
            {
                return BadRequest();
            }

            db.Entry(traslado_Pensiones).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Traslado_PensionesExists(id))
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

        // POST: api/Traslado_Pensiones
        [ResponseType(typeof(Traslado_Pensiones))]
        public IHttpActionResult PostTraslado_Pensiones(Traslado_Pensiones traslado_Pensiones)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Traslado_Pensiones.Add(traslado_Pensiones);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = traslado_Pensiones.TRAS_CODIGO }, traslado_Pensiones);
        }

        // DELETE: api/Traslado_Pensiones/5
        [ResponseType(typeof(Traslado_Pensiones))]
        public IHttpActionResult DeleteTraslado_Pensiones(int id)
        {
            Traslado_Pensiones traslado_Pensiones = db.Traslado_Pensiones.Find(id);
            if (traslado_Pensiones == null)
            {
                return NotFound();
            }

            db.Traslado_Pensiones.Remove(traslado_Pensiones);
            db.SaveChanges();

            return Ok(traslado_Pensiones);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Traslado_PensionesExists(int id)
        {
            return db.Traslado_Pensiones.Count(e => e.TRAS_CODIGO == id) > 0;
        }
    }
}