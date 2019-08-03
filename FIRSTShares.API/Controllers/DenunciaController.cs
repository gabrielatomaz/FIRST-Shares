using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FIRSTShares.API.Models;
using FIRSTShares.Data;
using FIRSTShares.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FIRSTShares.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DenunciaController : ControllerBase
    {
        private readonly LazyContext BD;
        public Denuncia Denuncia { get; set; }
        public Usuario Usuario { get; set; }

        public DenunciaController(LazyContext bd)
        {
            BD = bd;

            Denuncia = new Denuncia(BD);
            Usuario = new Usuario(BD);
        }

        [HttpGet]
        public ActionResult<List<DenunciaModel>> Get()
        {
            var denuncias = Denuncia.RetornarTodasDenuncias();
            var denunciaModel = new List<DenunciaModel>();

            denuncias.ForEach(d =>
            {
                denunciaModel.Add(new DenunciaModel
                {
                    IDDenuncia = d.ID,
                    IDUsuario = d.UsuarioDenunciado.ID,
                    NomeUsuario = d.UsuarioDenunciado.NomeUsuario,
                    Motivo = d.Motivo,
                    Foto = Convert.ToBase64String(d.UsuarioDenunciado.Foto.FotoBase64)
                });
            });

            return Ok(denunciaModel);
        }

        [Route("Banir/{denuncia}")]
        [HttpGet]
        public IActionResult BanirUsuario(string denuncia)
        {
            var denunciaMotivoUsuario = denuncia.Split(",");

            if (Usuario.Excluir(Int32.Parse(denunciaMotivoUsuario[0])))
                return Ok();

            return NotFound();
        }

        [Route("CancelarDenuncia/{denuncia}")]
        [HttpGet]
        public IActionResult CancelarDenuncia(string denuncia)
        {
            var denunciaMotivoUsuario = denuncia.Split(",");

            if (Denuncia.Excluir(Int32.Parse(denunciaMotivoUsuario[0])))
                return Ok();

            return NotFound();
        }
    }
}