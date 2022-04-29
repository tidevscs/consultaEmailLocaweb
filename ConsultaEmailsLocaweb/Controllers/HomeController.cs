using ConsultaEmailsLocaweb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ConsultaEmailsLocaweb.Controllers
{
    public class HomeController : Controller
    {
        
        private IConfiguration _configuration;
       
         public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
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

        
        public IActionResult autenticar()
        {
            string usuario= Request.Form["username"];
            string senha = Request.Form["password"];
            
            Autenticacao aut = new Autenticacao();
            Boolean autenticado  = aut.autenticar(usuario, senha);

            if (autenticado )
            {
                //Response.Redirect("/home/emails");
                Response.Cookies.Append("autenticado","true");

                return RedirectToAction("emails", "home");
            }
            else
            {
                Response.Cookies.Append("autenticado", "false");
                return RedirectToAction("", "Home", new { msg = "Usuario ou senha inválidos" });
            }



        }

     
        public IActionResult emails(Emails emails)
        {
            string cookie = Request.Cookies["autenticado"];

            if (cookie == "true")
            {

                return View();
            }
            else
            {
                return RedirectToAction("", "Home");
            }
        }

        
        public IActionResult pesquisa_emails2()
        {
            
            string link = "";
            if (Request.Query.Count > 0) {
                link = Request.Query["link"] + "&status=all&start_date=" + Request.Query["start_date"] + "&page=" + Request.Query["page"] + "&per=" + Request.Query["per"];
            }
            
            Emails emails = new Emails();

            string apikey = _configuration["APIKey"];
            emails = emails.buscar(apikey,link);

            ViewBag.links = emails.links;
            ViewBag.emails = emails.data.messages;

            return View();
        }


        public IActionResult pesquisa_emails()
        {
            string cookie = Request.Cookies["autenticado"];

            if (cookie == "true") { 
            Emails emails = new Emails();
                
             string destinatario = Request.Form["destinatario"];

                string datainicial = Request.Form["data_inicial"];
                string datafinal = Request.Form["data_final"];

                string html = emails.buscar_por_destinatario(destinatario,datainicial,datafinal);


            ViewBag.html = html;
            

            return View();
            }
            else
            {
                return RedirectToAction("", "Home");
            }
        }

    }
}
