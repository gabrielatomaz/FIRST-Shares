using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FIRSTShares.Data;
using FIRSTShares.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FIRSTShares.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        public readonly LazyContext BD;

        private Usuario Usuario;

        public ProfileController(LazyContext bd)
        {
            BD = bd;
            Usuario = new Usuario(BD);
        }

        public IActionResult Index()
        {
            var claims = (ClaimsIdentity)User.Identity;

            var usuario = Usuario.RetonarUsuarioPorNomeUsuario(claims.Claims.Single(u => u.Type == "NomeUsuario").Value);
            return View(usuario);
        }
    }
}