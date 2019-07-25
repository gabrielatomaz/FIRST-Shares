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
        public virtual List<Denuncia> Denuncias { get; set; }
        public virtual List<Anexo> Anexos { get; set; }

        public Usuario RetornarUsuarioPorNomeUsuario(string nomeUsuario)
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
            var cargo = BD.Cargos.SingleOrDefault(c => (int)c.Tipo == cargoId);

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

        public bool ChecarSeEmailOuUsuarioEstaCadastrado(Usuario usuario)
        {
            return BD.Usuarios
                .Any(u => (u.Email == usuario.Email || u.NomeUsuario == usuario.NomeUsuario) && u.Excluido == false);
        }

        public bool ChecarSeEmailOuUsuarioEstaCadastrado(string usuario, string email)
        {
            return BD.Usuarios
                .Any(u => (u.Email == email || u.NomeUsuario == usuario) && u.Excluido == false);
        }

        public bool ChecarSeEmailEstaCadastrado(string email)
        {
            return BD.Usuarios
                .Any(u => (u.Email == email) && u.Excluido == false);
        }

        public bool ChecarSeUsuarioEstaCadastrado(string usuario)
        {
            return BD.Usuarios
                .Any(u => (u.NomeUsuario == usuario) && u.Excluido == false);
        }

        public bool AlterarUsuario(Usuario usuario)
        {
            BD.Usuarios.Update(usuario);
            return BD.SaveChanges() > 0;
        }

        public Usuario RetornarUsuario(int id)
        {
            return BD.Usuarios.Single(u => u.ID == id);
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
