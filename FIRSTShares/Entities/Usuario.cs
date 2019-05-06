using FIRSTShares.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FIRSTShares.Entities
{
    public class Usuario
    {
        public DatabaseContext Bd { get; set; }
        public Usuario(DatabaseContext bd)
        {
            Bd = bd;
        }
        public Usuario() { }

        public int ID { get; set; }
        public string Nome { get; set; }
        public string NomeUsuario { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public CargoTime CargoTime { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool Excluido { get; set; } = false;
        public Time Time { get; set; }
        public Cargo Cargo { get; set; }
        public List<Curtida> Curtidas { get; set; }
        public List<Postagem> Postagens { get; set; }

        public Usuario RetonarUsuarioPorNomeUsuario(string nomeUsuario)
        {
            return Bd.Usuarios.Single(u => u.NomeUsuario == nomeUsuario);
        }
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
