using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FIRSTShares.API.Entities
{
    public class Notificacao
    {
        public int ID { get; set; }
        public virtual Usuario UsuarioNotificado { get; set; }
        public virtual Usuario UsuarioAcao { get; set; }
        public TipoAcao Acao { get; set; }
        public bool Excluido { get; set; }
        public DateTime Data { get; set; }
    }

    public enum TipoAcao {
        VisualizouPerfil,
        Comentou,
        Curtiu
    }
}
