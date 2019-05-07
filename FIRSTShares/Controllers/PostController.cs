using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FIRSTShares.Data;
using FIRSTShares.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FIRSTShares.Controllers
{
    public class PostController : Controller
    {
        public readonly LazyContext BD;
        public PostController(LazyContext bd)
        {
            BD = bd;
        }

        public IActionResult Index(int id)
        {
            var postagem = BD.Postagens.Single(p => p.ID == id);

            return View(postagem);
        }
    }
}