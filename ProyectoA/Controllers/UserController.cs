using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoA.Models;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Data.Entity.Infrastructure;

namespace ProyectoA.Controllers
{
    public class UserController : Controller
    {
        public static string loggedUser;
        public static string detalle;
        public ActionResult Index()
        {
            return View("~/Views/Home/Index.cshtml");
        }
        [HttpGet]
        public ViewResult nuevoArticulo()
        {
            return View();
        }

        [HttpPost]
        public ViewResult nuevoArticulo(ARTICULO articulo, HttpPostedFileBase upload1, HttpPostedFileBase upload2, HttpPostedFileBase upload3)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HttpPostedFileBase[] upload = new HttpPostedFileBase[3] {upload1, upload2, upload3};

                    for (int i = 0; i < 3; i++)
                    {
                        if (upload[i] != null && upload[i].ContentLength > 0)
                        {
                            byte[] imagenData = null;
                            using (var imagen = new BinaryReader(upload[i].InputStream))
                            {
                                imagenData = imagen.ReadBytes(upload[i].ContentLength);
                            }
                            if (i == 0) {
                                articulo.IMAGEN1 = imagenData;
                            }else if (i == 1)
                            {
                                articulo.IMAGEN2 = imagenData;
                            }else if (i == 2)
                            {
                                articulo.IMAGEN3 = imagenData;
                            }
                            
                        }

                    }
                    
                    
                    using (var context = new ProyectoaDbContext())
                    {
                        articulo.FECHA_PUBLICACION = System.DateTime.Today;
                        articulo.ID_USUARIO = loggedUser;
                        context.ARTICULO.Add(articulo);
                        context.SaveChanges();
                        return View("Index");

                    }
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View();
        }
        public ActionResult cerrarSesion()
        {
            ViewBag.loggedUser = "";
            loggedUser = "";
            return View("~/Views/Home/Index.cshtml");
        }

        public ActionResult convertirImagenA(int codigo, int img)
        {
           
            using (var context = new ProyectoaDbContext())
            {
                var imagen = (from articulo in context.ARTICULO
                             where articulo.ID_ARTICULO == codigo
                             select articulo).FirstOrDefault();
                if (img==1)
                {
                    return File(imagen.IMAGEN1, "Imagenes/jpg");
                }
                else if (img==2)
                {
                    return File(imagen.IMAGEN2, "Imagenes/jpg");
                }
                else if (img==3)
                {
                    return File(imagen.IMAGEN3, "Imagenes/jpg");
                }
                else
                {
                    return View("Index");
                };

            }

        }
        public ActionResult convertirImagenU(string codigo)
        {

            using (var context = new ProyectoaDbContext())
            {
                var imagen = (from u in context.USUARIOS
                              where u.ID_USUARIO == codigo
                              select u.FOTO).FirstOrDefault();
                return File(imagen, "Imagenes/jpg");
            }

        }
        public ViewResult Perfil()
        {
            ViewBag.logged = loggedUser;
            USUARIOS u = new USUARIOS();
            return View(u.Listar());
        }
        
        public ActionResult cambiarFoto(USUARIOS usuario, HttpPostedFileBase cambio)
        {
            using (var context = new ProyectoaDbContext())
            {
                var update = (from u in context.USUARIOS
                              where u.ID_USUARIO == loggedUser
                              select u);

                foreach(USUARIOS u in update)
                {
                    if(cambio != null && cambio.ContentLength > 0)
                    {
                        byte[] imagenData = null;
                        using (var imagen = new BinaryReader(cambio.InputStream))
                        {
                            imagenData = imagen.ReadBytes(cambio.ContentLength);
                        }
                        u.FOTO = imagenData;
                    }
                }
                try
                {
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                
                return View("Perfil", usuario.Listar());
            }
               
        }

        [HttpGet]
        public ActionResult Detalles()
        {
            var det = Request.QueryString["id"];
            ViewBag.id = det;
            detalle = det;
            ARTICULO a = new ARTICULO();
            return View(a.Listar());
        }

        public ActionResult Articulos()
        {
            ViewBag.id = loggedUser;
            ARTICULO a = new ARTICULO();
            return View(a.Listar());
        }
    }
}