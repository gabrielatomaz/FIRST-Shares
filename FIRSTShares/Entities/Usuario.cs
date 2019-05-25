using FIRSTShares.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FIRSTShares.Entities
{
    public class Usuario
    {
        public LazyContext BD { get; set; }
        public Usuario(LazyContext bd)
        {
            BD = bd;
        }
        public Usuario() { }

        public int ID { get; set; }
        public string Nome { get; set; }
        public string NomeUsuario { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public CargoTime CargoTime { get; set; }
        public DateTime DataCriacao { get; set; }
        public string Foto { get; set; }
        public bool Excluido { get; set; } = false;
        public virtual Time Time { get; set; }
        public virtual Cargo Cargo { get; set; }
        public virtual List<Curtida> Curtidas { get; set; }
        public virtual List<Postagem> Postagens { get; set; }

        public Usuario RetonarUsuarioPorNomeUsuario(string nomeUsuario)
        {
            return BD.Usuarios.Single(u => u.NomeUsuario == nomeUsuario);
        }

        public List<Usuario> RetornarUsuarios()
        {
            return BD.Set<Usuario>().ToList();
        }

        public bool AlterarCargoUsuario(int idUsuario, int cargoId)
        {
            var usuario = BD.Usuarios.SingleOrDefault(u => u.ID == idUsuario);
            var cargo = BD.Cargos.SingleOrDefault(c => c.ID == cargoId);

            if (usuario != null && cargo != null)
            {
                usuario.Cargo = cargo;

                BD.Usuarios.Update(usuario);
                BD.SaveChanges();

                return true;
            }

            return false;
        }

        public bool Excluir(int idUsuario)
        {
            var usuario = BD.Usuarios.SingleOrDefault(u => u.ID == idUsuario);

            if (usuario != null)
            {
                usuario.Excluido = true;

                BD.Usuarios.Update(usuario);
                BD.SaveChanges();

                return true;
            }

            return false;
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
