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
            //ARTICULO a = new ARTICULO();

            return View();
        }

        public ActionResult Inicio()
        {

            return View();
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

                    int q = query.Count();
                    if(q == 1)
                    {
                        return View("~/Views/User/Index.cshtml");
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
                using(var context = new ProyectoaDbContext())
                {
                    usuario.ADMINISTRADOR = "NO";
                    context.USUARIOS.Add(usuario);
                    context.SaveChanges();
                }
            }
            return View("Inicio");
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}