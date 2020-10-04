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
    // Clase para el registro de nuevos grados
    public class RegistroGradosController : Controller
    {
        // Variable que hace referencia y conexión a la base de datos
        private NCEntities1 db = new NCEntities1();

        // Método para mostrar el contenido de la tabla (Tbl_Grados)
        public ActionResult Index()
        {
            return View(db.Tbl_Grados.ToList());
        }

        // Clase para ver los detalles de cada grado o la información requerida
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

        [AuthorizeUser(IdOperacion: 6)]
        public ActionResult Create()
        {
            return View();
        }

        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        // Clase para registrar un nuevo grado
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

        [AuthorizeUser(IdOperacion: 7)]

        // Clase para la edición previa de la información ingresada
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

        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        [HttpPost]

        // Esta función se encarga de generar una cookie única, la cual validará el apartado de edición una vez se esté dentro de este y
        // Se realice la solicitud para modificar la información deseada, si no son las mismas cookies una vez se realice la solicitud
        // Nos mostrará un error
        [ValidateAntiForgeryToken]

        // Clase para editar la información de un grado
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

        [AuthorizeUser(IdOperacion: 9)]

        // Clase para eliminar un grado del sitio web
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

        
        [HttpPost, ActionName("Delete")]

        // Esta función se encarga de generar una cookie única, la cual validará el apartado de eliminación una vez se esté dentro de este y
        // Se realice la solicitud para eliminar el usuario, si no son las mismas cookies una vez se realice la solicitud
        // Nos mostrará un error
        [ValidateAntiForgeryToken]

        // Este método es la confirmación para eliminar un grado
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
