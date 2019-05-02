using System;
using System.Collections.Generic;

namespace FIRSTShares.Entities
{
    public class Postagem
    {
        public int ID { get; set; }
        public Discussao Discussao { get; set; }
        public string Conteudo { get; set; }
        public Usuario Usuario { get; set; }
        public Postagem PostagemPai { get; set; }
        public List<Postagem> Postagens { get; set; }
        public bool PostagemOficial { get; set; }
        public DateTime DataCriacao { get; set; }
        public List<Curtida> Curtidas { get; set; }
        public bool Excluido { get; set; }
    }
}