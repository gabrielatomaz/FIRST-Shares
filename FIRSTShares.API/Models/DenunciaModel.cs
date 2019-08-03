using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FIRSTShares.API.Models
{
    public class DenunciaModel
    {
        public int IDDenuncia { get; set; }
        public string Motivo { get; set; }
        public int IDUsuario { get; set; }
        public string NomeUsuario { get; set; }
        public string Foto { get; set; }
    }
}
