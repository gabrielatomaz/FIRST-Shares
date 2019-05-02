using FIRSTShares.Entities;
using Microsoft.EntityFrameworkCore;

namespace FIRSTShares.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options = null) : base(options) { }

        public DbSet<Time> Times { get; set; }
        public DbSet<Permissao> Permissoes { get; set; }
        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Discussao> Discussoes { get; set; }
        public DbSet<Curtida> Curtidas { get; set; }
        public DbSet<Postagem> Postagens { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cargo>()
                .HasMany(p => p.Permissoes);

            modelBuilder.Entity<Usuario>()
                .HasOne( t => t.Time);

            modelBuilder.Entity<Usuario>()
                .HasOne(c => c.Cargo);

            modelBuilder.Entity<Curtida>()
                .HasOne(p => p.Postagem);
            modelBuilder.Entity<Curtida>()
                .HasOne(u => u.Usuario);

            modelBuilder.Entity<Postagem>()
                .HasOne(d => d.Discussao);
            modelBuilder.Entity<Postagem>()
                .HasOne(u => u.Usuario);
            modelBuilder.Entity<Postagem>()
                .HasMany(p => p.Postagens);
            modelBuilder.Entity<Postagem>()
                .HasMany(c => c.Curtidas);
            modelBuilder.Entity<Postagem>()
                .HasOne(p => p.PostagemPai);
             
        }
    }
}
