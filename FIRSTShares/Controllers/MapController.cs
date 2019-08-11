using System.Collections.Generic;
using System.Linq;
using FIRSTShares.Client;
using FIRSTShares.Data;
using FIRSTShares.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Globalization;
using FIRSTShares.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System;

namespace FIRSTShares.Controllers
{
    public class MapController : Controller
    {
        private readonly LazyContext BD;
        private readonly Usuario Usuario;

        public IActionResult Index()
        {
            MostrarFotoPerfil();

            return View();
        }


        public MapController(LazyContext context)
        {
            BD = context;

            Usuario = new Usuario(BD);
        }

        [HttpPost]
        public IActionResult CadastrarTime(string numero)
        {
            var time = new Time();
            var timeTheBlueAlliance = time.RetornarTimeTheBlueAlliance(numero);
            var timeExiste = time.ChecarSeTimeExiste(timeTheBlueAlliance);

            if (timeExiste && !(time.ChecarSeTimeEstaCadastrado(numero, RetornarTimes())))
            {
                time = new Time()
                {
                    Nome = timeTheBlueAlliance.Nome,
                    Numero = timeTheBlueAlliance.Numero,
                    Pais = timeTheBlueAlliance.Pais,
                    CodPais = time.RetornarCodPais(timeTheBlueAlliance.Pais)
                };

                SalvarTime(time);

                return View("Index");
            }

            ViewBag.Mensagem = timeExiste ?
                 "Este time já está cadastrado!" : "Não há nenhum time com esse número de registro!";

            return View("Index");
        }

        private string SalvarTime(Time time)
        {
            BD.Times.Add(time);
            if (BD.SaveChanges() > 0)
                return ViewBag.Mensagem = "Time cadastrado com sucesso!";

            return ViewBag.Mensagem = "Falha ao cadastrar time.";
        }

        public string RetornarTimesJson()
        {
            var times = RetornarTimes();
            var jsonPaisTimes = new { countries = RetornarPaisTimes(times) };

            return JsonConvert.SerializeObject(jsonPaisTimes, Formatting.Indented);
        }

        private List<Time> RetornarTimes()
        {
            return BD.Times.ToList();
        }

        private List<string> RetornarListaPaisesDeTimes(List<Time> times)
        {
            var paises = times.Select(time => time.CodPais).ToList();

            return paises.Distinct().ToList();
        }

        private Dictionary<string, List<string>> RetornarPaisTimes(List<Time> times)
        {
            var paisesModel = new List<TimeModel>();
            var dictionaryPais = new Dictionary<string, List<string>>();

            RetornarListaPaisesDeTimes(times).ForEach(pais => {
                var paisModel = new TimeModel();
                times.ForEach(time => {
                    if (time.CodPais == pais) {
                        paisModel.CodigoPais = time.CodPais;
                        paisModel.Infos.Add(time.Nome + " - " + time.Numero);
                    }

                    if (paisModel.CodigoPais != null && !dictionaryPais.Keys.Any(key => key.Equals(pais)))
                        dictionaryPais.Add(paisModel.CodigoPais, paisModel.Infos);
                });
            });

            return dictionaryPais;
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