using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoA.Models;

namespace ProyectoA.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            

            return View();
        }

        public ActionResult Inicio()
        {
            ARTICULO a = new ARTICULO();
            return View(a.Listar());
        }

        [HttpGet]
        public ViewResult Login()
        {
            return View();
        }

        [HttpPost]
        public ViewResult Login(USUARIOS usuario)
        {
            using(var context = new ProyectoaDbContext())
            {
                try
                {
                    var query = from user in context.USUARIOS
                                where user.ID_USUARIO == usuario.ID_USUARIO && user.PASS == usuario.PASS
                                select user.ID_USUARIO;
                    var admin =( from user in context.USUARIOS
                                where user.ID_USUARIO == usuario.ID_USUARIO && user.PASS == usuario.PASS
                                select user).FirstOrDefault();

                    int q = query.Count();
                    if(q == 1)
                    {
                        if (admin.ADMINISTRADOR=="SI")
                        {
                            AdminController.loggedUser = usuario.ID_USUARIO;
                            ARTICULO a = new ARTICULO();
                            return View("~/Views/Admin/Index.cshtml",a.Listar());
                        }
                        else
                        {

                            UserController.loggedUser = usuario.ID_USUARIO;
                            return View("~/Views/User/Index.cshtml");
                        }
                    }
                    else
                    {
                        return View();
                    }
                }catch(Exception ex)
                {
                    throw ex;
                }
                
                
            }
        }

        [HttpGet]
        public ViewResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ViewResult Signup(USUARIOS usuario)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    using (var context = new ProyectoaDbContext())
                    {
                        var query = from user in context.USUARIOS
                                where user.ID_USUARIO == usuario.ID_USUARIO
                                select user.ID_USUARIO;

                    int q = query.Count();
                    if (q == 1)
                    {
                        return View();
                    }
                    else
                    {
                            usuario.ADMINISTRADOR = "NO";
                            context.USUARIOS.Add(usuario);
                            context.SaveChanges();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                
            }
            return View("Login");
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}