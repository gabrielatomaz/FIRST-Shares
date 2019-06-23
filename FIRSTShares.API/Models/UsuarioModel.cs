using FIRSTShares.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FIRSTShares.API.Models
{
    public class UsuarioModel
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string NomeUsuario { get; set; }
        public string Email { get; set; }
        public CargoTipo CargoTipo { get; set; }

    }
}
