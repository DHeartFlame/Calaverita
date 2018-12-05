using ProyectoA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoA.Controllers
{
    public class AdminController : Controller
    {
        public static string loggedUser;
        public static string idedit;

        public ActionResult Index()
        {
            ARTICULO a = new ARTICULO();
            return View(a.Listar());
        }

        public ActionResult adminUsuarios()
        {
            USUARIOS u = new USUARIOS();
            return View(u.Listar());
        }

        [HttpPost]
        public ActionResult Borrar(string Borrar)
        {
            idedit = Borrar;
            using (var context = new ProyectoaDbContext())
            {
                var update = (from u in context.USUARIOS
                             where u.ID_USUARIO == idedit
                             select u).ToList();

                DELETED_US d = new DELETED_US();
                foreach (var item in update)
                {
                    d.FECHA_DEL = System.DateTime.Today;
                    d.ADMINISTRADOR = item.ADMINISTRADOR;
                    d.NOMBRE = item.NOMBRE;
                    d.APELLIDO = item.APELLIDO;
                    d.CIUDAD = item.CIUDAD;
                    d.CORREO = item.CORREO;
                    d.PASS = item.PASS;
                }

                context.SaveChanges();
            }
            USUARIOS user = new USUARIOS();
            return View("adminUsuarios", user.Listar());
        }

        [HttpGet]
        public ActionResult Editar()
        {
            var edit = Request.QueryString["Editar"];
            ViewBag.id = edit;
            idedit = edit;
            USUARIOS u = new USUARIOS();
            return View(u.Listar());
        }

        [HttpPost]
        public ActionResult Editar(USUARIOS usuario)
        {
            if (ModelState.IsValid)
            {
                    using (var context = new ProyectoaDbContext())
                    {
                        var update = (from user in context.USUARIOS
                                      where user.ID_USUARIO == idedit
                                      select user).ToList();
                        foreach(var item in update)
                        {
                            item.ADMINISTRADOR = usuario.ADMINISTRADOR;
                            item.NOMBRE = usuario.NOMBRE;
                            item.APELLIDO = usuario.APELLIDO;
                            item.CIUDAD = usuario.CIUDAD;
                            item.CORREO = usuario.CORREO;
                            item.PASS = usuario.PASS;
                        }
                        
                        
                        context.SaveChanges();
                        return View("adminUsuarios", usuario.Listar());
                    }
                }
            return View("adminUsuarios");
        }
            
        
    }
}