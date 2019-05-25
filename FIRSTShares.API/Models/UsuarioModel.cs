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
        public string Senha { get; set; }
        public CargoTime CargoTime { get; set; }
        public DateTime DataCriacao { get; set; }
        public string Foto { get; set; }
        public bool Excluido { get; set; } = false;
        public virtual TimeModel Time { get; set; }
        public virtual CargoModel Cargo { get; set; }
    }
}
