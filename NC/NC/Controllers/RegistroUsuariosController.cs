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
using NC.Pass;

namespace NC.Controllers
{
    public class RegistroUsuariosController : Controller
    {
        private NCEntities db = new NCEntities();

        // GET: RegistroUsuarios

        [AuthorizeUser(IdOperacion:5)]
        public ActionResult Index()
        {
            var tbl_Usuarios = db.Tbl_Usuarios.Include(t => t.Tbl_Roles);
            return View(tbl_Usuarios.ToList());
        }

        [AuthorizeUser(IdOperacion: 3)]
        // GET: RegistroUsuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Usuarios tbl_Usuarios = db.Tbl_Usuarios.Find(id);
            if (tbl_Usuarios == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Usuarios);
        }

        [AuthorizeUser(IdOperacion: 1)]
        // GET: RegistroUsuarios/Create
        public ActionResult Create()
        {
            ViewBag.IdRol = new SelectList(db.Tbl_Roles, "IdRol", "NombreRol");
            return View();
        }

        // POST: RegistroUsuarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdUsuario,NombreUsuario,ApellidoUsuario,CorreoUsuario,ContraseñaUsuario,IdRol")] Tbl_Usuarios tbl_Usuarios)
        {
            if (ModelState.IsValid)
            {
                db.Tbl_Usuarios.Add(tbl_Usuarios);
                tbl_Usuarios.ContraseñaUsuario = Hashing.HashPassword(tbl_Usuarios.ContraseñaUsuario);
                db.Tbl_Usuarios.Add(tbl_Usuarios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdRol = new SelectList(db.Tbl_Roles, "IdRol", "NombreRol", tbl_Usuarios.IdRol);
            return View(tbl_Usuarios);
        }

        // GET: RegistroUsuarios/Edit/5
        [AuthorizeUser(IdOperacion: 2)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Usuarios tbl_Usuarios = db.Tbl_Usuarios.Find(id);
            if (tbl_Usuarios == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdRol = new SelectList(db.Tbl_Roles, "IdRol", "NombreRol", tbl_Usuarios.IdRol);
            return View(tbl_Usuarios);
        }

        // POST: RegistroUsuarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdUsuario,NombreUsuario,ApellidoUsuario,CorreoUsuario,ContraseñaUsuario,IdRol")] Tbl_Usuarios tbl_Usuarios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Usuarios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdRol = new SelectList(db.Tbl_Roles, "IdRol", "NombreRol", tbl_Usuarios.IdRol);
            return View(tbl_Usuarios);
        }

        // GET: RegistroUsuarios/Delete/5
        [AuthorizeUser(IdOperacion: 4)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Usuarios tbl_Usuarios = db.Tbl_Usuarios.Find(id);
            if (tbl_Usuarios == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Usuarios);
        }

        // POST: RegistroUsuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tbl_Usuarios tbl_Usuarios = db.Tbl_Usuarios.Find(id);
            db.Tbl_Usuarios.Remove(tbl_Usuarios);
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
