using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FIRSTShares.Models;
using FIRSTShares.Data;
using FIRSTShares.Entities;
using System.Security.Claims;
using ReflectionIT.Mvc.Paging;

namespace FIRSTShares.Controllers
{
    public class HomeController : Controller
    {
        private readonly LazyContext BD;
        private Usuario Usuario;
        public Categoria Categoria;
        public Postagem Postagem;
        public Discussao Discussao;

        public HomeController(LazyContext bd)
        {
            BD = bd;

            Categoria = new Categoria(BD);

            Usuario = new Usuario(BD);

            Postagem = new Postagem(BD);

            Discussao = new Discussao(BD);
        }

        public async Task<IActionResult> Index(string search, int page = 1)
        {
            var categorias = BD.Categorias.Where(c => c.Excluido == false).ToList();

            IOrderedQueryable<Postagem> postagens;

            if (!string.IsNullOrEmpty(search))
            {
                postagens = BD.Postagens
                .Where(p => p.Excluido == false && p.PostagemPai == null && !p.PostagemOficial && (p.Discussao.Assunto.Contains(search) || p.Categoria.Nome.Contains(search)))
                .Select(p => new Postagem
                {
                    Discussao = p.Discussao,
                    Usuario = p.Usuario,
                    Categoria = p.Categoria,
                    DataCriacao = p.DataCriacao,
                    Conteudo = p.Conteudo,
                    Excluido = p.Excluido,
                    ID = p.ID
                })
                .OrderByDescending(p => p.DataCriacao);
            }
            else
            {
                postagens = BD.Postagens
                    .Where(p => p.Excluido == false && p.PostagemPai == null && !p.PostagemOficial)
                    .Select(p => new Postagem
                    {
                        Discussao = p.Discussao,
                        Usuario = p.Usuario,
                        Categoria = p.Categoria,
                        DataCriacao = p.DataCriacao,
                        Conteudo = p.Conteudo,
                        Excluido = p.Excluido,
                        ID = p.ID
                    })
                    .OrderByDescending(p => p.DataCriacao);
            }

            MostrarFotoPerfil();

            var modelPostagens = await PagingList.CreateAsync(postagens, 5, page);

            var usuarios = Usuario.RetornarUsuarios().OrderByDescending(u => u.Postagens.Where(p => !p.Excluido).Count()).Take(5).ToList();

            var modelTupleCategoriasPostagens = new Tuple<List<Categoria>, PagingList<Postagem>, List<Usuario>>(categorias, modelPostagens, usuarios);

            return View(modelTupleCategoriasPostagens);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public void AdicionarPostagem([FromBody] PostagemModel conteudo)
        {
            var dataAtual = DateTime.Now;
            var claims = (ClaimsIdentity)User.Identity;

            var categoria = Categoria.RetornarCategoriaPorId(conteudo.Categoria);
            var usuario = Usuario.RetornarUsuarioPorNomeUsuario(claims.Claims.Single(u => u.Type == "NomeUsuario").Value);

            var postagem = new Postagem
            {
                Discussao = Discussao.Adicionar(conteudo.Assunto, dataAtual),
                ConteudoHtml = conteudo.ConteudoHtml,
                Conteudo = conteudo.Conteudo,
                DataCriacao = dataAtual,
                Usuario = usuario,
                Categoria = categoria,
                PostagemOficial = conteudo.PostagemOficial
            };

            Postagem.Salvar(postagem);
        }

        private void MostrarFotoPerfil()
        {
            if (User.Identity.IsAuthenticated)
            {
                var claims = (ClaimsIdentity)User.Identity;
                var usuario = Usuario.RetornarUsuarioPorNomeUsuario(claims.Claims.Single(u => u.Type == "NomeUsuario").Value);

                var foto = Convert.ToBase64String(usuario.Foto.FotoBase64);
                ViewData["foto"] = foto;
            }
        }
    }
}
