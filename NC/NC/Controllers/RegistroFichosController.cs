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
    // Clase para el registro de fichos
    public class RegistroFichosController : Controller
    {
        // Variable que hace referencia y conexión a la base de datos
        private NCEntities db = new NCEntities();

        // Método para mostrar el contenido de la tabla (Tbl_Fichos)
        public ActionResult Index()
        {
            var tbl_Fichos = db.Tbl_Fichos.Include(t => t.Tbl_Usuarios);
            return View(tbl_Fichos.ToList());
        }

        // Clase para ver los detalles de cada ficho
        public ActionResult Details(int? id)
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

        [AuthorizeUser(IdOperacion: 11)]
        public ActionResult Create()
        {
            ViewBag.IdUsuario = new SelectList(db.Tbl_Usuarios, "IdUsuario", "NombreUsuario", "HoraFicho");
            return View();
        }

        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        [HttpPost]
        [ValidateAntiForgeryToken]

        // Clase para registrar un nuevo ficho
        public ActionResult Create([Bind(Include = "IdFicho,FechaFicho,IdUsuario,HoraFicho")] Tbl_Fichos tbl_Fichos)
        {
            if (ModelState.IsValid)
            {
                db.Tbl_Fichos.Add(tbl_Fichos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdUsuario = new SelectList(db.Tbl_Usuarios, "IdUsuario", "NombreUsuario", "HoraFicho", tbl_Fichos.IdUsuario);
            return View(tbl_Fichos);
        }

        [AuthorizeUser(IdOperacion: 1)]

        // Clase para la edición previa de la información ingresada
        public ActionResult Edit(int? id)
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

        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        [HttpPost]

        // Esta función se encarga de generar una cookie única, la cual validará el apartado de edición una vez se esté dentro de este y
        // Se realice la solicitud para modificar la información deseada, si no son las mismas cookies una vez se realice la solicitud
        // Nos mostrará un error
        [ValidateAntiForgeryToken]

        // Clase para editar la información de un ficho
        public ActionResult Edit([Bind(Include = "IdFicho,FechaFicho,IdUsuario,HoraFicho")] Tbl_Fichos tbl_Fichos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Fichos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdUsuario = new SelectList(db.Tbl_Usuarios, "IdUsuario", "NombreUsuario", "HoraFicho", tbl_Fichos.IdUsuario);
            return View(tbl_Fichos);
        }

        // Clase para eliminar un ficho del sitio web
        [AuthorizeUser(IdOperacion: 1)]
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

        
        [HttpPost, ActionName("Delete")]

        // Esta función se encarga de generar una cookie única, la cual validará el apartado de eliminación una vez se esté dentro de este y
        // Se realice la solicitud para eliminar el usuario, si no son las mismas cookies una vez se realice la solicitud
        // Nos mostrará un error
        [ValidateAntiForgeryToken]

        // Este método es la confirmación para eliminar un ficho
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
