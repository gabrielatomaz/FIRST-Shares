using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using FIRSTShares.Data;
using FIRSTShares.Entities;
using FIRSTShares.Models;
using FIRSTShares.Util;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace FIRSTShares.Controllers
{
    public class AccountController : Controller
    {

        private readonly LazyContext BD;

        public AccountController(LazyContext bd)
        {
            BD = bd;
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index", "Home");
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

            if (ChecarSeEmailOuUsuarioEstaCadastrado(usuario))
            {
                ViewBag.Mensagem = "Este e-mail ou usuário já está cadastrado.";

                return View("Register");
            }

            var usuarioDb = new Usuario
            {
                Nome = string.Format("{0} {1}", usuario.Nome, sobrenome),
                Email = usuario.Email,
                Senha = usuario.Senha,
                Time = RetornarTime(numero),
                CargoTime = usuario.CargoTime,
                DataCriacao = DateTime.Now,
                Cargo = BD.Cargos.ToList().Find(cargo => cargo.Tipo == CargoTipo.Usuario),
                NomeUsuario = usuario.NomeUsuario,
                Foto = SalvarFoto(foto, usuario.NomeUsuario)
            };

            SalvarUsuario(usuarioDb);

            ViewBag.Mensagem = "Usuáro cadastrado com sucesso!";

            return View("Register");
        }


        [HttpPost]
        public async Task<IActionResult> AcessarConta(UsuarioViewModel usuarioModel)
        {
            var usuario = RetornarUsuarioPorEmailOuUsuario(usuarioModel.UsuarioEmail);
            if (usuario != null)
            {
                if (LoginUsuario(usuario, usuarioModel.Senha))
                {
                    var nomeUsuario = usuario.NomeUsuario;
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usuario.Nome),
                    new Claim(ClaimTypes.Email, usuario.Email),
                    new Claim("NomeUsuario", nomeUsuario),
                    new Claim("Foto", RetornarFoto(nomeUsuario))
                };

                    var userIdentity = new ClaimsIdentity(claims, "login");
                    var principal = new ClaimsPrincipal(userIdentity);

                    Thread.CurrentPrincipal = principal;

                    await HttpContext.SignInAsync(principal);

                    return RedirectToAction("Index", "Home");
                }
            }

            ViewBag.Mensagem = "Senha e/ou e-mail incorretos!";

            return View("Login");
        }

        public bool LoginUsuario(Usuario usuario, string senha)
        {
            return ((usuario == null) || (!Criptografia.Compara(usuario.Senha, senha))) ? false : true;
        }

        private bool ChecarSeEmailOuUsuarioEstaCadastrado(Usuario usuario)
        {
            return BD.Usuarios.Any(u => (u.Email == usuario.Email || u.NomeUsuario == usuario.NomeUsuario) && u.Excluido == false) ? true : false;
        }

        private Usuario RetornarUsuarioPorEmailOuUsuario(string emailUsuario)
        {
            return BD.Usuarios.SingleOrDefault(usuario => ((usuario.Email == emailUsuario) || (usuario.NomeUsuario == emailUsuario)) && usuario.Excluido == false);
        }

        private string SalvarUsuario(Usuario usuario)
        {
            BD.Usuarios.Add(usuario);
            if (BD.SaveChanges() > 0)
                return ViewBag.Mensagem = "Usuário cadastrado com sucesso!";

            return ViewBag.Mensagem = "Falha ao cadastrar usuário.";
        }

        private Time RetornarTime(string numero)
        {
            if (numero != null)
            {
                var time = new Time();
                var retornarTime = time.RetornarTimePorNumero(numero, BD.Times.ToList());

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

        private string RetornarFoto(string nomeUsuario)
        {
            var folderName = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/usuarios");
            var imgPadrao = "user.png";

            foreach (var files in Directory.GetFiles(folderName))
            {
                var info = new FileInfo(files);
                var fileName = Path.GetFileNameWithoutExtension(info.Name);

                if (fileName == nomeUsuario)
                    return Path.GetFileName(info.FullName);
            }

            return imgPadrao;
        }

        private string SalvarFoto(IFormFile foto, string nomeUsuario)
        {
            if (foto != null)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/usuarios", nomeUsuario + Path.GetExtension(foto.FileName));

                if (foto.Length > 0) { 
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        foto.CopyTo(stream);
                        stream.Flush();
                    }
                }

                return nomeUsuario + Path.GetExtension(foto.FileName);
            }

            return "user.png";
        }
    }
}