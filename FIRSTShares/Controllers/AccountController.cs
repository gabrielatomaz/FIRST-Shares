using System;
using System.IO;
using System.Linq;
using FIRSTShares.Data;
using FIRSTShares.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FIRSTShares.Controllers
{
    public class AccountController : Controller
    {

        private readonly DatabaseContext Bd;

        public AccountController(DatabaseContext bd)
        {
            Bd = bd;
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CadastrarUsuario(Usuario usuario, string confirmaSenha,
            string numero, string sobrenome, IFormFile foto)
        {
            if (usuario.Senha == confirmaSenha)
                usuario.Senha = Criptografia.Codifica(usuario.Senha);
            else
            {
                ViewBag.Mensagem = "As senhas devem ser iguais.";

                return View("Register");
            }

            if (ChecarSeEmailEstaCadastrado(usuario.Email))
            {
                ViewBag.Mensagem = "Este e-mail já está cadastrado.";

                return View("Register");
            }

            var usuarioDb = new Usuario()
            {
                Nome = string.Format("{0} {1}", usuario.Nome, sobrenome),
                Email = usuario.Email,
                Senha = usuario.Senha,
                Time = RetornarTime(numero),
                CargoTime = usuario.CargoTime,
                DataCriacao = DateTime.Now,
                Cargo = Bd.Cargos.ToList().Find(cargo => cargo.Tipo == CargoTipo.Usuario)
            };

            SalvarUsuario(usuario);

            SalvarFoto(foto, usuario.Email);

            ViewBag.Mensagem = "Usuáro cadastrado com sucesso!";

            return View("Register");
        }


        [HttpPost]
        public IActionResult AcessarConta(string email, string senha)
        {
            var usuario = RetornarUsuarioPorEmail(email);

            if (usuario == null)
            {
                ViewBag.Mensagem = "Não há nenhum usuário cadastrado com esse e-mail.";

                return View("Login");
            }

            if (Criptografia.Compara(usuario.Senha, senha))
            {
                HttpContext.Session.SetObject("Usuario", usuario);

                return RedirectToAction("Index", "Home");
            }

            return View("Login");
        }


        private bool ChecarSeEmailEstaCadastrado(string email)
        {
            return Bd.Usuarios.ToList().Any(usuario => usuario.Email == email) ? true : false;
        }

        private Usuario RetornarUsuarioPorEmail(string email)
        {
            return Bd.Usuarios.ToList().Find(usuario => usuario.Email == email);
        }

        private string SalvarUsuario(Usuario usuario)
        {
            Bd.Usuarios.Add(usuario);
            if (Bd.SaveChanges() > 0)
                return ViewBag.Mensagem = "Usuário cadastrado com sucesso!";

            return ViewBag.Mensagem = "Falha ao cadastrar usuário.";
        }

        private Time RetornarTime(string numero)
        {
            if (numero != null)
            {
                var time = new Time();
                var retornarTime = time.RetornarTimePorNumero(numero, Bd.Times.ToList());

                if (retornarTime == null)
                {
                    var timeTheBlueAlliance = time.RetornarTimeTheBlueAlliance(numero);

                    time.Nome = timeTheBlueAlliance.Nome;
                    time.Numero = numero;
                    time.Pais = timeTheBlueAlliance.Pais;
                    time.CodPais = time.RetornarCodPais(time.Pais);

                    return time;
                }

                return retornarTime;
            }

            return null;
        }

        private void SalvarFoto(IFormFile foto, string email)
        {
            if (foto != null)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/usuarios", email + Path.GetExtension(foto.FileName));

                if (foto.Length > 0)
                    using (var stream = new FileStream(filePath, FileMode.Create))
                        foto.CopyToAsync(stream);
            }
        }
    }
}