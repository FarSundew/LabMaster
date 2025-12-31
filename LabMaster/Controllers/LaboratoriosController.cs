using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LabMaster.Models;

namespace LabMaster.Controllers
{
    [Authorize] // Bloquea el acceso a usuarios no autenticados
    public class LaboratoriosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Laboratorios
        // Maestros y Admins pueden ver la lista
        public ActionResult Index()
        {
            var laboratorios = db.Laboratorios.Include(l => l.Carrera);
            return View(laboratorios.ToList());
        }

        // GET: Laboratorios/Details/5
        // Maestros y Admins pueden ver los detalles
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Laboratorio laboratorio = db.Laboratorios.Find(id);
            if (laboratorio == null)
            {
                return HttpNotFound();
            }
            return View(laboratorio);
        }

        // --- ACCIONES RESTRINGIDAS SOLO PARA ADMINISTRADORES ---

        // GET: Laboratorios/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.CarreraID = new SelectList(db.Carreras, "CarreraID", "NombreCarrera");
            return View();
        }

        // POST: Laboratorios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "LabID,NombreLab,Capacidad,CarreraID")] Laboratorio laboratorio)
        {
            if (ModelState.IsValid)
            {
                db.Laboratorios.Add(laboratorio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CarreraID = new SelectList(db.Carreras, "CarreraID", "NombreCarrera", laboratorio.CarreraID);
            return View(laboratorio);
        }

        // GET: Laboratorios/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Laboratorio laboratorio = db.Laboratorios.Find(id);
            if (laboratorio == null)
            {
                return HttpNotFound();
            }
            ViewBag.CarreraID = new SelectList(db.Carreras, "CarreraID", "NombreCarrera", laboratorio.CarreraID);
            return View(laboratorio);
        }

        // POST: Laboratorios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "LabID,NombreLab,Capacidad,CarreraID")] Laboratorio laboratorio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(laboratorio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CarreraID = new SelectList(db.Carreras, "CarreraID", "NombreCarrera", laboratorio.CarreraID);
            return View(laboratorio);
        }

        // GET: Laboratorios/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Laboratorio laboratorio = db.Laboratorios.Find(id);
            if (laboratorio == null)
            {
                return HttpNotFound();
            }
            return View(laboratorio);
        }

        // POST: Laboratorios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Laboratorio laboratorio = db.Laboratorios.Find(id);
            db.Laboratorios.Remove(laboratorio);
            db.SaveChanges();
            return RedirectToAction("Index");
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