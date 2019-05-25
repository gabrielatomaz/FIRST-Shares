using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FIRSTShares.API.Models
{
    public class TimeModel
    {
        public int ID { get; set; }
        public string Numero { get; set; }
        public string Nome { get; set; }
        public string Pais { get; set; }
        public string CodPais { get; set; }
        public bool Excluido { get; set; }
    }
}
