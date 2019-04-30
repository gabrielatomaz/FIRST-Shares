using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FIRSTShares.Models
{
    public class PaisModel
    {
        public string Codigo { get; set; }
        public List<string> Infos { get; set; } = new List<string>();
    }
}
