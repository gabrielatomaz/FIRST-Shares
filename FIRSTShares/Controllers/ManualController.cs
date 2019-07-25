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
    }
}