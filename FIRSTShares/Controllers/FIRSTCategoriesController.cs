using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FIRSTShares.Data;
using FIRSTShares.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FIRSTShares.Controllers
{
    public class FIRSTCategoriesController : Controller
    {
        private readonly LazyContext BD;
        private Usuario Usuario;

        public FIRSTCategoriesController(LazyContext bd)
        {
            BD = bd;
            Usuario = new Usuario(BD);
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var claims = (ClaimsIdentity)User.Identity;
                var usuario = Usuario.RetornarUsuarioPorNomeUsuario(claims.Claims.Single(u => u.Type == "NomeUsuario").Value);

                var foto = Convert.ToBase64String(usuario.Foto.FotoBase64);
                ViewData["foto"] = foto;
                ViewData["temNotificacao"] = usuario.NotificacoesRecebidas.Where(n => !n.Excluido).ToList().Count > 0;
            }

            return View();
        }
    }
}