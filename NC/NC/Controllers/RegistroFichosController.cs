using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NC.Filters;
using NC.Models;

namespace NC.Controllers
{
    public class RegistroFichosController : Controller
    {
        private NCEntities db = new NCEntities();

        // GET: RegistroFichos
        public ActionResult Index()
        {
            var tbl_Fichos = db.Tbl_Fichos.Include(t => t.Tbl_Usuarios);
            return View(tbl_Fichos.ToList());
        }

        // GET: RegistroFichos/Create
        [AuthorizeUser(IdOperacion: 11)]
        public ActionResult Create()
        {
            ViewBag.IdUsuario = new SelectList(db.Tbl_Usuarios, "IdUsuario", "NombreUsuario");
            return View();
        }

        // POST: RegistroFichos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdFicho,FechaFicho,IdUsuario")] Tbl_Fichos tbl_Fichos)
        {
            if (ModelState.IsValid)
            {
                db.Tbl_Fichos.Add(tbl_Fichos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdUsuario = new SelectList(db.Tbl_Usuarios, "IdUsuario", "NombreUsuario", tbl_Fichos.IdUsuario);
            return View(tbl_Fichos);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdFicho,FechaFicho,IdUsuario")] Tbl_Fichos tbl_Fichos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Fichos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdUsuario = new SelectList(db.Tbl_Usuarios, "IdUsuario", "NombreUsuario", tbl_Fichos.IdUsuario);
            return View(tbl_Fichos);
        }

        // GET: RegistroFichos/Delete/5
        [AuthorizeUser(IdOperacion: 12)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Fichos tbl_Fichos = db.Tbl_Fichos.Find(id);
            if (tbl_Fichos == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Fichos);
        }

        // POST: RegistroFichos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tbl_Fichos tbl_Fichos = db.Tbl_Fichos.Find(id);
            db.Tbl_Fichos.Remove(tbl_Fichos);
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
