using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FIRSTShares.Models
{
    public class AnexoModel
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string SRC { get; set; }
        public TipoAnexo Tipo { get; set; }
    }
}
