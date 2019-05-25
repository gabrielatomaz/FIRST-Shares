using FIRSTShares.API.Models;
using FIRSTShares.Data;
using FIRSTShares.Entities;
using FIRSTShares.Util;
using Microsoft.AspNetCore.Mvc;
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
                    CargoTime = u.CargoTime,
                    DataCriacao = u.DataCriacao,
                    Email = u.Email,
                    Senha = u.Senha,
                    Foto = u.Foto,
                    Excluido = u.Excluido,
                    Time = new TimeModel
                    {
                        ID = u.Time.ID,
                        CodPais = u.Time.CodPais,
                        Nome = u.Time.Nome,
                        Numero = u.Time.Numero,
                        Pais = u.Time.Pais,
                        Excluido = u.Time.Excluido
                    },
                    Cargo = new CargoModel
                    {
                        ID = u.Cargo.ID,
                        Tipo = u.Cargo.Tipo,
                        Excluido = u.Cargo.Excluido
                    }
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
                    && u.Cargo.Tipo == CargoTipo.Administrador
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
                Email = usuario.Email
            };

            return Ok(usuarioModel);
        }


        [Route("AlterarCargo/{id}")]
        [HttpPut("{id}")]
        public ActionResult AlterarCargo(int id, [FromBody] int idCargo)
        {
            var usuarioAlterado = Usuario.AlterarCargoUsuario(id, idCargo);

            if (usuarioAlterado)
                return Ok("Alterado com sucesso!");

            return NotFound();
        }

        [Route("Excluir/{id}")]
        [HttpPut("{id}")]
        public ActionResult Excluir(int id)
        {
            var usuarioAlterado = Usuario.Excluir(id);

            if (usuarioAlterado)
                return Ok("Excluido com sucesso!");

            return NotFound();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
