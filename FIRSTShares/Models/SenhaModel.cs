using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FIRSTShares.Models
{
    public class SenhaModel
    {
        public int IDUsuario { get; set; }
        public string SenhaAtual { get; set; }
        public string Senha { get; set; }
        public string ConfirmaSenha { get; set; }
    }
}
