using FIRSTShares.API.Models;
using FIRSTShares.Data;
using FIRSTShares.Entities;
using FIRSTShares.Util;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FIRSTShares.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly LazyContext BD;
        public Usuario Usuario { get; set; }

        public UsuarioController(LazyContext bd)
        {
            BD = bd;

            Usuario = new Usuario(BD);
        }

        [HttpGet]
        public ActionResult<List<UsuarioModel>> Get()
        {
            var usuarios = Usuario.RetornarUsuarios();
            var usuariosModel = new List<UsuarioModel>();

            usuarios.ForEach(u =>
            {
                usuariosModel.Add(new UsuarioModel
                {
                    ID = u.ID,
                    Nome = u.Nome,
                    NomeUsuario = u.NomeUsuario,
                    CargoTipo = u.Cargo.Tipo.ToString(),
                    Foto = Convert.ToBase64String(u.Foto.FotoBase64)
                });
            });

            return Ok(usuariosModel);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [Route("RetornarUsuarioAutenticado/{nomeUsuario}/{senha}")]
        [HttpGet]
        public ActionResult<UsuarioModel> RetornarUsuarioAutenticado(string nomeUsuario, string senha)
        {
            var usuario = Usuario.RetornarUsuarios().Find(u =>
                    (u.NomeUsuario == nomeUsuario || u.Email == nomeUsuario) 
                    && (u.Cargo.Tipo == CargoTipo.Administrador || u.Cargo.Tipo == CargoTipo.Moderador)
                    && u.Excluido == false
                );

            if (usuario == null)
                return NotFound();

            if (!Criptografia.Compara(usuario.Senha, senha))
                return NotFound();

            var usuarioModel = new UsuarioModel
            {
                ID = usuario.ID,
                Nome = usuario.Nome,
                NomeUsuario = usuario.NomeUsuario,
                Email = usuario.Email,
                Foto = Convert.ToBase64String(usuario.Foto.FotoBase64),
                CargoTipo = usuario.Cargo.Tipo.ToString()
            };

            return Ok(usuarioModel);
        }


        [Route("AlterarCargo/{idUsuario}/{novoCargo}")]
        [HttpGet]
        public ActionResult AlterarCargo(int idUsuario, string novoCargo)
        {
            var enumCargo = (CargoTipo)Enum.Parse(typeof(CargoTipo), novoCargo, true);
            var usuarioAlterado = Usuario.AlterarCargoUsuario(idUsuario, (int)enumCargo);

            if (usuarioAlterado)
                return Ok("Cargo alterado com sucesso!");

            return NotFound();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var usuarioAlterado = Usuario.Excluir(id);

            if (usuarioAlterado)
                return Ok("Excluido com sucesso!");

            return NotFound();
        }
    }
}
