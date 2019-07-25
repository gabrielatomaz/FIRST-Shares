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
    public class ResourcesController : Controller
    {
        private readonly LazyContext BD;
        private Usuario Usuario;

        public ResourcesController(LazyContext bd)
        {
            BD = bd;

            Usuario = new Usuario(BD);
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var postagens = BD.Postagens
                .Where(p => !p.Excluido && p.PostagemPai == null && p.PostagemOficial)
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

            var modelPostagens = await PagingList.CreateAsync(postagens, 5, page);

            return View(modelPostagens);
        }

        public IActionResult GetDataVideo(int pageIndex, int pageSize)
        {
            var anexos = BD.Anexos.Where(a => !a.Excluido && a.TipoAnexo == TipoAnexo.Video)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .Select(a => new { a.DataCriacao, a.TipoAnexo, a.Titulo, idUsuario = a.Usuario.ID, a.SRC, a.Resumo, a.Usuario.NomeUsuario, a.ID })
                .OrderByDescending(a => a.DataCriacao).ToList();

            return Json(anexos);
        }

        public IActionResult GetDataPDF(int pageIndex, int pageSize)
        {
            var anexos = BD.Anexos.Where(a => !a.Excluido && a.TipoAnexo == TipoAnexo.PDF)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .Select(a => new { a.DataCriacao, a.TipoAnexo, a.Titulo, idUsuario = a.Usuario.ID, a.SRC, a.Resumo, a.Usuario.NomeUsuario, a.ID })
                .OrderByDescending(a => a.DataCriacao).ToList();

            return Json(anexos);
        }

        public IActionResult Excluir(int id)
        {
            var anexo = BD.Anexos.SingleOrDefault(a => a.ID == id);
            anexo.Excluido = true;

            BD.Update(anexo);
            BD.SaveChanges();

            var anexos = BD.Anexos.Where(a => !a.Excluido).OrderByDescending(a => a.DataCriacao).ToList();

            return View("Index", anexos);
        }

        public IActionResult AdicionarAnexo([FromBody] AnexoModel model)
        {
            var usuario = Usuario.RetornarUsuarioPorNomeUsuario(((ClaimsIdentity)User.Identity)
                .Claims.Single(u => u.Type == "NomeUsuario").Value);

            var anexo = new Anexo
            {
                Resumo = model.Descricao,
                SRC = model.SRC,
                TipoAnexo = model.Tipo,
                Titulo = model.Titulo,
                Usuario = usuario,
                DataCriacao = DateTime.Now
            };

            BD.Add(anexo);

            return BD.SaveChanges() > 0 ? Json("Anexo adicionado com sucesso!") : Json("Falha ao adicionar anexo, tente novamente mais tarde.");
        }
    }
}