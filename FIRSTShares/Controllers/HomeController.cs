using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FIRSTShares.Models;
using FIRSTShares.Data;
using FIRSTShares.Entities;
using FIRSTShares.Util;

namespace FIRSTShares.Controllers
{
    public class HomeController : Controller
    {
        private readonly DatabaseContext Bd;

        public HomeController(DatabaseContext bd)
        {
            Bd = bd;
        }

        public IActionResult Index()
        {
            ViewBag.Usuario = HttpContext.Session.GetObject<Usuario>("Usuario");
            var categorias = Bd.Categorias.ToList();

            return View(categorias);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public void AdicionarPostagem([FromBody] PostagemModel conteudo)
        {
            var p = conteudo.Conteudo;
        }
    }
}
