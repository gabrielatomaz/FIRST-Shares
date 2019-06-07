using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FIRSTShares.Models
{
    public class PostagemModel
    {
        public int ID { get; set; }
        public string Conteudo { get; set; }
        public string ConteudoHtml { get; set; }
        public int Categoria { get; set; }
        public string Assunto { get; set; }
        public bool PostagemOficial { get; set; }
    }
}
