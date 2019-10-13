using FIRSTShares.API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FIRSTShares.API.Entities
{
    public class Discussao
    {
        public LazyContext BD { get; set; }
        public Discussao(LazyContext bd)
        {
            BD = bd;
        }
        public Discussao() { }

        public int ID { get; set; }
        public string Assunto { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool Excluido { get; set; }
        public virtual List<Postagem> Postagens { get; set; }

        public Discussao Adicionar(string assunto, DateTime dataAtual)
        {
            var discussao = new Discussao
            {
                Assunto = assunto,
                DataCriacao = dataAtual
            };

            BD.Discussoes.Add(discussao);

            var salvar = BD.SaveChanges() > 0 ? true : false;

            return salvar ? discussao : null;
        }

        public Discussao Alterar(int idDiscussao, string assunto, DateTime dataAtual)
        {

            var discussao = BD.Discussoes.FirstOrDefault(post => post.ID == idDiscussao);

            discussao.Assunto = assunto;
            discussao.DataCriacao = dataAtual;

            BD.Discussoes.Update(discussao);

            var salvar = BD.SaveChanges() > 0 ? true : false;

            return salvar ? discussao : null;
        }
    }
}
