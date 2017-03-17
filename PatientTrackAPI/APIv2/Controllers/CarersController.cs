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

        [Route("api/Carers/{loginEmail}/{loginPwd}")]
        public IHttpActionResult Get(string loginEmail, string loginPwd)
        {
            Carer c = null;
            try
            {
                c = (from car in db.Carers
                           where car.CarerEmail == loginEmail
                           && car.CarerPwd == loginPwd
                           select car).First();
            }
            catch
            {
                return NotFound();
            }

            if (c == null)
            {
                return NotFound();
            }

            return Ok(c);
        }

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

        // PUT: api/Carers/1/AddPatient/abc123
        [Route("api/Carers/{carerID}/AddPatient/{patientCode}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCarerAddPatient(int carerID, string patientCode)
        {
            Patient patient = null;
            //fetch the patient from database (by patientCode)
            try
            {
                patient = db.Patients.FirstOrDefault(x => x.PatientCode == patientCode);
            }
            catch
            {
                return NotFound();
            }
            try
            {
                db.Database.ExecuteSqlCommand("INSERT INTO CarerPatient (CarerID, PatientID) VALUES (" + carerID + "," + patient.PatientID + ");");
            }
            catch
            {
                return BadRequest();
            }
            return Ok();
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
                    return BadRequest();
                }
            }

            Carer car = db.Carers.Find(id);
            if (car == null)
            {
                return NotFound();
            }

            return Ok(carer);
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

        // POST: api/Login/Carers
        [ResponseType(typeof(Carer))]
        public IHttpActionResult LoginCarer(string email, string pwd)
        {
            Carer c = (from car in db.Carers
                       where car.CarerEmail == email
                       && car.CarerPwd == pwd
                       select car).First();

            if (c == null)
            {
                return NotFound();
            }

            return Ok(c);
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
            db.Database.ExecuteSqlCommand("DELETE FROM CarerPatient WHERE CarerID = " + carer.CarerID + ";");
            db.Carers.Remove(carer);
            db.SaveChanges();

            return Ok(carer);
        }

        // DELETE: api/Carers/1/AddPatient/abc123
        [Route("api/Carers/{carerID}/DeletePatient/{patientID}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult DeleteCarerPatient(int carerID, int patientID)
        {
            try
            {
                db.Database.ExecuteSqlCommand("DELETE FROM CarerPatient WHERE CarerID = " + carerID + " AND PatientID = " + patientID + ";");
            }
            catch
            {
                return BadRequest();
            }
            return Ok();
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