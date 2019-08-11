using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FIRSTShares.Data;
using FIRSTShares.Entities;
using FIRSTShares.Models;
using Microsoft.AspNetCore.Mvc;
using ReflectionIT.Mvc.Paging;

namespace FIRSTShares.Controllers
{
    public class ManualController : Controller
    {
        private readonly LazyContext BD;
        private Usuario Usuario;

        public ManualController(LazyContext bd)
        {
            BD = bd;

            Usuario = new Usuario(BD);
        }


        public async Task<IActionResult> Index(int page = 1)
        {
            var anexos = BD.Anexos
                .Where(a => !a.Excluido && a.TipoAnexo == TipoAnexo.Manual)
                .OrderByDescending(a => a.Titulo);

            var modelAnexos = await PagingList.CreateAsync(anexos, 6, page);

            MostrarFotoPerfil();

            return View(modelAnexos);
        }

        public IActionResult AdicionarManual([FromBody] ManualModel model)
        {
            var usuario = Usuario.RetornarUsuarioPorNomeUsuario(((ClaimsIdentity)User.Identity)
                .Claims.Single(u => u.Type == "NomeUsuario").Value);

            var anexo = new Anexo
            {
                SRC = model.SRC,
                TipoAnexo = TipoAnexo.Manual,
                Titulo = $"{model.Ano} - {model.Nome} (versão em {model.Idioma})",
                Usuario = usuario,
                DataCriacao = DateTime.Now
            };

            BD.Add(anexo);

            return BD.SaveChanges() > 0 ? Json("Manual adicionado com sucesso!") : Json("Falha ao adicionar manual, tente novamente mais tarde.");
        }

        public async Task<IActionResult> Excluir(int id)
        {
            var anexo = BD.Anexos.SingleOrDefault(a => a.ID == id);
            anexo.Excluido = true;

            BD.Update(anexo);
            BD.SaveChanges();
            
            var anexos = BD.Anexos
                .Where(a => !a.Excluido && a.TipoAnexo == TipoAnexo.Manual)
                .OrderByDescending(a => a.Titulo);

            var modelAnexos = await PagingList.CreateAsync(anexos, 6, 1);

            return View("Index", modelAnexos);
        }

        private void MostrarFotoPerfil()
        {
            if (User.Identity.IsAuthenticated)
            {
                var claims = (ClaimsIdentity)User.Identity;
                var usuario = Usuario.RetornarUsuarioPorNomeUsuario(claims.Claims.Single(u => u.Type == "NomeUsuario").Value);

                var foto = Convert.ToBase64String(usuario.Foto.FotoBase64);
                ViewData["foto"] = foto;
                ViewData["temNotificacao"] = usuario.NotificacoesRecebidas.Where(n => !n.Excluido).ToList().Count > 0;
            }
        }
    }
}