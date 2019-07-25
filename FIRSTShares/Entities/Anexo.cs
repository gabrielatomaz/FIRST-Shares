using System;

namespace FIRSTShares.Entities
{
    public class Anexo
    {
        public int ID { get; set; }
        public string SRC { get; set; }
        public string Titulo { get; set; }
        public string Resumo { get; set; }
        public TipoAnexo TipoAnexo { get; set; }
        public virtual Usuario Usuario { get; set; }
        public bool Excluido { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}

public enum TipoAnexo
{
    Video,
    PDF,
    NULL, 
    Manual
}
