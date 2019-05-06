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
using System.Security.Claims;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace FIRSTShares.Controllers
{
    public class HomeController : Controller
    {
        private readonly DatabaseContext Bd;
        public Categoria Categoria { get; set; }
        public Usuario Usuario { get; set; }

        public HomeController(DatabaseContext bd)
        {
            Bd = bd;

            Categoria = new Categoria(Bd);

            Usuario = new Usuario(Bd);
        }

        public IActionResult Index()
        {
            var categorias = Bd.Categorias.Where(c => c.Excluido == false).ToList();
            var postagens = Bd.Postagens.Include(p => p.Usuario)
                .Include(p => p.Discussao)
                .Include(p => p.Categoria)
                .Where(p => p.Excluido == false).OrderByDescending(p => p.DataCriacao).ToList();

            var modelTupleCategoriasPostagens = new Tuple<List<Categoria>, List<Postagem>>(categorias, postagens);

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
                Conteudo = conteudo.Conteudo,
                DataCriacao = dataAtual,
                Usuario = usuario,
                Categoria = categoria
            };

            SalvarPostagem(postagem);
        }

        public string SalvarPostagem(Postagem postagem)
        {
            Bd.Postagens.Add(postagem);

             return Bd.SaveChanges() > 0 ?
                "Sucesso ao adicionar post" : ViewBag.Mensagem = "Falha ao adicionar post";
        }

        public Discussao AdicionarDiscussao(string assunto, DateTime dataAtual)
        {
            var discussao = new Discussao
            {
                Assunto = assunto,
                DataCriacao = dataAtual
            };

            Bd.Discussoes.Add(discussao);

            var salvar = Bd.SaveChanges() > 0 ? true : false;

            return salvar ? discussao : null;
        }

    }
}
