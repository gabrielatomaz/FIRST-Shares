using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FIRSTShares.API.Entities
{
    public class Curtida
    {
        public int ID { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual Postagem Postagem { get; set; }
        public bool Curtiu { get; set; }
        public bool Excluido { get; set; }
    }
}
