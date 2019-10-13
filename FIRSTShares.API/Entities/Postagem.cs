using FIRSTShares.API.Data;
using System;
using System.Collections.Generic;

namespace FIRSTShares.API.Entities
{
    public class Postagem
    {
        public LazyContext BD { get; set; }
        public Postagem(LazyContext bd)
        {
            BD = bd;
        }
        public Postagem() { }

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


        public bool Salvar(Postagem postagem)
        {
            BD.Postagens.Add(postagem);

            return BD.SaveChanges() > 0;
        }

        public bool Alterar(Postagem postagem)
        {
            BD.Postagens.Update(postagem);

            return BD.SaveChanges() > 0;
        }
    }
}