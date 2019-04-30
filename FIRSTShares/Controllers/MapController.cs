using System.Collections.Generic;
using System.Linq;
using FIRSTShares.Client;
using FIRSTShares.Data;
using FIRSTShares.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Globalization;
using FIRSTShares.Entities;

namespace FIRSTShares.Controllers
{
    public class MapController : Controller
    {
        private readonly DatabaseContext Bd;

        public IActionResult Index()
        {
            return View();
        }


        public MapController(DatabaseContext context)
        {
            Bd = context;
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
            Bd.Times.Add(time);
            if (Bd.SaveChanges() > 0)
                return ViewBag.Mensagem = "Time cadastrado com sucesso!";

            return ViewBag.Mensagem = "Falha ao cadastrar time.";
        }



        public string RetonarTimesJson()
        {
            var times = RetornarTimes();
            var jsonPaisTimes = new { countries = RetornarPaisTimes(times) };

            return JsonConvert.SerializeObject(jsonPaisTimes, Formatting.Indented);
        }

        private List<Time> RetornarTimes()
        {
            return Bd.Times.ToList();
        }

        private List<string> RetornarListaPaisesDeTimes(List<Time> times)
        {
            var paises = new List<string>();

            foreach (var time in times)
            {
                if (!paises.Contains(time.CodPais))
                    paises.Add(time.CodPais);
            }

            return paises;
        }

        private Dictionary<string, List<string>> RetornarPaisTimes(List<Time> times)
        {
            var paisesModel = new List<PaisModel>();
            var dictionaryPais = new Dictionary<string, List<string>>();

            foreach (var pais in RetornarListaPaisesDeTimes(times))
            {
                var paisModel = new PaisModel();
                foreach (var time in times)
                {
                    if (time.CodPais == pais)
                    {
                        paisModel.Codigo = time.CodPais;
                        paisModel.Infos.Add(time.Nome + " - " + time.Numero);
                    }

                    if (paisModel.Codigo != null && !dictionaryPais.Keys.Any(key => key.Equals(pais)))
                        dictionaryPais.Add(paisModel.Codigo, paisModel.Infos);
                }
            }

            return dictionaryPais;
        }

    }
}