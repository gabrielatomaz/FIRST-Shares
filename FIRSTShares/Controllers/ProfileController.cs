using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
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

            MostrarFotoPerfil();

            var errorViewModel = new ErrorViewModel { };

            var model = new Tuple<Usuario, ErrorViewModel>(usuario, errorViewModel);

            return View(model);
        }

        public IActionResult UserProfile(int idUsuario)
        {
            var usuario = Usuario.RetornarUsuario(idUsuario);
            var error = new ErrorViewModel { };

            MostrarFotoPerfil();

            var claims = (ClaimsIdentity)User.Identity;
            if (claims.Claims.Count() > 0)
            {
                var usuarioLogado = Usuario.RetornarUsuarioPorNomeUsuario(claims.Claims.Single(u => u.Type == "NomeUsuario").Value);
                var model = new Tuple<Usuario, ErrorViewModel>(usuarioLogado, error);

                if (usuarioLogado.ID != usuario.ID)
                {
                    var notificacao = new Notificacao
                    {
                        Acao = TipoAcao.VisualizouPerfil,
                        UsuarioAcao = usuarioLogado,
                        UsuarioNotificado = usuario,
                        Data = DateTime.Now
                    };

                    BD.Add(notificacao);
                    BD.SaveChanges();
                }

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

            var model = new Tuple<Usuario, ErrorViewModel>(usuario, error);
            MostrarFotoPerfil();

            return View("Index", model);
        }

        public bool AlterarUsuarioFoto(IFormFile foto, Usuario usaurio)
        {
            using (var ms = new MemoryStream())
            {
                foto.CopyTo(ms);
                var fotoArray = ms.ToArray();
                fotoArray = ResizeImage(byteArrayToImage(fotoArray), 400, 400);
                usaurio.Foto.FotoBase64 = fotoArray;
            }

            BD.Update(usaurio);

            return BD.SaveChanges() > 0;
        }


        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            var ms = new MemoryStream(byteArrayIn);
            var returnImage = Image.FromStream(ms);

            return returnImage;
        }

        public byte[] ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            MemoryStream ms = new MemoryStream();
            destImage.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);

            return ms.ToArray();
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
            MostrarFotoPerfil();

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

        private void MostrarFotoPerfil()
        {
            if (User.Identity.IsAuthenticated)
            {
                var claims = (ClaimsIdentity)User.Identity;
                var usuario = Usuario.RetornarUsuarioPorNomeUsuario(claims.Claims.Single(u => u.Type == "NomeUsuario").Value);

                var foto = Convert.ToBase64String(usuario.Foto.FotoBase64);
                ViewData["foto"] = foto;
                ViewData["temNotificacao"] = usuario.NotificacoesRecebidas.Where(n => !n.Excluido).ToList().Count > 0;
            }
        }
    }
}