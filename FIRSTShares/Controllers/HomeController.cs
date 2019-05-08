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
        public Categoria Categoria { get; set; }

        public HomeController(LazyContext bd)
        {
            BD = bd;

            Categoria = new Categoria(BD);

            Usuario = new Usuario(BD);
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var categorias = BD.Categorias.Where(c => c.Excluido == false).ToList();
            var postagens = BD.Postagens.Where(p => p.Excluido == false && p.PostagemPai == null).OrderByDescending(p => p.DataCriacao);
            var modelPostagens = await PagingList.CreateAsync(postagens, 5, page);

            var modelTupleCategoriasPostagens = new Tuple<List<Categoria>, PagingList<Postagem>>(categorias, modelPostagens);

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
            var usuario = Usuario.RetonarUsuarioPorNomeUsuario(claims.Claims.Single(u => u.Type == "NomeUsuario").Value);

            var postagem = new Postagem
            {
                Discussao = AdicionarDiscussao(conteudo.Assunto, dataAtual),
                ConteudoHtml = conteudo.ConteudoHtml,
                Conteudo = conteudo.Conteudo,
                DataCriacao = dataAtual,
                Usuario = usuario,
                Categoria = categoria
            };

            SalvarPostagem(postagem);
        }

        private string SalvarPostagem(Postagem postagem)
        {
            BD.Postagens.Add(postagem);

            return BD.SaveChanges() > 0 ?
               "Sucesso ao adicionar post" : ViewBag.Mensagem = "Falha ao adicionar post";
        }

        private Discussao AdicionarDiscussao(string assunto, DateTime dataAtual)
        {
            var discussao = new Discussao
            {
                Assunto = assunto,
                DataCriacao = dataAtual
            };

            BD.Discussoes.Add(discussao);

            var salvar = BD.SaveChanges() > 0 ? true : false;

            return salvar ? discussao : null;
        }
    }
}
