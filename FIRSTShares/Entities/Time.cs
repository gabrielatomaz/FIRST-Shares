using FIRSTShares.Client;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace FIRSTShares.Entities
{
    public class Time
    {
        public int ID { get; set; }
        public string Numero { get; set; }
        public string Nome { get; set; }
        public string Pais { get; set; }
        public string CodPais { get; set; }
        public bool Excluido { get; set; }
        public virtual List<Usuario> Usuarios { get; set; }

        public TimeTheBlueAlliance RetornarTimeTheBlueAlliance(string numero)
        {
            var theBlueAlliance = new TheBlueAllianceClient();

            return theBlueAlliance.RetoenarTimeTheBlueAllianceAsync(numero);
        }

        public bool ChecarSeTimeExiste(TimeTheBlueAlliance timeTheBlueAlliance)
        {
            if (timeTheBlueAlliance != null)
                return true;

            return false;
        }

        public bool ChecarSeTimeEstaCadastrado(string numero, List<Time> times)
        {
            if (!times.Any(time => time.Numero == numero))
                return false;

            return true;
        }

        public Time RetornarTimePorNumero(string numero, List<Time> times)
        {
            if (ChecarSeTimeEstaCadastrado(numero, times))
                return times.Find(time => time.Numero == numero);

            return null;
        }

        public string RetornarCodPais(string nomePais)
        {
            if (nomePais.Count() > 3)
            {
                var regions = CultureInfo.GetCultures(CultureTypes.SpecificCultures).Select(x => new RegionInfo(x.LCID));
                var englishRegion = regions.FirstOrDefault(region => region.EnglishName.Contains(nomePais));

                return englishRegion.TwoLetterISORegionName;
            }

            return nomePais;
        }
    }
}
