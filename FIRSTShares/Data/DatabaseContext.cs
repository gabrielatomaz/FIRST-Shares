using FIRSTShares.Entities;
using Microsoft.EntityFrameworkCore;

namespace FIRSTShares.Data
{
    public class LazyContext : DbContext
    {
        public LazyContext(DbContextOptions<LazyContext> options = null) : base(options) { }

        public DbSet<Time> Times { get; set; }
        public DbSet<Permissao> Permissoes { get; set; }
        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Discussao> Discussoes { get; set; }
        public DbSet<Curtida> Curtidas { get; set; }
        public DbSet<Postagem> Postagens { get; set; }
        public DbSet<Denuncia> Denuncias { get; set; }
        public DbSet<Anexo> Anexos { get; set; }
        public DbSet<Foto> Fotos { get; set; }
        public DbSet<Notificacao> Notificacoes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Permissao>()
                .HasOne(c => c.Cargo)
                .WithMany(p => p.Permissoes);

            modelBuilder.Entity<Usuario>()
                .HasOne(t => t.Time)
                .WithMany(u => u.Usuarios);

            modelBuilder.Entity<Usuario>()
                .HasOne(c => c.Cargo)
                .WithMany(u => u.Usuarios);

            modelBuilder.Entity<Curtida>()
                .HasOne(u => u.Usuario)
                .WithMany(c => c.Curtidas);

            modelBuilder.Entity<Curtida>()
                .HasOne(p => p.Postagem)
                .WithMany(c => c.Curtidas);

            modelBuilder.Entity<Postagem>()
                .HasOne(d => d.Discussao)
                .WithMany(p => p.Postagens);

            modelBuilder.Entity<Postagem>()
                .HasOne(u => u.Usuario)
                .WithMany(p => p.Postagens);

            modelBuilder.Entity<Postagem>()
                .HasMany(p => p.Postagens);

            modelBuilder.Entity<Postagem>()
                .HasOne(p => p.PostagemPai);

            modelBuilder.Entity<Postagem>()
                .HasOne(d => d.Discussao)
                .WithMany(p => p.Postagens);

            modelBuilder.Entity<Denuncia>()
               .HasOne(d => d.UsuarioDenunciado)
               .WithMany(u => u.Denuncias);

            modelBuilder.Entity<Anexo>()
                .HasOne(u => u.Usuario)
                .WithMany(a => a.Anexos);

            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Foto)
                .WithMany(f => f.Usuarios);

            modelBuilder.Entity<Notificacao>()
                .HasOne(n => n.UsuarioNotificado)
                .WithMany(u => u.NotificacoesRecebidas);

            modelBuilder.Entity<Notificacao>()
               .HasOne(n => n.UsuarioAcao)
               .WithMany(u => u.NotificacoesFeitas);
        }
    }
}
