using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Hospital.Controllers
{
    public class PatientsController : Controller
    {
        private HospitalContext db = new HospitalContext();

        public ActionResult Index(string searchPatientString)
        {
            if (!String.IsNullOrEmpty(searchPatientString))
            {
                return View(db.Patients.Where(p => p.Name.Contains(searchPatientString)).ToList());
            }
            return View(db.Patients.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Status,DayOfBirth,TaxCode")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                db.Patients.Add(patient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(patient);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            PatientDoctorsVM patientDoctorsVM = new PatientDoctorsVM();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            patientDoctorsVM.Patient = db.Patients.Find(id);
            if (patientDoctorsVM.Patient == null)
            {
                return HttpNotFound();
            }
            patientDoctorsVM.AllDoctors = db.Doctors.ToList();
            var assignedDoctors = db.PatientDoctors.Where(p => p.Patient_Id == id);
            for (int i = 0; i < patientDoctorsVM.AllDoctors.Count; i++)
            {
                foreach (var IsAssigned in assignedDoctors)
                {
                    if (patientDoctorsVM.AllDoctors[i].Id == IsAssigned.Doctor_Id)
                    {
                        patientDoctorsVM.AllDoctors[i].IsSelected = true;
                    }
                }
            }
            return View(patientDoctorsVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PatientDoctorsVM patientDocs)
        {
            if (ModelState.IsValid)
            {
                Patient patient = patientDocs.Patient;
                db.Entry(patient).State = EntityState.Modified;
                db.SaveChanges();

                PatientDoctors doctorAssigned = new PatientDoctors();
                doctorAssigned.Patient_Id = patientDocs.Patient.Id;

                var assignedDoctors = db.PatientDoctors.Where(p => p.Patient_Id == patient.Id).ToList();

                foreach (var doctor in patientDocs.AllDoctors.Where(s => s.IsSelected))
                {
                    doctorAssigned.Doctor_Id = doctor.Id;
                    db.PatientDoctors.Add(doctorAssigned);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Patient patient = db.Patients.Find(id);
            db.Patients.Remove(patient);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Assignations(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}