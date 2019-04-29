using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FIRSTShares.Entities
{
    public class Usuario
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public CargoTime CargoTime { get; set; }
        public DateTime DataCriacao { get; set; }
        public Time Time { get; set; }
        public Cargo Cargo { get; set; }
        public bool Excluido { get; set; } = false;
    }

    public enum CargoTime
    {
        Nenhum = 0,
        Coordenador = 1,
        Mentor = 2,
        Aluno = 3,
        Voluntario = 4
    }
}
