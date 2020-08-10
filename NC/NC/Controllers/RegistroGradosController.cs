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
    public class RegistroGradosController : Controller
    {
        private NCEntities db = new NCEntities();

        // GET: RegistroGrados
        public ActionResult Index()
        {
            return View(db.Tbl_Grados.ToList());
        }

        // GET: RegistroGrados/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Grados tbl_Grados = db.Tbl_Grados.Find(id);
            if (tbl_Grados == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Grados);
        }

        // GET: RegistroGrados/Create
        [AuthorizeUser(IdOperacion: 6)]
        public ActionResult Create()
        {
            return View();
        }

        // POST: RegistroGrados/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdGrado,NombreGrado,JornadaGrado,DisponibilidadCupoGrado")] Tbl_Grados tbl_Grados)
        {
            if (ModelState.IsValid)
            {
                db.Tbl_Grados.Add(tbl_Grados);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_Grados);
        }

        // GET: RegistroGrados/Edit/5
        [AuthorizeUser(IdOperacion: 7)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Grados tbl_Grados = db.Tbl_Grados.Find(id);
            if (tbl_Grados == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Grados);
        }

        // POST: RegistroGrados/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdGrado,NombreGrado,JornadaGrado,DisponibilidadCupoGrado")] Tbl_Grados tbl_Grados)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Grados).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_Grados);
        }

        // GET: RegistroGrados/Delete/5
        [AuthorizeUser(IdOperacion: 9)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Grados tbl_Grados = db.Tbl_Grados.Find(id);
            if (tbl_Grados == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Grados);
        }

        // POST: RegistroGrados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tbl_Grados tbl_Grados = db.Tbl_Grados.Find(id);
            db.Tbl_Grados.Remove(tbl_Grados);
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
