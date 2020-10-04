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
    // Clase para el registro de usuarios
    public class RegistroUsuariosController : Controller
    {
        // Variable que hace referencia y conexión a la base de datos
        private NCEntities db = new NCEntities();

        [AuthorizeUser(IdOperacion:5)]

        // Método para mostrar el contenido de la tabla (Tbl_Usuarios)
        public ActionResult Index()
        {
            var tbl_Usuarios = db.Tbl_Usuarios.Include(t => t.Tbl_Roles);

            return View(tbl_Usuarios.ToList());
        }

        [AuthorizeUser(IdOperacion: 3)]

        // Clase para ver los detalles de cada usuario
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
 
        public ActionResult Create()
        {
            ViewBag.IdRol = new SelectList(db.Tbl_Roles, "IdRol", "NombreRol");
            return View();
        }

     
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        // Clase para registrar un nuevo usuario
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

        [AuthorizeUser(IdOperacion: 2)]

        // Clase para la edición previa de la información ingresada
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

        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        [HttpPost]

        // Esta función se encarga de generar una cookie única, la cual validará el apartado de edición una vez se esté dentro de este y
        // Se realice la solicitud para modificar la información deseada, si no son las mismas cookies una vez se realice la solicitud
        // Nos mostrará un error
        [ValidateAntiForgeryToken]

        // Clase para editar la información de un usuario
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

        [AuthorizeUser(IdOperacion: 4)]

        // Clase para eliminar un usuario del sitio web
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

        [HttpPost, ActionName("Delete")]

        // Esta función se encarga de generar una cookie única, la cual validará el apartado de eliminación una vez se esté dentro de este y
        // Se realice la solicitud para eliminar el usuario, si no son las mismas cookies una vez se realice la solicitud
        // Nos mostrará un error
        [ValidateAntiForgeryToken]

        // Este método es la confirmación para eliminar un usuario
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
