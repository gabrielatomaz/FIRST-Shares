using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FIRSTShares.Data;
using FIRSTShares.Entities;
using FIRSTShares.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FIRSTShares.Controllers
{
    public class PostController : Controller
    {
        public readonly LazyContext BD;
        public Usuario Usuario;
        public PostController(LazyContext bd)
        {
            BD = bd;

            Usuario = new Usuario(BD);
        }

        public IActionResult Index(int id)
        {
            var postagem = BD.Postagens.Single(p => p.ID == id);

            return View(postagem);
        }

        [HttpPost]
        public void AdicionarComentario([FromBody] ComentarioModel comentario)
        {
            var postagemPai = BD.Postagens.Single(p => p.ID == comentario.IDPostagemPai);

            var comentarioPostagem = new Postagem
            {
                Categoria = postagemPai.Categoria,
                ConteudoHtml = comentario.Conteudo,
                DataCriacao = DateTime.Now,
                Discussao = postagemPai.Discussao,
                PostagemPai = postagemPai,
                Usuario = RetornarUsuarioLogado()
            };

            SalvarComentario(comentarioPostagem);
        }

        private Usuario RetornarUsuarioLogado()
        {
            var claims = (ClaimsIdentity)User.Identity;

            return Usuario.RetonarUsuarioPorNomeUsuario(claims.Claims.Single(u => u.Type == "NomeUsuario").Value);
        }

        private string SalvarComentario(Postagem postagem)
        {
            BD.Postagens.Add(postagem);

            return BD.SaveChanges() > 0 ?
               "Sucesso ao adicionar comentário" : ViewBag.Mensagem = "Falha ao adicionar comentário";
        }
    }
}