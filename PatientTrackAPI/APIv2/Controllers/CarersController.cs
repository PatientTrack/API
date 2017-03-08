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
using APIv2.Models;

namespace APIv2.Controllers
{
    public class CarersController : ApiController
    {
        private PatientTrackDBEntities db = new PatientTrackDBEntities();

        // GET: api/Carers
        public IQueryable<Carer> GetCarers()
        {
            return db.Carers;
        }

        // GET: api/Carers/5
        [ResponseType(typeof(Carer))]
        public IHttpActionResult GetCarer(int id)
        {
            Carer carer = db.Carers.Find(id);
            if (carer == null)
            {
                return NotFound();
            }

            return Ok(carer);
        }

        // PUT: api/Carers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCarer(int id, Carer carer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != carer.CarerID)
            {
                return BadRequest();
            }

            db.Entry(carer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarerExists(id))
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

        // POST: api/Carers
        [ResponseType(typeof(Carer))]
        public IHttpActionResult PostCarer(Carer carer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Carers.Add(carer);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = carer.CarerID }, carer);
        }

        // DELETE: api/Carers/5
        [ResponseType(typeof(Carer))]
        public IHttpActionResult DeleteCarer(int id)
        {
            Carer carer = db.Carers.Find(id);
            if (carer == null)
            {
                return NotFound();
            }

            db.Carers.Remove(carer);
            db.SaveChanges();

            return Ok(carer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CarerExists(int id)
        {
            return db.Carers.Count(e => e.CarerID == id) > 0;
        }
    }
}