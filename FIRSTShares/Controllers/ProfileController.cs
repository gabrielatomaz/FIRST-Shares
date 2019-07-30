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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FIRSTShares.Controllers
{

    public class ProfileController : Controller
    {
        public readonly LazyContext BD;
        private Usuario Usuario;

        public ProfileController(LazyContext bd)
        {
            BD = bd;
            Usuario = new Usuario(BD);
        }

        [Authorize]
        public IActionResult Index()
        {
            var claims = (ClaimsIdentity)User.Identity;
            var usuario = Usuario.RetornarUsuarioPorNomeUsuario(claims.Claims.Single(u => u.Type == "NomeUsuario").Value);

            var errorViewModel = new ErrorViewModel { };

            var model = new Tuple<Usuario, ErrorViewModel>(usuario, errorViewModel);

            return View(model);
        }

        public IActionResult UserProfile(int idUsuario)
        {
            var usuario = Usuario.RetornarUsuario(idUsuario);
            var error = new ErrorViewModel { };

            var claims = (ClaimsIdentity)User.Identity;
            if (claims.Claims.Count() > 0)
            {
                var usuarioLogado = Usuario.RetornarUsuarioPorNomeUsuario(claims.Claims.Single(u => u.Type == "NomeUsuario").Value);


                var model = new Tuple<Usuario, ErrorViewModel>(usuarioLogado, error);

                if (usuarioLogado.NomeUsuario == usuario.NomeUsuario)
                    return View("Index", model);
            }

            var modelProfile = new Tuple<Usuario, ErrorViewModel>(usuario, error);

            return View("UserProfile", modelProfile);
        }

        public IActionResult AlterarFoto(IFormFile foto, int id)
        {
            var oneMb = 1024 * 1024;
            var usuario = Usuario.RetornarUsuario(id);

            var error = new ErrorViewModel
            {
                NotFound = foto.Length > oneMb
            };

            if (!AlterarUsuarioFoto(foto, usuario))
                error.NotFound = true;

            SalvarFoto(foto, usuario.NomeUsuario);

            var model = new Tuple<Usuario, ErrorViewModel>(usuario, error);

            return View("Index", model);
        }

        public bool AlterarUsuarioFoto(IFormFile foto, Usuario usaurio) {
            using (var ms = new MemoryStream())
            {
                foto.CopyTo(ms);
                usaurio.Foto.FotoBase64 = ms.ToArray();
            }

            BD.Update(usaurio);

            return BD.SaveChanges() > 0;
        }


        [HttpPost]
        public async Task<IActionResult> AlterarPerfilAsync([FromBody] ProfileModel profile)
        {
            var usuario = Usuario.RetornarUsuario(profile.ID);
            var claims = (ClaimsIdentity)User.Identity;

            if (usuario.Email != profile.Email)
            {
                if (Usuario.ChecarSeEmailEstaCadastrado(profile.Email))
                    return Json("Email encontra-se em uso.");

                usuario.Email = profile.Email;
            }

            if (usuario.NomeUsuario != profile.Usuario)
            {
                if (Usuario.ChecarSeUsuarioEstaCadastrado(profile.Usuario))
                    return Json("Usuário encontra-se em uso.");

                await UpdateClaimValueAsync(claims, "NomeUsuario", profile.Usuario);
                usuario.NomeUsuario = profile.Usuario;
            }

            usuario.Nome = profile.Nome;

            Usuario.AlterarUsuario(usuario);

            return Usuario.AlterarUsuario(usuario) ? Json("Usuário alterado com sucesso.") : Json("Erro ao alterar usuário");
        }

        public IActionResult AlterarSenha([FromBody] SenhaModel model)
        {
            var usuario = Usuario.RetornarUsuario(model.IDUsuario);

            if (!Criptografia.Compara(usuario.Senha, model.SenhaAtual))
                return Json("Senha inválida!");

            if (Criptografia.Compara(usuario.Senha, model.Senha))
                return Json("Senha atual e nova senha são iguais.");

            if (model.Senha != model.ConfirmaSenha)
                return Json("Senhas não são compatíveis.");

            usuario.Senha = Criptografia.Codifica(model.Senha);

            return Usuario.AlterarUsuario(usuario) ? Json("Usuário alterado com sucesso") : Json("Erro ao alterar usuário");
        }

        public IActionResult Denunciar([FromBody] DenunciaModel model)
        {
            var usuario = Usuario.RetornarUsuario(model.UsuarioDenunciadoID);

            var denuncia = new Denuncia
            {
                Motivo = model.Motivo,
                UsuarioDenunciado = usuario
            };

            BD.Add(denuncia);

            return BD.SaveChanges() > 0 ? Json("Obrigado! Nossos moderadores vão analisar seu pedido.") : Json("Falha ao denunciar usuário, tente mais tarde novamente.");
        }

        public async Task<IActionResult> ExcluirPerfilAsync(int id)
        {
            var usuario = Usuario.RetornarUsuario(id);

            usuario.Excluido = true;

            Usuario.AlterarUsuario(usuario);

            await HttpContext.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
        private async Task UpdateClaimValueAsync(ClaimsIdentity myClaims, string claim, string newValue)
        {
            myClaims.RemoveClaim(myClaims.FindFirst(claim));
            myClaims.AddClaim(new Claim(claim, newValue));

            var principal = new ClaimsPrincipal(myClaims);

            Thread.CurrentPrincipal = principal;

            await HttpContext.SignInAsync(principal);
        }

        private string SalvarFoto(IFormFile foto, string nomeUsuario)
        {
            if (foto != null)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/usuarios", nomeUsuario + Path.GetExtension(foto.FileName));

                if (foto.Length > 0)
                {
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