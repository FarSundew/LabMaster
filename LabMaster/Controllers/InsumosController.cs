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
    // Solo usuarios con el rol "Admin" pueden acceder a estas funciones
    [Authorize(Roles = "Admin")]
    public class InsumosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Insumos
        // Muestra la lista de todos los materiales y su stock actual
        public ActionResult Index()
        {
            return View(db.Insumos.ToList());
        }

        // GET: Insumos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insumo insumo = db.Insumos.Find(id);
            if (insumo == null)
            {
                return HttpNotFound();
            }
            return View(insumo);
        }

        // GET: Insumos/Create
        // Carga la pantalla para registrar un nuevo insumo
        public ActionResult Create()
        {
            // Enviamos la lista de carreras a la vista
            ViewBag.CarreraID = new SelectList(db.Carreras, "CarreraID", "NombreCarrera");
            return View();
        }

        // POST: Insumos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InsumoID,NombreInsumo,Stock,CarreraID")] Insumo insumo)
        {
            if (ModelState.IsValid)
            {
                db.Insumos.Add(insumo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CarreraID = new SelectList(db.Carreras, "CarreraID", "NombreCarrera", insumo.CarreraID);
            return View(insumo);
        }

        // GET: Insumos/Edit/5
        // Carga la pantalla para modificar el nombre o la cantidad de un insumo existente
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insumo insumo = db.Insumos.Find(id);
            if (insumo == null)
            {
                return HttpNotFound();
            }
            return View(insumo);
        }

        // POST: Insumos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InsumoID,NombreInsumo,Stock")] Insumo insumo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(insumo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(insumo);
        }

        // GET: Insumos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insumo insumo = db.Insumos.Find(id);
            if (insumo == null)
            {
                return HttpNotFound();
            }
            return View(insumo);
        }

        // POST: Insumos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Insumo insumo = db.Insumos.Find(id);
            db.Insumos.Remove(insumo);
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