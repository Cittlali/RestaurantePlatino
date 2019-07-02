using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Practica.Models;


namespace Practica.Controllers
{
   
    public class HomeController : Controller
    {
       
        public IActionResult Index()
        {

            return View();
        }

        
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (username != null && password != null && username.Equals("Administrador") && password.Equals("admin"))
            {
                HttpContext.Session.SetString("username", username);
                return View("Formularios/Inicio");
            }
            else
            {
                ViewBag.error = "Invalid Account";
                return View("Index");
            }
        }

      
    



    public IActionResult About()
        {
            ViewData["Message"] = "La página de descripción de su aplicación.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Su página de contacto.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
