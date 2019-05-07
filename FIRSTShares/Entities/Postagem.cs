using System;
using System.Collections.Generic;

namespace FIRSTShares.Entities
{
    public class Postagem
    {
        public int ID { get; set; }
        public virtual Discussao Discussao { get; set; }
        public string ConteudoHtml { get; set; }
        public string Conteudo { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual Postagem PostagemPai { get; set; }
        public virtual List<Postagem> Postagens { get; set; }
        public bool PostagemOficial { get; set; }
        public DateTime DataCriacao { get; set; }
        public virtual List<Curtida> Curtidas { get; set; }
        public virtual Categoria Categoria { get; set; }
        public bool Excluido { get; set; }
    }
}