using FIRSTShares.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FIRSTShares.Entities
{
    public class Categoria
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public bool Excluido { get; set; }

        public DatabaseContext Bd { get; set; }
        public Categoria(DatabaseContext bd)
        {
            Bd = bd;
        }

        public Categoria RetornarCategoriaPorId(int id)
        {
            return Bd.Categorias.Single(c => c.ID == id);
        }
    }
}
