using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FIRSTShares.API.Entities
{
    public class Permissao
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public bool Excluido { get; set; }
        public virtual Cargo Cargo { get; set; }
    }
}
