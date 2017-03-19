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
    public class PatientsController : ApiController
    {
        private PatientTrackDBEntities db = new PatientTrackDBEntities();

        [Route("api/Patients/{loginEmail}/{loginPwd}")]
        public IHttpActionResult Get(string loginEmail, string loginPwd)
        {
            Patient p = null;
            try
            {
                p = (from pat in db.Patients
                     where pat.PatientEmail == loginEmail
                     && pat.PatientPwd == loginPwd
                     select pat).First();
            }
            catch
            {
                return NotFound();
            }

            if (p == null)
            {
                return NotFound();
            }

            return Ok(p);
        }

        [Route("api/Patients/code/{patientCode}")]
        public IHttpActionResult GetByCode(string patientCode)
        {
            Patient p = null;
            try
            {
                p = (from pat in db.Patients
                     where pat.PatientCode == patientCode
                     select pat).First();
            }
            catch
            {
                return NotFound();
            }

            if (p == null)
            {
                return NotFound();
            }

            return Ok(p);
        }

        // GET: api/Patients
        public IQueryable<Patient> GetPatients()
        {
            return db.Patients;
        }

        // GET: api/Patients/5
        [ResponseType(typeof(Patient))]
        public IHttpActionResult GetPatient(int id)
        {
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return NotFound();
            }

            return Ok(patient);
        }

        // PUT: api/Patients/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPatient(int id, Patient patient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != patient.PatientID)
            {
                return BadRequest();
            }

            db.Entry(patient).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest();
                }
            }

            return GetPatient(id);
        }

        // POST: api/Patients/17/AddLocation/51.477156/0.001112
        [Route("api/Patients/AddLocation/{patientID}")]
        public IHttpActionResult PostLocation(int patientID, [FromBody]Location location)
        {
            try
            {
                db.Database.ExecuteSqlCommand("INSERT INTO Location (PatientID, Longitude, Latitude) VALUES (" + patientID + "," + location.Longitude + "," + location.Latitude + ");");
            }
            catch
            {
                return BadRequest();
            }
            return Ok();
        }

        // POST: api/Patients
        [ResponseType(typeof(Patient))]
        public IHttpActionResult PostPatient(Patient patient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Patients.Add(patient);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = patient.PatientID }, patient);
        }

        // DELETE: api/Patients/5
        [ResponseType(typeof(Patient))]
        public IHttpActionResult DeletePatient(int id)
        {
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return NotFound();
            }

            List<Location> patientLocations = (from l in db.Locations
                                               where l.PatientID == patient.PatientID
                                               select l).ToList();
            foreach (Location loc in patientLocations)
            {
                db.Locations.Remove(loc);
            }
            db.Patients.Remove(patient);
            db.SaveChanges();

            return Ok(patient);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PatientExists(int id)
        {
            return db.Patients.Count(e => e.PatientID == id) > 0;
        }
    }
}