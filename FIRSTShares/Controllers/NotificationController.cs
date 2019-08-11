using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FIRSTShares.Data;
using FIRSTShares.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;

namespace FIRSTShares.Controllers
{
    public class NotificationController : Controller
    {
        public readonly LazyContext BD;
        public Usuario Usuario;

        public NotificationController(LazyContext bd)
        {
            BD = bd;
            Usuario = new Usuario(BD);
        }

        public IActionResult Index()
        {
            MostrarFotoPerfil();

            var notificacoes = RetornarUsuarioLogado().NotificacoesRecebidas.Where(n => !n.Excluido);

            return View(notificacoes);
        }

        public IActionResult ExcluirNotificacoes(int userId)
        {
            var notificacoes = Usuario.RetornarUsuario(userId).NotificacoesRecebidas;

            notificacoes.ToList().ForEach(n =>
            {
                n.Excluido = true;
                BD.Update(n);
                BD.SaveChanges();
            });

            return RedirectToAction("Index", "Home");
        }

        public IActionResult GetNotification(int pageIndex, int pageSize)
        {
            var notificacoes = RetornarUsuarioLogado().NotificacoesRecebidas.Where(a => !a.Excluido)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .Select(a => new { a.Data, a.Acao, a.UsuarioAcao.Nome, a.UsuarioAcao.NomeUsuario, a.UsuarioAcao.ID})
                .OrderByDescending(a => a.Data).ToList();
            
            return Json(notificacoes);
        }

        private Usuario RetornarUsuarioLogado()
        {
            var claims = (ClaimsIdentity)User.Identity;
            return Usuario.RetornarUsuarioPorNomeUsuario(claims.Claims.Single(u => u.Type == "NomeUsuario").Value);
        }

        private void MostrarFotoPerfil()
        {
            if (User.Identity.IsAuthenticated)
            {
                var usuario = RetornarUsuarioLogado();

                var foto = Convert.ToBase64String(usuario.Foto.FotoBase64);
                ViewData["foto"] = foto;
                ViewData["temNotificacao"] = usuario.NotificacoesRecebidas.Where(n => !n.Excluido).ToList().Count > 0;
            }
        }
    }
}