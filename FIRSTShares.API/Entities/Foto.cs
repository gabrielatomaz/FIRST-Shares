using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FIRSTShares.API.Entities
{
    public class Foto
    {
        public Foto() { }
        public int ID { get; set; }
        public string FotoBase64 { get; set; }
        public virtual List<Usuario> Usuarios { get; set; }
    }
}
