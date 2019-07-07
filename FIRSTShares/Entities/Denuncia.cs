using FIRSTShares.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FIRSTShares.Entities
{
    public class Denuncia
    {
        public LazyContext BD { get; set; }
        public Denuncia(LazyContext bd)
        {
            BD = bd;
        }
        public Denuncia() { }

        public int ID { get; set; }
        public string Motivo { get; set; }
        public virtual Usuario UsuarioDenunciado { get; set; }
        public bool Excluido { get; set; }

        public List<Denuncia> RetornarTodasDenuncias()
        {
            return BD.Denuncias.Where(d => d.Excluido == false && d.UsuarioDenunciado.Excluido == false).ToList();
        }

        public Denuncia RetornarDenunciaPorUsuarioId(int idUsuario)
        {
            return BD.Denuncias.SingleOrDefault(d => d.UsuarioDenunciado.ID == idUsuario);
        }

        public bool Excluir(int idDenuncia)
        {
            var denuncia = RetornarDenunciaPorUsuarioId(idDenuncia);

            if (denuncia != null)
            {
                denuncia.Excluido = true;

                BD.Denuncias.Update(denuncia);
                BD.SaveChanges();

                return true;
            }

            return false;
        }

    }
}
