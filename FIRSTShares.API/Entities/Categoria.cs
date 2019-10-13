using FIRSTShares.API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FIRSTShares.API.Entities
{
    public class Categoria
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public bool Excluido { get; set; }

        public LazyContext Bd { get; set; }
        public Categoria(LazyContext bd)
        {
            Bd = bd;
        }

        public Categoria RetornarCategoriaPorId(int id)
        {
            return Bd.Categorias.Single(c => c.ID == id);
        }
    }
}
