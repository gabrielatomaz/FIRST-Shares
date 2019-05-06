using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FIRSTShares.Entities
{
    public class Discussao
    {
        public int ID { get; set; }
        public string Assunto { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool Excluido { get; set; }
        public List<Postagem> Postagens { get; set; }
    }
}
