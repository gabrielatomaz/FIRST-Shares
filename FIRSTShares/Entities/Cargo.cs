using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FIRSTShares.Entities
{
    public class Cargo
    {
        public int ID { get; set; }
        public CargoTipo Tipo { get; set; }
        public virtual List<Permissao> Permissoes { get; set; }
        public bool Excluido { get; set; }
        public virtual List<Usuario> Usuarios { get; set; }
    }

    public enum CargoTipo
    {
        Administrador = 0,
        Moderador = 1,
        Usuario = 2
    }
}
