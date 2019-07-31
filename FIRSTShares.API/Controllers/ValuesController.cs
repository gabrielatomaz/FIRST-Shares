using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoogleTranslateFreeApi;
using Microsoft.AspNetCore.Mvc;

namespace FIRSTShares.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<string>>> GetAsync()
        //{
        //    //var translator = new GoogleTranslator();

        //    //var from = GoogleTranslator.GetLanguageByName("Portuguese");
        //    //var to = GoogleTranslator.GetLanguageByName("English");

        //    //var result = await translator.TranslateLiteAsync(
        //    //    "A FIRST Shares é uma plataforma web para participantes da competição de robótica internacional FIRST (tendo como foco a FIRST Robotics Competition) e para pessoas interessadas em robótica educacional. Contém um fórum, documentações, tutoriais e manuais traduzidos para português e espanhol, a fim de promover a FIRST na América do Sul. A FIRST é a maior competição de robótica do mundo.É um programa educacional que têm como meta desenvolver maneiras de inspirar os estudantes nas áreas de engenharia e tecnologia.A competição foi fundada em 1989 e, atualmente, possui 5 categorias, sendo elas: FIRST Lego League Jr. (para participantes de 6 a 10 anos); FIRST Lego League(9 a 14 anos); FIRST Tech Challenge(14 a 16 anos); FIRST Robotics Competition(16 a 18 anos) e FIRST Global Challenge(14 a 18 anos). Atualmente, na FIRST Robotics Competition, existem 7915 times registrados por todo mundo; entretanto, menos de 0.5 % desses times estão localizados na América do Sul.Isso acontece por alguns motivos, podemos citar, como exemplo, a falta de investimento tecnológico nos países da América do Sul o que torna a participação na competição extremamente cara, pois a inscrição na competição e a compra do kit de robótica é feita em dólares; além disso, hoje em dia, não existem competições na América do Sul.Uma grande barreira para times internacionais, hoje em dia, é que todos os tutoriais e documentação são feitos em inglês e não há tradução para outras línguas. Além disso, a plataforma online de troca de informações entre as equipes (Chief Delphi), também é completamente em inglês.Podemos trazer como exemplo o Brasil, que, de acordo com a revista brasileira especializada em economia, Exame, apenas 5 % da população fala uma segunda língua e menos de 3 % têm fluência em inglês.Com isso, o acesso a plataformas como Chief Delphi e o próprio site da competição, acabam se tornando inacessíveis.Em vista disso, é custoso para times internacionais se manterem(pois o material disponível online torna - se inacessível para aqueles que não falam inglês), fora que a comunicação entre outras equipes para troca de conhecimentos fica inviável.Por isso, é difícil a criação de novos times, visto que quase não há informações sobre a competição em outras línguas além do inglês.",
        //    //    from, to);

        //    ////The result is separated by the suggestions and the '\n' symbols
        //    //string[] resultSeparated = result.FragmentedTranslation;

        //    ////You can get all text using MergedTranslation property
        //    //string resultMerged = result.MergedTranslation;

        //    //return new string[] { resultMerged };
        //}

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

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
